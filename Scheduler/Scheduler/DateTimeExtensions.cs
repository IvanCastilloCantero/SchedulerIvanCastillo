using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public static class DateTimeExtensions
    {
        public static DateTime LastDayOfTheMonth(this DateTime dateTime)
        {
            DateTime firstDayNextMonth = new DateTime(dateTime.Year, dateTime.AddMonths(1).Month, 1);
            var daysLeftToTheLastDayOfTheMonth = firstDayNextMonth.AddDays(-1).Day - dateTime.Day;
            dateTime = dateTime.AddDays(daysLeftToTheLastDayOfTheMonth);
            return dateTime;
        }
    }
}
