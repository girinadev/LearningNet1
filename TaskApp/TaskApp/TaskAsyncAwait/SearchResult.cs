namespace TaskApp.TaskAsyncAwait
{
  public class SearchResult
  {
    public SearchEngine Engine { get; set; }
    public string Result { get; set; }

    public SearchResult(SearchEngine engine, string result)
    {
      Engine = engine;
      Result = result;
    }    
  }
}
