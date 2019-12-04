using System;
using TaskApp.TaskClasses;

namespace TaskApp
{
  class Program
  {
    static void Main(string[] args)
    {
      //1
      var address = new Address
      {
        Index = 4900
      };
      Console.WriteLine($"Address: {string.Join(",", address.Index, address.Country, address.City, address.Street, address.House, address.Appartment)}");

      //2
      var rectangle = new Rectangle(5, 6);
      rectangle.AreaCalculator();
      rectangle.PerimeterCalculator();
      Console.WriteLine($"Area: {rectangle.Area} Perimeter: {rectangle.Perimeter}");

      //3
      var book = new Book(new Author("Rob Miles"), new Title("C# Programming Yellow Book"), new Content("The C# Yellow book is a great way to learn how to program by just reading."));
      book.Show();

      //4
      var point1 = new Point(1, 1, "(1,1)");
      var point2 = new Point(1, 2, "(1,2)");
      var point3 = new Point(3, 1, "(3,1)");
      var point4 = new Point(3, 2, "(3,2)");
      var point5 = new Point(4, -2, "(4,-2)");
      var figure3 = new Figure(point1, point2, point3); 
      Console.WriteLine($"Triangle {figure3} perimeter={figure3.PerimeterCalculator()}");

      var figure4 = new Figure(point1, point2, point3, point4);
      Console.WriteLine($"Rectangle {figure4} perimeter={figure4.PerimeterCalculator()}");

      var figure5 = new Figure(point1, point2, point3, point4, point5);
      Console.WriteLine($"Polygon {figure5} perimeter={figure5.PerimeterCalculator()}");

      //5
      var user = new User("Irina", "Gershun", "girinadev", DateTime.Now);
      Console.WriteLine($"{user.FirstName} {user.LastName}, {user.Login}, {user.CreateDate.ToString("D")}");

      //6
      var converter = new Converter(23.782, 26.170, 0.328);
      var amountToConvert = 100;
      Console.WriteLine($" {amountToConvert}{Currencies.UAH} = {converter.ConvertFromUah(amountToConvert, Currencies.USD)}{Currencies.USD}");
      Console.WriteLine($" {amountToConvert}{Currencies.UAH} = {converter.ConvertFromUah(amountToConvert, Currencies.EUR)}{Currencies.EUR}");
      Console.WriteLine($" {amountToConvert}{Currencies.UAH} = {converter.ConvertFromUah(amountToConvert, Currencies.RUB)}{Currencies.RUB}");
      Console.WriteLine($" {amountToConvert}{Currencies.UAH} = {converter.ConvertFromUah(amountToConvert, Currencies.UAH)}{Currencies.UAH}");

      amountToConvert = 1800;
      Console.WriteLine($" {amountToConvert}{Currencies.USD} = {converter.ConvertToUah(amountToConvert, Currencies.USD)}{Currencies.UAH}");
      Console.WriteLine($" {amountToConvert}{Currencies.EUR} = {converter.ConvertToUah(amountToConvert, Currencies.EUR)}{Currencies.UAH}");
      Console.WriteLine($" {amountToConvert}{Currencies.RUB} = {converter.ConvertToUah(amountToConvert, Currencies.RUB)}{Currencies.UAH}");
      Console.WriteLine($" {amountToConvert}{Currencies.UAH} = {converter.ConvertToUah(amountToConvert, Currencies.UAH)}{Currencies.UAH}");

      //7
      var employee = new Employee("Irina", "Gershun")
      {
        Experience = 7,
        Position = Positions.Deveporer
      };
      var salary = employee.GetSalary(5);
      Console.WriteLine($"Employee: {employee}. Salary: {salary.Amount.ToString("C")}. Salary Fee: {salary.Fee.ToString("C")}");

      //8
      var invoice = new Invoice(12345, "Irina", "SomeProvider", "#7849357", 10);
      invoice.UnitPrice = 5;
      var invoicePrice = invoice.CalculatePrice(false);
      var invoicePriceWithNdc = invoice.CalculatePrice(true);

      Console.WriteLine($"Invoice: {invoice}. Price: {invoicePrice.ToString("C")}. Price with NDS: {invoicePriceWithNdc.ToString("C")}");

      Console.ReadLine();
    }
  }
}
