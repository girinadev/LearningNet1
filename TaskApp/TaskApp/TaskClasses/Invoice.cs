namespace TaskApp.TaskClasses
{
  public class Invoice
  {
    private readonly int account;
    private readonly string customer;
    private readonly string provider;

    private string article;
    private int quantity;

    public decimal UnitPrice { get; set; }

    public Invoice(int account, string customer, string provider, string article, int quantity)
    {
      this.account = account;
      this.customer = customer;
      this.provider = provider;
      this.article = article;
      this.quantity = quantity;
    }

    public decimal CalculatePrice(bool withNdc)
    {
      var p = UnitPrice * quantity;
      return withNdc ? p + p * 0.2M : p;
    }

    public override string ToString()
    {
      return $"Account: {this.account} Customer: {this.customer} Provider: {this.provider} Article: {this.article} Quantity: {this.quantity} UnitPrice {this.UnitPrice.ToString("C")}";
    }
  }
}
