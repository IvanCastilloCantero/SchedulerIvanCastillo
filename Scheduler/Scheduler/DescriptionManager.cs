using System;
using System.Linq;

namespace Scheduler
{
    public static class DescriptionManager
    {
        public static string CalculateDescription(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.Type == ExecutionType.Once)
            {
                return CalculateDescriptionOnce(scheduler, nextExecutionTime);
            }
            else if (scheduler.Occurs == OccursType.Weekly)
            {
                return CalculateDescriptionRecurringWeekly(scheduler);
            }
            else
            {
                return CalculateDescriptionRecurringMonthly(scheduler);
            }
        }

        public static string CalculateDescriptionRecurringMonthly(SchedulerConfiguration scheduler)
        {
            string description;
            if (scheduler.MonthlyConf == MonthlyConfType.Day)
            {
                description = CalculateDescriptionRecurringMonthlyDay(scheduler);
            } else
            {
                description = CalculateDescriptionRecurringMonthlyThe(scheduler);
            }
            return description;
        }

        private static string CalculateDescriptionRecurringMonthlyThe(SchedulerConfiguration scheduler)
        {
            string description; 
            if (scheduler.OccursEvery > 1)
            {
                 description = "Occurs the " + scheduler.OrderDay.ToString().ToLower()
                    + " " + scheduler.OccursDay.ToString().ToLower()
                    + " of every " + scheduler.Frequency
                    + " months every " + scheduler.OccursEvery.ToString()
                    + " hours between " + scheduler.StartingAt.ToShortTimeString()
                    + " and " + scheduler.EndingAt.ToShortTimeString()
                    + " starting on " + scheduler.StartDate.ToShortDateString();
            } 
            else
            {
                description = "Occurs the " + scheduler.OrderDay.ToString().ToLower()
                    + " " + scheduler.OccursDay.ToString().ToLower()
                    + " of every " + scheduler.Frequency
                    + " months every " + scheduler.OccursEvery.ToString()
                    + " hour between " + scheduler.StartingAt.ToShortTimeString()
                    + " and " + scheduler.EndingAt.ToShortTimeString()
                    + " starting on " + scheduler.StartDate.ToShortDateString();
            }
            return description;
        }

        private static string CalculateDescriptionRecurringMonthlyDay(SchedulerConfiguration scheduler)
        {
            string description = "Occurs day " + scheduler.Day.ToString()
                + " every " + scheduler.Frequency
                + " months every " + scheduler.OccursEvery.ToString()
                + " " + scheduler.UnitTime.ToString().ToLower()
                + " between " + scheduler.StartingAt.ToShortTimeString() 
                + " and " + scheduler.EndingAt.ToShortTimeString() 
                + " starting on " + scheduler.StartDate.ToShortDateString();
            return description;
        }

        public static string CalculateDescriptionOnce(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            string description = "Occurs " + scheduler.Type + ". Schedule will be used on " + nextExecutionTime.ToShortDateString() + " at " + nextExecutionTime.ToShortTimeString() + " starting on " + scheduler.StartDate.ToShortDateString();
            return description;
        }

        public static string CalculateDescriptionRecurringWeekly(SchedulerConfiguration scheduler)
        {
            string days = string.Empty;
            foreach (DayOfWeek dayOfWeek in scheduler.DayOfWeeks)
            {
                days = CalculateDescripcionRecurringWeeklyCheck(scheduler, days, dayOfWeek);
            }
            return CalculateDescriptionRecurringWeeklyMessage(scheduler, days);
        }

        public static string CalculateDescripcionRecurringWeeklyCheck(SchedulerConfiguration scheduler, string days, DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == scheduler.DayOfWeeks.First())
            {
                days = "on " + dayOfWeek.ToString().ToLower();
            }
            else if (dayOfWeek == scheduler.DayOfWeeks.Last())
            {
                days = CalculateDescriptionRecurringWeeklyLastDay(days, dayOfWeek);
            }
            else
            {
                days = days + ", " + dayOfWeek.ToString().ToLower();
            }
            return days;
        }

        public static string CalculateDescriptionRecurringWeeklyLastDay
            
            
            (string days, DayOfWeek dayOfWeek)
        {
            if (!string.IsNullOrEmpty(days))
            {
                days = days + " and " + dayOfWeek.ToString().ToLower();
            }
            else
            {
                days = "on " + dayOfWeek.ToString().ToLower();
            }
            return days;
        }

        public static string CalculateDescriptionRecurringWeeklyMessage(SchedulerConfiguration scheduler, string days)
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
