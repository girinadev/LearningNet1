namespace TaskApp.TaskClasses
{
  public class EmployeeSalary
  {
    private readonly decimal amount;
    private readonly decimal fee;

    public decimal Amount => amount;
    public decimal Fee => fee;

    public EmployeeSalary(decimal amount, decimal fee)
    {
      this.amount = amount;
      this.fee = fee;
    }
  }
}
