using System;

namespace TaskApp.TaskStruct
{
  public struct Notebook
  {
    private readonly string _model;
    private readonly string _producer;
    private readonly decimal _price;

    public Notebook(string model, string producer, decimal price)
    {
      _model = model;
      _producer = producer;
      _price = price;
    }

    public void Print()
    {
      Console.WriteLine($"Model: {_model}, Producer: {_producer}, Price: {_price}");
    }
  }
}
