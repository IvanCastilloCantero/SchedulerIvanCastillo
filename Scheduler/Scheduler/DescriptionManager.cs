using System;
using System.Linq;

namespace Scheduler
{
    public static class DescriptionManager
    {
        public static string CalculateDescription(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
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
            return scheduler.OccursEvery > 1
                ? "Occurs the " + scheduler.OrderDay.ToString().ToLower()
                   + " " + scheduler.OccursDay.ToString().ToLower()
                   + " of every " + scheduler.Frequency
                   + " months every " + scheduler.OccursEvery.ToString()
                   + " hours between " + scheduler.StartingAt.ToShortTimeString()
                   + " and " + scheduler.EndingAt.ToShortTimeString()
                   + " starting on " + scheduler.StartDate.ToShortDateString()
                : "Occurs the " + scheduler.OrderDay.ToString().ToLower()
                    + " " + scheduler.OccursDay.ToString().ToLower()
                    + " of every " + scheduler.Frequency
                    + " months every " + scheduler.OccursEvery.ToString()
                    + " hour between " + scheduler.StartingAt.ToShortTimeString()
                    + " and " + scheduler.EndingAt.ToShortTimeString()
                    + " starting on " + scheduler.StartDate.ToShortDateString();
        }

        private static string CalculateDescriptionRecurringMonthlyDay(SchedulerConfiguration scheduler)
        {
            return "Occurs day " + scheduler.Day.ToString()
                + " every " + scheduler.Frequency
                + " months every " + scheduler.OccursEvery.ToString()
                + " " + scheduler.UnitTime.ToString().ToLower()
                + " between " + scheduler.StartingAt.ToShortTimeString()
                + " and " + scheduler.EndingAt.ToShortTimeString()
                + " starting on " + scheduler.StartDate.ToShortDateString();
        }

        private static string CalculateDescriptionOnce(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            return "Occurs " + scheduler.Type 
                + ". Schedule will be used on " + nextExecutionTime.ToShortDateString() 
                + " at " + nextExecutionTime.ToShortTimeString() 
                + " starting on " + scheduler.StartDate.ToShortDateString();
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
            if (dayOfWeek == scheduler.DayOfWeeks.First())
            {
                days = "on " + dayOfWeek.ToString().ToLower();
            }
            else if (dayOfWeek == scheduler.DayOfWeeks.Last())
            {
                days = days + " and " + dayOfWeek.ToString().ToLower();
            }
            else
            {
                days = days + ", " + dayOfWeek.ToString().ToLower();
            }
            return days;
        }

        private static string CalculateDescriptionRecurringWeeklyMessage(SchedulerConfiguration scheduler, string days)
        {
            string description = "Occurs every " + scheduler.Frequency.ToString()
                + " weeks " + days + " every "
                + scheduler.OccursEvery + " "
                + scheduler.UnitTime.ToString().ToLower() + " between "
                + scheduler.StartingAt.ToShortTimeString()
                + " and " + scheduler.EndingAt.ToShortTimeString()
                + " starting on " + scheduler.CurrentDate.ToShortDateString();
            return description;
        }
    }
}
