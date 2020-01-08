namespace TaskApp.TaskStruct
{
  public struct Train
  {
    private readonly string _destTitle;
    private readonly string _trainNumber;
    private readonly string _departureTime;

    public Train(string destTitle, string trainNumber, string departureTime)
    {
      _destTitle = destTitle;
      _trainNumber = trainNumber;
      _departureTime = departureTime;
    }

    public string TrainNumber => _trainNumber;

    public override string ToString()
    {
      return $"{_trainNumber}, {_departureTime} {_destTitle}";
    }
  }
}
