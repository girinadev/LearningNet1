use HospitalDB

-- 1) диагнозы, которые были поставлены за последний месяц в порядке убывания их частоты за все время
 select 
	 di.Title as Diagnose
	 , count(rc.DiagnoseId) as 'Count'
 from dbo.Receptions as rc inner join dbo.Diagnoses as di on di.Id = rc.DiagnoseId
 group by rc.DiagnoseId, di.Title
 having rc.DiagnoseId in (select distinct rc.DiagnoseId
			 from dbo.Receptions as rc
			 where rc.StartDate >= dateadd(month, datediff(month, 0, dateadd(month, -1,current_timestamp)), 0))
 order by 'Count' desc

-- 2) статистику по отделениям: вывести для каждого отделения врача, который больше всего работал и самый частый диагноз (по всем врачам) за последние три месяца
--врача, который больше всего работал
select 
	  dep.Title
      ,doc.FirstName + ' ' + doc.LastName as Doctor
	  ,doc.Title as DoctorTitle
	  ,depdocmax.MaxDuration as MaxWorkMinutes
	  ,di.Title
	  ,mdc.DiagnoseCount
from 
   dbo.Department as dep 
   left join dbo.Doctors_Departments as dd on dd.DepartmentId = dep.Id
   left join dbo.Doctors as doc on dd.DoctorId = doc.Id
   inner join 
   (
	select drt.DoctorDepartmentId, max(drt.TotalDuration) as MaxDuration
    from
	(select dre.DoctorDepartmentId, dre.ReceptionMonth, sum(dre.Duration)  as TotalDuration
	from 
		(select re.DoctorDepartmentId, datepart(m, re.StartDate) as ReceptionMonth, du.Duration
		 from [dbo].Receptions as re left join [dbo].Durations as du on du.Id = re.DurationId
		 where re.StartDate >= dateadd(month, datediff(month, 0, dateadd(month, -3,current_timestamp)), 0)
		  ) as dre
	group by dre.DoctorDepartmentId, dre.ReceptionMonth) as drt
    group by drt.DoctorDepartmentId
	) as depdocmax on dd.Id = depdocmax.DoctorDepartmentId	
-- самый частый диагноз (по всем врачам)
   left join
   (
   select
	distinct  ddc.*
from
	(
	select rc1.DoctorDepartmentId, rc1.DiagnoseId, count(rc1.DiagnoseId) as DiagnoseCount
								from dbo.Receptions as rc1
								group by rc1.DoctorDepartmentId, rc1.DiagnoseId
	) as ddc
	inner join (
		select  gd.DoctorDepartmentId, max(gd.DiagnoseCount) as MaxDiagnoseCount
		from   
			(select rc1.DoctorDepartmentId, rc1.DiagnoseId, count(rc1.DiagnoseId) as DiagnoseCount
			from dbo.Receptions as rc1
			where rc1.StartDate >= dateadd(month, Datediff(month, 0, dateadd(month, -3,current_timestamp)), 0)
			group by rc1.DoctorDepartmentId, rc1.DiagnoseId
			) as gd
		group by gd.DoctorDepartmentId
	) mxdc ON 
	ddc.DoctorDepartmentId = mxdc.DoctorDepartmentId
	AND ddc.DiagnoseCount = mxdc.MaxDiagnoseCount
   ) as mdc on mdc.DoctorDepartmentId = dd.Id
   left join 
   dbo.Diagnoses as di on di.Id = mdc.DiagnoseId

-- 3) перерывы в работе врача более чем 30 минут, выводятся по уменьшению длительности перерыва. Выводить нужно колонки имя врача, его отделение, дата и время начала перерыва, длительность и конец перева.
declare @breakResult table (DoctorDepartmentId int, BreakStartDate datetime2, BreakEndDate datetime2, BreakDuration int)

insert into @breakResult
select 
	r.DoctorDepartmentId
	,r.EndDate    as BreakStartDate     
	,lead(r.StartDate) over (order by r.StartDate) as BreakEndDate
	,case when datepart(day, r.EndDate) = datepart(day, (lead(r.StartDate) over (order by r.StartDate))) then datediff(minute, r.EndDate, (lead(r.StartDate) over (order by r.StartDate))) else 0 end as BreakDuration
from 
(
	select 
	rc.DoctorDepartmentId
	,rc.StartDate
	,(case when rc.StartDate IS NULL then NULL else dateadd(minute,(select Duration from Durations where Durations.Id = DurationId), rc.StartDate) end) as EndDate
	from dbo.Receptions as rc
) as r

select 
	doc.FirstName + ' '+ doc.LastName as Doctor
	,dep.Title
	,format (br.BreakStartDate, 'dd-MM-yy hh:mm') as BreakStartDate
	,br.BreakDuration
	,format (br.BreakEndDate, 'dd-MM-yy hh:mm') as BreakEndDate
from dbo.Doctors_Departments dd
	left join dbo.Doctors as doc on doc.Id = dd.DoctorId
	left join dbo.Department as dep on dep.Id = dd.DepartmentId
	left join @breakResult as br on br.DoctorDepartmentId = dd.Id
where br.BreakDuration > 30 
order by br.BreakDuration desc

-- 4) список врачей, по каждому указано количество посетивших его пациентов за все время, среднее число пациентов в месяц, самый частый диагноз, в порядке убывания среднего числа пациентов за месяц
select  
	ptc.DoctorId 
	,doc.FirstName + ' ' + doc.LastName as DoctorName
	,doc.Title as DoctorTitle
	,ptc.PatientTotalCount
	,pavg.PatientAvgCountByMonth
	,di.Title as DiagnoseTitle
	,diag.DiagnoseCount
from 
--количество посетивших его пациентов за все время, 
(select dd.DoctorId, count(rc.PatientId) as PatientTotalCount
from dbo.Doctors as doc 
     left join dbo.Doctors_Departments as dd on doc.Id = dd.DoctorId
     left join  dbo.[Receptions] as rc on rc.DoctorDepartmentId = dd.Id   
group by dd.DoctorId) as ptc  
inner join 
--среднее число пациентов в месяц,
(select da.DoctorId, cast(sum(PatientTotalCountByMonth) as float) / (case when count(da.ReceptionMonth) > 0 then count(da.ReceptionMonth) else 1 end)  as PatientAvgCountByMonth 
from (select dd.DoctorId, datepart(month, rc.StartDate) ReceptionMonth, count(rc.PatientId) as PatientTotalCountByMonth
from dbo.Doctors as doc 
     left join dbo.Doctors_Departments as dd on doc.Id = dd.DoctorId
     left join  dbo.[Receptions] as rc on rc.DoctorDepartmentId = dd.Id  
group by  dd.DoctorId, datepart(month, rc.StartDate)) as da 
group by  da.DoctorId) as pavg on ptc.DoctorId = pavg.DoctorId
inner join dbo.Doctors as doc on ptc.DoctorId = doc.Id
--самый частый диагноз,
inner join 
(select dd.DoctorId, rc.DiagnoseId, count(rc.DiagnoseId) as DiagnoseCount
from dbo.Doctors as doc 
		left join dbo.Doctors_Departments as dd on doc.Id = dd.DoctorId
		left join  dbo.[Receptions] as rc on rc.DoctorDepartmentId = dd.Id 
group by dd.DoctorId, rc.DiagnoseId
having count(rc.DiagnoseId) = (
			select max(gd.DiagnoseCount) as MaxDiagnoseCount
			from (select dd.DoctorId, rc1.DiagnoseId, count(rc1.DiagnoseId) as DiagnoseCount
			from dbo.Doctors as doc1 
					left join dbo.Doctors_Departments as dd on doc1.Id = dd.DoctorId
					left join  dbo.[Receptions] as rc1 on rc1.DoctorDepartmentId = dd.Id 
			group by dd.DoctorId, rc1.DiagnoseId) as gd
			group by gd.DoctorId
			having gd.DoctorId = dd.DoctorId
)) as diag on diag.DoctorId = doc.Id
inner join 
dbo.Diagnoses as di on di.Id = diag.DiagnoseId 
--в порядке убывания среднего числа пациентов за месяц
order by pavg.PatientAvgCountByMonth desc

-- 5) число пациентов, проходящих прием, сгруппированных по времени прихода с 7-10, 10-13, 13-16, 16-19
(select '7-10' as Interval, count(rc.PatientId) as PatientsCount 
from dbo.[Receptions] as rc 
where cast(rc.StartDate as time) >= '07:00:00' and cast(rc.StartDate as time) < '10:00:00'
union
select '10-13' as Interval, count(rc.PatientId) as PatientsCount 
from dbo.[Receptions] as rc 
where cast(rc.StartDate as time) >= '10:00:00' and cast(rc.StartDate as time) < '13:00:00'
union
select '13-16' as Interval, count(rc.PatientId) as PatientsCount
from dbo.[Receptions] as rc 
where cast(rc.StartDate as time) >= '13:00:00' and cast(rc.StartDate as time) < '16:00:00'
union
select '16-19' as Interval, count(rc.PatientId) as PatientsCount 
from dbo.[Receptions] as rc 
where cast(rc.StartDate as time) >= '16:00:00' and cast(rc.StartDate as time) < '19:00:00')
order by PatientsCount desc