namespace TaskApp.TaskClasses
{
  public class Employee
  {
    private readonly string _firstName;

    private readonly string _lastName;

    public Positions Position { get; set; }

    public decimal Experience { get; set; }

    public Employee(string firstName, string lastName)
    {
      this._firstName = firstName;
      this._lastName = lastName;
    }

    public EmployeeSalary GetSalary(decimal feePercent)
    {
      var salaryAmount = PayPerDay() * 160;
      var fee = salaryAmount * feePercent / 100;
      return new EmployeeSalary(salaryAmount, fee);
    }

    private decimal PayPerDay()
    {
      var payPerDay = 3M;
      switch (Position)
      {
        case Positions.CEO: payPerDay *= 10; break;
        case Positions.PM: payPerDay *= 9; break;
        case Positions.Designer: payPerDay *= 5; break;
        case Positions.Deveporer: payPerDay *= 6; break;
        case Positions.DeveporerLead: payPerDay *= 8; break;
        case Positions.QA: payPerDay *= 4; break;
        case Positions.QALead: payPerDay *= 7; break;
      }

      if (Experience <= 0.5M)
        payPerDay *= 0.5M;
      else if (Experience > 0.5M && Experience <= 2M)
        payPerDay *= 1M;
      else if (Experience > 2M && Experience <= 5M)
        payPerDay *= 1.5M;
      else if (Experience > 5M)
        payPerDay *= 2M;

      return payPerDay;
    }

    public override string ToString()
    {
      return $"{this._firstName} {this._lastName} {this.Position} {this.Experience}";
    }
  }
}
