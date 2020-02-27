USE [HospitalDB]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2/27/2020 8:33:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diagnoses]    Script Date: 2/27/2020 8:33:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diagnoses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Diagnoses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 2/27/2020 8:33:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors_Departments]    Script Date: 2/27/2020 8:33:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors_Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Doctors_Departments_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Durations]    Script Date: 2/27/2020 8:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Durations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Duration] [smallint] NOT NULL,
 CONSTRAINT [PK_Durations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 2/27/2020 8:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receptions]    Script Date: 2/27/2020 8:33:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DoctorDepartmentId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[DurationId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[DiagnoseId] [int] NULL,
 CONSTRAINT [PK_Receptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([Id], [Title]) VALUES (1, N'Ортопедия и Травматология')
GO
INSERT [dbo].[Department] ([Id], [Title]) VALUES (2, N'Терапия')
GO
INSERT [dbo].[Department] ([Id], [Title]) VALUES (3, N'Хирургия')
GO
INSERT [dbo].[Department] ([Id], [Title]) VALUES (4, N'Урология-андрология')
GO
INSERT [dbo].[Department] ([Id], [Title]) VALUES (5, N'Гинекология')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Diagnoses] ON 
GO
INSERT [dbo].[Diagnoses] ([Id], [Title]) VALUES (1, N'Диагноз1')
GO
INSERT [dbo].[Diagnoses] ([Id], [Title]) VALUES (2, N'Диагноз2')
GO
INSERT [dbo].[Diagnoses] ([Id], [Title]) VALUES (3, N'Диагноз3')
GO
INSERT [dbo].[Diagnoses] ([Id], [Title]) VALUES (4, N'Диагноз4')
GO
INSERT [dbo].[Diagnoses] ([Id], [Title]) VALUES (5, N'Диагноз5')
GO
SET IDENTITY_INSERT [dbo].[Diagnoses] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (2, N'Андрей', N'Авраменко', N'Врач-ортопед-травматолог высшей категории.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (5, N'Наталья', N'Базылева', N'Врач-ревматолог высшей категории.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (6, N'Артем', N'Бреус', N'Врач-хирург высшей категории. Онкодерматолог. Специалист по ударно-волновой терапии. Заведующий хирургическим отделением и операционным блоком.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (7, N'Сергей', N'Глущенко', N'Заведующий урологическим отделением. Врач-уролог первой категории.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (8, N'Валентина', N'Гниненко', N'Детский врач-гинеколог первой категории.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (10, N'Юрий ', N'Зозуля', N'Врач-нейрохирург высшей категории, к.мед.н.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (11, N'Сергей', N'Слесаренко', N'Доктор мед. наук, врач-хирург, пластический хирург, комбустиолог высшей категории.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (12, N'Иван', N'Жердев', N'Врач-ортопед-травматолог высшей категории кандидат медицинских наук, заслуженный врач Украины.')
GO
INSERT [dbo].[Doctors] ([Id], [FirstName], [LastName], [Title]) VALUES (13, N'Валентин', N'Куринной', N'Врач-ортопед-травматолог')
GO
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctors_Departments] ON 
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (1, 2, 1)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (2, 5, 2)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (3, 6, 3)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (4, 7, 4)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (5, 8, 5)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (6, 10, 3)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (7, 11, 3)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (8, 12, 1)
GO
INSERT [dbo].[Doctors_Departments] ([Id], [DoctorId], [DepartmentId]) VALUES (9, 13, 1)
GO
SET IDENTITY_INSERT [dbo].[Doctors_Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Durations] ON 
GO
INSERT [dbo].[Durations] ([Id], [Duration]) VALUES (1, 10)
GO
INSERT [dbo].[Durations] ([Id], [Duration]) VALUES (2, 20)
GO
INSERT [dbo].[Durations] ([Id], [Duration]) VALUES (3, 30)
GO
SET IDENTITY_INSERT [dbo].[Durations] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (1, N'Лев', N'Мышлен', N'+380979979459')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (2, N'Потам', N'Потапенко', N'+389624365343')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (3, N'Наталья', N'Петрова', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (4, N'Александр', N'Иванов', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (5, N'Ольга', N'Дроздова', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (6, N'Евгения', N'Лисовол', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (7, N'Алексей', N'Ютило', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (9, N'Дмитрий', N'Борисов', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (11, N'Инна', N'Кульбашная', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (12, N'Алексей', N'Торба', N'+3809737373333')
GO
INSERT [dbo].[Patients] ([Id], [FirstName], [LastName], [Phone]) VALUES (13, N'Максим', N'Дворов', N'+3809737373333')
GO
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Receptions] ON 
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (2, 1, 1, 1, CAST(N'2020-01-10T08:46:58.2000000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (3, 2, 2, 2, CAST(N'2020-01-11T11:47:15.9766667' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (4, 3, 3, 3, CAST(N'2020-01-11T13:47:32.3400000' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (5, 4, 4, 1, CAST(N'2020-01-13T16:47:44.4133333' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (6, 5, 5, 2, CAST(N'2020-02-24T18:47:59.6200000' AS DateTime2), 5)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (7, 6, 6, 3, CAST(N'2020-02-24T18:48:12.6200000' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (9, 7, 7, 1, CAST(N'2020-02-24T18:48:36.7200000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (13, 8, 9, 2, CAST(N'2020-02-24T18:49:08.9900000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (14, 8, 9, 3, CAST(N'2020-02-24T18:49:19.2866667' AS DateTime2), 4)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (15, 1, 11, 1, CAST(N'2020-02-24T10:49:38.4900000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (16, 2, 12, 2, CAST(N'2020-02-24T18:49:51.1600000' AS DateTime2), 4)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (17, 3, 13, 3, CAST(N'2020-02-24T18:50:00.9666667' AS DateTime2), 5)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (18, 4, 1, 1, CAST(N'2020-02-24T18:50:45.1766667' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (19, 5, 2, 2, CAST(N'2020-02-24T18:50:52.1866667' AS DateTime2), 4)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (20, 6, 3, 3, CAST(N'2020-02-24T18:50:58.7700000' AS DateTime2), 3)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (21, 7, 4, 1, CAST(N'2020-02-24T10:51:06.3400000' AS DateTime2), 3)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (22, 8, 5, 2, CAST(N'2020-02-24T13:51:15.2366667' AS DateTime2), 4)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (23, 8, 6, 3, CAST(N'2020-02-24T15:51:28.9733333' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (28, 1, 9, 1, CAST(N'2020-02-24T18:52:13.8466667' AS DateTime2), 5)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (29, 5, 6, 2, CAST(N'2020-02-27T09:49:06.9366667' AS DateTime2), 3)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (1002, 1, 2, 2, CAST(N'2020-01-10T08:57:58.3000000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (1003, 1, 2, 2, CAST(N'2020-01-10T11:00:00.3000000' AS DateTime2), 2)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (1004, 1, 2, 1, CAST(N'2020-01-10T11:30:00.3000000' AS DateTime2), 1)
GO
INSERT [dbo].[Receptions] ([Id], [DoctorDepartmentId], [PatientId], [DurationId], [StartDate], [DiagnoseId]) VALUES (1005, 1, 2, 3, CAST(N'2020-01-10T15:30:00.4560000' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Receptions] OFF
GO
ALTER TABLE [dbo].[Receptions] ADD  CONSTRAINT [DF_Receptions_StartDate]  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[Doctors_Departments]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Departments_Doctors] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Doctors_Departments] CHECK CONSTRAINT [FK_Doctors_Departments_Doctors]
GO
ALTER TABLE [dbo].[Doctors_Departments]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Departments_Doctors1] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Doctors_Departments] CHECK CONSTRAINT [FK_Doctors_Departments_Doctors1]
GO
ALTER TABLE [dbo].[Receptions]  WITH CHECK ADD  CONSTRAINT [FK_Receptions_Diagnoses] FOREIGN KEY([DiagnoseId])
REFERENCES [dbo].[Diagnoses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receptions] CHECK CONSTRAINT [FK_Receptions_Diagnoses]
GO
ALTER TABLE [dbo].[Receptions]  WITH CHECK ADD  CONSTRAINT [FK_Receptions_Doctors_Departments] FOREIGN KEY([DoctorDepartmentId])
REFERENCES [dbo].[Doctors_Departments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receptions] CHECK CONSTRAINT [FK_Receptions_Doctors_Departments]
GO
ALTER TABLE [dbo].[Receptions]  WITH CHECK ADD  CONSTRAINT [FK_Receptions_Durations] FOREIGN KEY([DurationId])
REFERENCES [dbo].[Durations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receptions] CHECK CONSTRAINT [FK_Receptions_Durations]
GO
ALTER TABLE [dbo].[Receptions]  WITH CHECK ADD  CONSTRAINT [FK_Receptions_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Receptions] CHECK CONSTRAINT [FK_Receptions_Patients]
GO
