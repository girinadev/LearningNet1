using System.Text;

namespace TaskApp.TaskStatic
{
  public static class StringExtention
  {
    public static string Substring1(this string s, int start, int? length = null)
    {
      if (s == null) return s;

      var end = (length.HasValue ? start + length.Value : s.Length);
      if (end > s.Length)
        end = s.Length;

      var builder = new StringBuilder();
      for (int i = start; i < end; i++)
      {
        builder.Append(s[i]);
      }

      return builder.ToString();
    }

    public static int IndexOf1(this string s, string value)
    {
      if (s == null) return -1;

      for (int i = 0; i < s.Length; i++)
      {
        if (s[i].Equals(value[0]) && value.Equals(s.Substring1(i, value.Length)))
          return i;
      }

      return -1;
    }

    public static string Replace1(this string s, string source, string dest)
    {
      if (s == null) return s;

      var builder = new StringBuilder();

      var start = s.IndexOf1(source);
      if (start < 0)
        return s;

      builder.Append(s.Substring1(0, start));
      builder.Append(dest);
      builder.Append(s.Substring1(start + source.Length));

      return builder.ToString().Replace1(source, dest);
    }
  }
}
