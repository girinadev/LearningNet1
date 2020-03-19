using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TaskApp.Delegates;
using TaskApp.Generics;
using TaskApp.TaskAbstraction;
using TaskApp.TaskClasses;
using TaskApp.TaskInheritance;
using TaskApp.TaskPageSaver;
using TaskApp.TaskStatic;
using TaskApp.TaskStruct;

namespace TaskApp
{
  class Program
  {
    static void Main(string[] args)
    {
      //TaskClasses();

      //TaskInheritance();

      //TaskAbstraction();

      //TaskStatic();

      //TaskStruct();

      //TaskDelegat();

      //TaskGenerics();

      TaskPageSaver();

      Console.ReadLine();
    }

    private static void TaskClasses()
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
    }

    private static void TaskInheritance()
    {
      //1

      Printer printer = new LatherPrinter(ConsoleColor.White);
      printer.Print("LatherPrinter. White");
      printer.Color = ConsoleColor.DarkYellow;
      printer.Print("LatherPrinter. DarkYellow");

      printer = new ThreeDPrinter(ConsoleColor.Green);
      printer.Print("ThreeDPrinter. Green");
      printer.Color = ConsoleColor.Blue;
      printer.Print("ThreeDPrinter. Blue");

      //2
      var classRoom1 = new ClassRoom(new ExcelentPupil(), new GoodPupil());
      classRoom1.PrintInfoAboutPupils();

      var classRoom2 = new ClassRoom(new ExcelentPupil(), new GoodPupil(), new BadPupil(), new BadPupil());
      classRoom2.PrintInfoAboutPupils();

      var classRoom3 = new ClassRoom(new ExcelentPupil(), new GoodPupil(), new BadPupil(), new BadPupil(), new ExcelentPupil());
      classRoom3.PrintInfoAboutPupils();

      //3
      var vehicles = new Vehicle[]
      {
        new Plane
        {
          X = 100.44,
          Y = 3434.34,
          Height = 2342734,
          PassengersCount = 39,
          Price = 79000000,
          Speed = 12345,
          YearOfIssue = 2019
        },
        new Ship
        {
          X = 134.44,
          Y = 56.34,
          Port = "Port 1234",
          PassengersCount = 2897,
          Price = 12000000,
          Speed = 567,
          YearOfIssue = 200
        },
        new Саг
        {
          X = 23.44,
          Y = 12,
          Price = 70000,
          Speed = 180,
          YearOfIssue = 1999
        }
      };

      foreach (var v in vehicles)
      {
        Console.WriteLine(v);
      }

      //4
      Console.OutputEncoding = Encoding.UTF8;
      Console.WriteLine("Введите ключ доступа и нажмите Enter:");
      var documentWorkerKey = Console.ReadLine();

      var documentWorker = new DocumentWorker();
      if (!string.IsNullOrEmpty(documentWorkerKey))
      {
        if (documentWorkerKey.Equals("pro", StringComparison.OrdinalIgnoreCase))
        {
          documentWorker = new ProDocumentWorker();
        }
        else if (documentWorkerKey.Equals("exp", StringComparison.OrdinalIgnoreCase))
        {
          documentWorker = new ExpertDocumentWorker();
        }
      }

      documentWorker.OpenDocument();
      documentWorker.EditDocument();
      documentWorker.SaveDocument();
    }

    private static void TaskAbstraction()
    {
      //1
      Console.WriteLine("Input file name:");
      string fileName;
      do
      {
        fileName = Console.ReadLine();
      } while (string.IsNullOrEmpty(fileName));

      var handlerFactory = new HandlerFactory();
      var handler = handlerFactory.GetHandler(fileName);
      handler.Create();
      handler.Open();
      handler.Change();
      handler.Save();

      //2
      IPlayable player = new Player();
      player.Play();
      player.Pause();
      player.Play();
      player.Stop();

      var playerWithRecording = (IRecodable)player;
      playerWithRecording.Record();
      playerWithRecording.Pause();
      playerWithRecording.Record();
      playerWithRecording.Stop();

      player.Play();
      player.Stop();
    }

    private static void TaskStatic()
    {
      //1
      Console.WriteLine("5 + 9 = {0}", Calculator.Add(5, 9));
      Console.WriteLine("5 - 9 = {0}", Calculator.Sub(5, 9));
      Console.WriteLine("5 * 9 = {0}", Calculator.Multiply(5, 9));
      Console.WriteLine("5 / 9 = {0}", Calculator.Divide(5, 9));

      //2
      int[] arr = new int[] { 6, 8, 1, 3, 89, 2, 56, 87, 0, -7, 2, 77, -5 };
      arr.Order();

      PrintToConsole(arr);

      //3
      arr = new int[] { 6, 8, 1, 3, 89, 2, 56, 87, 0, -7, 2, 77, -5 };
      arr.Order(true);
      PrintToConsole(arr);

      arr = new int[] { 6, 8, 1, 3, 89, 2, 56, 87, 0, -7, 2, 77, -5 };
      arr.Order(false);
      PrintToConsole(arr);

      //4
      var s1 = "Happy new year new";
      Console.WriteLine("{0} == {1}", s1.Substring1(6, 3), s1.Substring(6, 3));
      Console.WriteLine("{0} == {1}", s1.IndexOf1("year").ToString(), s1.IndexOf("year").ToString());
      Console.WriteLine("{0} == {1}", s1.Replace1("new", "old"), s1.Replace("new", "old"));
    }

    private static void PrintToConsole(int[] arr)
    {
      foreach (var item in arr)
      {
        Console.WriteLine(item);
      }

      Console.WriteLine();
    }

    private static void TaskStruct()
    {
      //1
      var noteBook = new Notebook("Z500", "Lenovo", 10000M);
      noteBook.Print();

      //2
      var trains = new Train[5];
      for (int i = 0; i < trains.Length; i++)
      {
        string destTitle;
        string trainNumber;
        string departureTime;
        do
        {
          Console.WriteLine("Please train destination title:");
          destTitle = Console.ReadLine();
          Console.WriteLine("Please train number:");
          trainNumber = Console.ReadLine();
          Console.WriteLine("Please train departure time:");
          departureTime = Console.ReadLine();
        } while (string.IsNullOrEmpty(destTitle)
                 || string.IsNullOrEmpty(trainNumber)
                 || string.IsNullOrEmpty(departureTime));

        trains[i] = new Train(destTitle, trainNumber, departureTime);
      }

      Array.Sort<Train>(trains, (t, t1) => t.TrainNumber.CompareTo(t1.TrainNumber));

      Console.WriteLine("Please train input what train number you search?");
      var trainNumberToSearch = Console.ReadLine();
      Train train = Array.Find(trains, t => t.TrainNumber.Equals(trainNumberToSearch));
      if (string.IsNullOrEmpty(train.TrainNumber))
        Console.WriteLine("Train not found");
      else
        Console.WriteLine("Train info: {0}", train.ToString());

      //3
      var myClass = new MyClass();
      myClass.change = "не изменено";
      ClassTaker(myClass);
      Console.WriteLine(myClass.change);

      var myStruct = new MyStruct();
      myStruct.change = "не изменено";
      StruktTaker(myStruct);
      Console.WriteLine(myStruct.change);

      //4
      string birthDayStr;
      DateTime birthDay;
      do
      {
        Console.WriteLine("Please input your birthday in format: MM/dd/yyyy");
        birthDayStr = Console.ReadLine();
      } while (!DateTime.TryParse(birthDayStr, out birthDay));

      Console.WriteLine("To you birthday left: {0}", BirthdayCounter.DaysLeft(birthDay));

      //5
      ConsolePrinter.Print("green", 10);

      //6
      var accauntant = new Accauntant();
      var p = Post.Designer;
      Console.WriteLine("{0}: {1}", p.ToString(), accauntant.AskForBonus(p, 30));
      p = Post.Devepoper;
      Console.WriteLine("{0}: {1}", p.ToString(), accauntant.AskForBonus(p, 190));
      p = Post.QA;
      Console.WriteLine("{0}: {1}", p.ToString(), accauntant.AskForBonus(p, 200));
    }

    private static void ClassTaker(MyClass myClass)
    {
      myClass.change = "изменено";
    }

    private static void StruktTaker(MyStruct myStruct)
    {
      myStruct.change = "изменено";
    }

    delegate int RandomInt();

    private static void TaskDelegat()
    {
      //1
      Func<int, int, int, int> AverаgeSum = delegate (int a, int b, int c) { return a + b + c; };
      Console.WriteLine(AverаgeSum(3, 4, 5));

      //2
      Func<decimal, decimal, decimal> Sum = (a, b) => a + b;
      Func<decimal, decimal, decimal> Sub = (a, b) => a - b;
      Func<decimal, decimal, decimal> Mul = (a, b) => a * b;
      Func<decimal, decimal, decimal> Div = (a, b) => b != 0 ? a / b : 0;

      decimal x = 0;
      decimal y = 0;
      string operation = null;
      string inputStr;
      do
      {
        Console.WriteLine("Please input expression:");
        inputStr = Console.ReadLine();
      } while (!ParseExpression(inputStr, ref x, ref y, ref operation));

      decimal result = 0;
      switch (operation)
      {
        case Operations.Sum: result = Sum(x, y); break;
        case Operations.Sub: result = Sub(x, y); break;
        case Operations.Mul: result = Mul(x, y); break;
        case Operations.Div: result = Div(x, y); break;
      }
      Console.WriteLine($"{inputStr} = {result}");

      //3
      var random = new Random();
      Func<IEnumerable<RandomInt>, long> Average = r => r.Sum(s => s.Invoke());
      var avResult = Average(new List<RandomInt>
      {
        () => random.Next(10000),
        () => random.Next(10000),
        () => random.Next(10000)
      });

      Console.WriteLine($"Average: {avResult}");
    }

    private static bool ParseExpression(string inputStr, ref decimal x, ref decimal y, ref string operation)
    {
      var operations = new[] { Operations.Sum, Operations.Sub, Operations.Mul, Operations.Div };
      foreach (var op in operations)
      {
        try
        {
          var operands = inputStr.Split(op);
          if (operands.Length != 2) continue;

          x = decimal.Parse(operands[0]);
          y = decimal.Parse(operands[1]);
          operation = op;

          return true;
        }
        catch
        {
          // ignored
        }
      }

      return false;
    }

    private static void TaskGenerics()
    {
      //1
      var sc = Factory<SomeClass>.FactoryMethod();
      Console.WriteLine(sc.F1);

      //2
      var list = new MyList<SomeClass>();
      list.Add(new SomeClass("1", "2"));
      list.Add(new SomeClass("3", "4")); //add element
      Console.WriteLine(list[1].ToString());//get by index
      Console.WriteLine(list.Count); //get total count
      foreach (var item in list)
      {
        Console.WriteLine(item.ToString());
      }

      //3
      var myDictionary = new MyDictionary<string, int>();
      myDictionary.Add("one", 1);
      myDictionary.Add("two", 2);
      var count = myDictionary.Count();
      Console.WriteLine(myDictionary["two"]);
      myDictionary["two"] = 2000;
      foreach (var item in myDictionary)
      {
        Console.WriteLine($"{item.Key} =  {item.Value}");
      }

      //4
      var arr = list.GetArray<SomeClass>();
      foreach (var item in arr)
      {
        Console.WriteLine(item.ToString());
      }

      //5
      var carCollection = new CarCollection<Car>();
      carCollection.AddCar(2019, "To");
      for (var i = 0; i < carCollection.Count; i++)//count
      {
        Console.WriteLine(carCollection[i].ToString());//get by index
      }

      carCollection.ClearAllCars();
      Console.WriteLine($"CarCollection count: {carCollection.Count}");

      //6

      //7
      var arrayList = new MyArrayList();
      arrayList.Add(1);
      arrayList.Add(2);
      arrayList.Add("string");
      arrayList.Insert(1, 22);
      var indexOf = arrayList.IndexOf(1);
      arrayList.Remove(22);

      foreach (var item in arrayList)
      {
        Console.WriteLine(item.ToString());
      }
    }

    private static void TaskPageSaver()
    {
      var testDate = new[] {
        "http://selin.in.ua/solvve/text.txt",
        "http://selin.in.ua/solvve/html.html",
        "http://info.cern.ch/hypertext/WWW/TheProject.html"
      };

      foreach (var item in testDate)
      {
        PageSaver.LoadAndSave(item);
      }
    }
  }
}
