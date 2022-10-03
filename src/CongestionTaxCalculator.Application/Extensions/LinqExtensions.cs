namespace CongestionTaxCalculator.Application.Extensions;

public static class LinqExtensions
{
  public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this List<T> source, Func<T, T, bool> continueSelector)
  {
    using var enumerator = source.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var currentGroup = new List<T> { enumerator.Current };
    while (enumerator.MoveNext())
    {
      var current = enumerator.Current;

      if (continueSelector(currentGroup.First(), current))
      {
        currentGroup.Add(current);
      }
      else
      {
        yield return currentGroup;
        currentGroup = new List<T> { current };
      }
    }

    yield return currentGroup;
  }
}
