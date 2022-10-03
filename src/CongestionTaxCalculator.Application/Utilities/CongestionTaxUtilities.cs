using PublicHoliday;

namespace CongestionTaxCalculator.Application.Utilities;

public static class CongestionTaxUtilities
{
  public static List<DateTime> GetPublicHolidaysForYear(int year)
  {
    return new SwedenPublicHoliday().PublicHolidays(year).ToList();
  }
  public static List<DateTime> GetWeekendsForYear(int year)
  {
    var weekends = new List<DateTime>();

    for (var day = DateTime.Parse($"{year}-01-01"); day <= DateTime.Parse($"{year}-12-31"); day = day.AddDays(1))
    {
      if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
      {
        weekends.Add(day);
      }
    }
    return weekends;
  }
  public static List<DateTime> GetDatesInMonthForYear(int year, int month)
  {
    return Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(day => new DateTime(year, month, day)).ToList();
  }
  public static List<DateTime> GetTollFreeDatesForYear(int year)
  {
    var tollFreeDates = new List<DateTime>();

    var publicHolidays = GetPublicHolidaysForYear(year);
    var weekends = GetWeekendsForYear(year);
    var datesInMonth = GetDatesInMonthForYear(year, 7);

    tollFreeDates.AddRange(publicHolidays);
    tollFreeDates.AddRange(weekends);
    tollFreeDates.AddRange(datesInMonth);

    foreach (var date in publicHolidays)
    {
      tollFreeDates.Add(date.AddDays(-1));
    }
    var uniqueDates = tollFreeDates.Select(x => x.Date).Distinct().ToList();
    return uniqueDates;
  }

  public static bool IsTollFreeDate(DateTime dateTime)
  {
    var year = dateTime.Year;

    var tollFreeDatesForYear = GetTollFreeDatesForYear(year);

    return tollFreeDatesForYear.Contains(dateTime);
  }
}