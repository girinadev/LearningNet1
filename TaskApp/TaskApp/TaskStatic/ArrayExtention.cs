namespace TaskApp.TaskStatic
{
  public static class ArrayExtention
  {
    public static void Order(this int[] arr)
    {
      SortAsc(arr, 0, arr.Length - 1);
    }

    public static void Order(this int[] arr, bool isAsc)
    {
      if (isAsc)
        SortAsc(arr, 0, arr.Length - 1);
      else
        SortDesc(arr, 0, arr.Length - 1);
    }

    private static void SortAsc(int[] arr, int low, int high)
    {
      if (arr == null || arr.Length == 0)
        return;
      if (low >= high)
        return;

      int middle = low + (high - low) / 2;
      int pivot = arr[middle];

      int i = low, j = high;
      while (i <= j)
      {
        while (arr[i] < pivot)
        {
          i++;
        }
        while (arr[j] > pivot)
        {
          j--;
        }
        if (i <= j)
        {
          int temp = arr[i];
          arr[i] = arr[j];
          arr[j] = temp;
          i++;
          j--;
        }
      }
      if (low < j)
        SortAsc(arr, low, j);
      if (high > i)
        SortAsc(arr, i, high);
    }

    private static void SortDesc(int[] arr, int low, int high)
    {
      if (arr == null || arr.Length == 0)
        return;
      if (low >= high)
        return;

      int middle = low + (high - low) / 2;
      int pivot = arr[middle];

      int i = low, j = high;
      while (i <= j)
      {
        while (arr[i] > pivot)
        {
          i++;
        }
        while (arr[j] < pivot)
        {
          j--;
        }
        if (i <= j)
        {
          int temp = arr[j];
          arr[j] = arr[i];
          arr[i] = temp;
          i++;
          j--;
        }
      }
      if (low < j)
        SortDesc(arr, low, j);
      if (high > i)
        SortDesc(arr, i, high);
    }
  }
}
