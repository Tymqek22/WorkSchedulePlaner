using System.Globalization;

namespace WorkSchedulePlaner.Web.Helpers
{
	public static class DateHelper
	{
		public static List<DateTime> GetWeekDates(int weekOffset)
		{
			DateTime startDay = DateTime.Today.AddDays(
				(int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
				(int)DateTime.Today.DayOfWeek)
				.AddDays(7 * weekOffset);

			var dates = Enumerable
				.Range(0,7)
				.Select(i => startDay.AddDays(i))
				.ToList();

			return dates;
		}
	}
}
