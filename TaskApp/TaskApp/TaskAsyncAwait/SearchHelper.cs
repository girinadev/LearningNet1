using System;
using System.Threading.Tasks;

namespace TaskApp.TaskAsyncAwait
{
  class SearchHelper
  {
    #region Async

    public static async Task<SearchResult[]> SearchAsync(string query)
    {
      var googleTask = SearchAsync(SearchEngine.Google, query);
      var bingTask = SearchAsync(SearchEngine.Bing, query);
      var yandexTask = SearchAsync(SearchEngine.Yandex, query);

      await Task.WhenAll(googleTask, bingTask, yandexTask);

      return new SearchResult[] { await googleTask, await bingTask, await yandexTask };
    }

    private static async Task<SearchResult> SearchAsync(SearchEngine engine, string query)
    {
      var searchClient = new SearchClient();

      string searchResult = string.Empty;
      try
      {
        Console.WriteLine($"Start search '{query}' in {engine}.");
        searchResult = await searchClient.SearchAsync(engine, query);

        Console.WriteLine($"Finished search '{query}' in {engine}.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return new SearchResult(engine, searchResult);
    }

    #endregion

    #region Sync

    public static SearchResult[] Search(string query)
    {
      var googleResult = Search(SearchEngine.Google, query);
      var bingResult = Search(SearchEngine.Bing, query);
      var yandexResult = Search(SearchEngine.Yandex, query);

      return new SearchResult[] { googleResult, bingResult, yandexResult };
    }

    private static SearchResult Search(SearchEngine engine, string query)
    {
      var searchClient = new SearchClient();

      string searchResult = string.Empty;
      try
      {
        Console.WriteLine($"Start search '{query}' in {engine}.");
        searchResult = searchClient.Search(engine, query);

        Console.WriteLine($"Finished search '{query}' in {engine}.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return new SearchResult(engine, searchResult);
    }

    #endregion
  }
}
