using System;
using System.Globalization;
using System.Linq;

namespace Scheduler
{
    public static class DescriptionManager
    {
        public static string CalculateDescription(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            StringsResources.InicializateResouces(scheduler.CultureInfo);
            return scheduler.Type == ExecutionType.Once
                ? CalculateDescriptionOnce(scheduler, nextExecutionTime)
                : CalculateDescriptionRecurring(scheduler);

        }

        private static string CalculateDescriptionRecurring(SchedulerConfiguration scheduler)
        {
            return scheduler.Occurs == OccursType.Weekly
                ? CalculateDescriptionRecurringWeekly(scheduler)
                : CalculateDescriptionRecurringMonthly(scheduler);
        }

        private static string CalculateDescriptionRecurringMonthly(SchedulerConfiguration scheduler)
        {
            return scheduler.MonthlyConf == MonthlyConfType.Day
                ? CalculateDescriptionRecurringMonthlyDay(scheduler)
                : CalculateDescriptionRecurringMonthlyThe(scheduler);
        }

        private static string CalculateDescriptionRecurringMonthlyThe(SchedulerConfiguration scheduler)
        {
            return string.Format(
                StringsResources.GetResource("RecurringMonthlyThe"),
                StringsResources.OccurrencyResources[scheduler.OrderDay],
                StringsResources.DayOccurrencyResources[scheduler.OccursDay],
                scheduler.Frequency,
                scheduler.OccursEvery.ToString(),
                StringsResources.UnitTimeResources[scheduler.UnitTime],
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.StartDate.ToString(scheduler.CultureInfo.DateTimeFormat.ShortDatePattern)
                );
        }

        private static string CalculateDescriptionRecurringMonthlyDay(SchedulerConfiguration scheduler)
        {
            return String.Format(
                StringsResources.GetResource("RecurringMonthlyDay"),
                scheduler.Day.ToString(),
                scheduler.Frequency,
                scheduler.OccursEvery.ToString(),
                StringsResources.UnitTimeResources[scheduler.UnitTime],
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.StartDate.ToString(scheduler.CultureInfo.DateTimeFormat.ShortDatePattern)
                );
        }

        private static string CalculateDescriptionOnce(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            return String.Format(
                StringsResources.GetResource("Once"),
                StringsResources.ExecutionTypeResources[scheduler.Type],
                nextExecutionTime.ToString(scheduler.CultureInfo.DateTimeFormat.ShortDatePattern),
                nextExecutionTime.ToShortTimeString(),
                scheduler.StartDate.ToString(scheduler.CultureInfo.DateTimeFormat.ShortDatePattern)
                );
        }

        private static string CalculateDescriptionRecurringWeekly(SchedulerConfiguration scheduler)
        {
            string days = string.Empty;
            foreach (var dayOfWeek in scheduler.DayOfWeeks)
            {
                days = CalculateDescripcionRecurringWeeklyCheck(scheduler, days, dayOfWeek);
            }
            return CalculateDescriptionRecurringWeeklyMessage(scheduler, days);
        }

        private static string CalculateDescripcionRecurringWeeklyCheck(SchedulerConfiguration scheduler, string days, DayOfWeek dayOfWeek)
        {
            return dayOfWeek == scheduler.DayOfWeeks.First()
                ? String.Format("{0} {1}",StringsResources.GetResource("on") ,StringsResources.DayofWeekResources[dayOfWeek])
                : CalculateDescripcionRecurringWeeklyCheckLast(scheduler, days, dayOfWeek);
        }

        private static string CalculateDescripcionRecurringWeeklyCheckLast(SchedulerConfiguration scheduler, string days, DayOfWeek dayOfWeek)
        {
            return dayOfWeek == scheduler.DayOfWeeks.Last()
                ? String.Format("{0} {1} {2}", days, StringsResources.GetResource("and"), StringsResources.DayofWeekResources[dayOfWeek])
                : String.Format("{0}, {1}", days, StringsResources.DayofWeekResources[dayOfWeek]);
        }

        private static string CalculateDescriptionRecurringWeeklyMessage(SchedulerConfiguration scheduler, string days)
        {
            return String.Format(
                StringsResources.GetResource("RecurringWeekly"),
                scheduler.Frequency,
                days,
                scheduler.OccursEvery,
                StringsResources.UnitTimeResources[scheduler.UnitTime],
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.CurrentDate.ToString(scheduler.CultureInfo.DateTimeFormat.ShortDatePattern)
                );
        }
    }
}
