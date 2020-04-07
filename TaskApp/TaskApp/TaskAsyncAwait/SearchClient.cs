using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskApp.TaskAsyncAwait
{
  public class SearchClient
  {
    private readonly Dictionary<SearchEngine, string> _searchEngineUrls;

    private string EngineUrl(SearchEngine engine, string query) => string.Format(_searchEngineUrls[engine], query);

    public SearchClient()
    {
      _searchEngineUrls = new Dictionary<SearchEngine, string>
      {
        { SearchEngine.Google, "https://www.google.com/search?q={0}" },
        { SearchEngine.Bing, "https://www.bing.com/search?q={0}" },
        { SearchEngine.Yandex, "https://yandex.ru/search/?text={0}" }
      };
    }

    public Task<string> SearchAsync(SearchEngine engine, string query)
    {
      TestSleep(engine);

      var httpClient = new HttpClient();
      return httpClient.GetStringAsync(EngineUrl(engine, query));

    }

    public string Search(SearchEngine engine, string query)
    {
      TestSleep(engine);

      HttpWebRequest request = WebRequest.CreateHttp(EngineUrl(engine, query));
      request.Method = "GET";

      using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
      {
        using (Stream responseStream = response.GetResponseStream())
        {
          using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.UTF8))
          {
            return myStreamReader.ReadToEnd();
          }
        }
      }
    }

    private void TestSleep(SearchEngine engine)
    {
      switch (engine)
      {
        case SearchEngine.Google: Thread.Sleep(5 * 1000); break;
        case SearchEngine.Bing: Thread.Sleep(25 * 1000); break;
        case SearchEngine.Yandex: Thread.Sleep(10 * 1000); break;
      }
    }
  }
}
