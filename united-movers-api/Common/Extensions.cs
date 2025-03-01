using System.Globalization;

namespace united_movers_api.Common
{
    public static class Extensions
    {
        public static DateTime ToCustomFormattedDate(this DateTime dateTime)
        {
            string formattedDate = dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            return DateTime.ParseExact(formattedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
