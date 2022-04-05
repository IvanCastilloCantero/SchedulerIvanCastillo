using System;
using System.Globalization;
using System.Linq;

namespace Scheduler
{
    public static class DescriptionManager
    {
        public static string CalculateDescription(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            StringsResources stringsResources = new StringsResources(scheduler.CultureInfo);
            return scheduler.Type == ExecutionType.Once
                ? CalculateDescriptionOnce(scheduler, nextExecutionTime)
                : CalculateDescriptionRecurring(scheduler);

        }

        public static void CheckDateFormat(SchedulerConfiguration scheduler)
        {
            if (scheduler.CultureInfo == new CultureInfo("en-US"))
            {
                
            }
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
                scheduler.OrderDay.ToString().ToLower(),
                scheduler.OccursDay.ToString().ToLower(),
                scheduler.Frequency,
                scheduler.OccursEvery.ToString(),
                scheduler.UnitTime.ToString().ToLower(),
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.StartDate.ToShortDateString()
                );
        }

        private static string CalculateDescriptionRecurringMonthlyDay(SchedulerConfiguration scheduler)
        {
            return String.Format(
                StringsResources.GetResource("RecurringMonthlyDay"),
                scheduler.Day.ToString(),
                scheduler.Frequency,
                scheduler.OccursEvery.ToString(),
                scheduler.UnitTime.ToString().ToLower(),
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.StartDate.ToShortDateString()
                );
        }

        private static string CalculateDescriptionOnce(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            return String.Format(
                StringsResources.GetResource("Once"),
                scheduler.Type,
                nextExecutionTime.ToShortDateString(),
                nextExecutionTime.ToShortTimeString(),
                scheduler.StartDate.ToShortDateString()
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
                ? String.Format("on {0}", dayOfWeek.ToString().ToLower())
                : CalculateDescripcionRecurringWeeklyCheckLast(scheduler, days, dayOfWeek);
        }

        private static string CalculateDescripcionRecurringWeeklyCheckLast(SchedulerConfiguration scheduler, string days, DayOfWeek dayOfWeek)
        {
            return dayOfWeek == scheduler.DayOfWeeks.Last()
                ? String.Format("{0} and {1}", days, dayOfWeek.ToString().ToLower())
                : String.Format("{0}, {1}", days, dayOfWeek.ToString().ToLower());
        }

        private static string CalculateDescriptionRecurringWeeklyMessage(SchedulerConfiguration scheduler, string days)
        {
            return String.Format(
                StringsResources.GetResource("RecurringWeekly"),
                scheduler.Frequency,
                days,
                scheduler.OccursEvery,
                scheduler.UnitTime.ToString().ToLower(),
                scheduler.StartingAt.ToShortTimeString(),
                scheduler.EndingAt.ToShortTimeString(),
                scheduler.CurrentDate.ToShortDateString()
                );
        }
    }
}
