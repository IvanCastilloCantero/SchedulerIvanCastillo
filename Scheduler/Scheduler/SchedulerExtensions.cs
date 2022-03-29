using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public static class SchedulerExtensions
    {
        public static SchedulerInfo[] CalculateNextExecution(this SchedulerConfiguration scheduler, int times)
        {
            DateTime nextExecutionTime = new DateTime();
            IEnumerable<SchedulerInfo> informations = new List<SchedulerInfo>();
            for (int i = 0; i < times; i++)
            {
                nextExecutionTime = CalculateNextExecutionCheckType(scheduler, i, nextExecutionTime);
                string description = DescriptionManager.CalculateDescription(scheduler, nextExecutionTime); 
                SchedulerInfo information = new SchedulerInfo(nextExecutionTime, description);
                
                informations = informations.Concat(new[] { information });
            }
            return informations.ToArray();
        }

        public static DateTime CalculateNextExecutionCheckType(SchedulerConfiguration scheduler, int i, DateTime nextExecutionTime)
        {
            if (scheduler.Type == ExecutionType.Once)
            {
                nextExecutionTime = CalculateOnceExecution(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringExecution(scheduler, i, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateOnceExecution(SchedulerConfiguration scheduler)
        {
            return scheduler.DateTime;
        }

        private static DateTime CalculateRecurringExecution(SchedulerConfiguration scheduler, int i, DateTime nextExecutionTime)
        {
            ValidateFields(scheduler);
            if (scheduler.Occurs == OccursType.Weekly)
            {
                nextExecutionTime = CalculateRecurringWeekly(scheduler, nextExecutionTime, i);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMonthly(scheduler, nextExecutionTime, i);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMonthly(SchedulerConfiguration scheduler, DateTime nextExecutionTime, int i)
        {
            if (i == 0)
            {
                nextExecutionTime = CalculateRecurringFirstMonthly(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthly(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthly(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.MonthlyConf == MonthlyConfType.Day)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyDay(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyThe(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            if (scheduler.CurrentDate.Day > scheduler.Day)
            {
                nextExecutionTime = nextExecutionTime.AddMonths(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyThe(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            switch (scheduler.OrderDay)
            {
                case Occurrency.First:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirst(scheduler, nextExecutionTime);
                    break;
                case Occurrency.Second:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecond(scheduler, nextExecutionTime);
                    break;
                case Occurrency.Third:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThird(scheduler, nextExecutionTime);
                    break;
                case Occurrency.Forth:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheForth(scheduler, nextExecutionTime);
                    break;
                case Occurrency.Last:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheLast(scheduler, nextExecutionTime);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirst(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.Monday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstMonday(scheduler);
                    break;
                case DayOccurrency.Tuesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstTuesday(scheduler);
                    break;
                case DayOccurrency.Wednesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWednesday(scheduler);
                    break;
                case DayOccurrency.Thursday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstThursday(scheduler);
                    break;
                case DayOccurrency.Friday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstFriday(scheduler);
                    break;
                case DayOccurrency.Saturday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSaturday(scheduler);
                    break;
                case DayOccurrency.Sunday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSunday(scheduler);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekDay(scheduler);
                    break;
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstDay(scheduler);
                    break;
                case DayOccurrency.weekend_day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekendDay(scheduler);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstMonday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstMondaySameMonth(scheduler, nextExecutionTime);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstMondayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstMondaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstMondayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstMondayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstTuesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day <= 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstTuesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstTuesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstTuesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstTuesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstTuesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWednesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWednesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWednesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWednesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWednesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWednesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstThursday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstThursdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstThursdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstThursdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstThursdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstThursdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstFriday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstFridaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstFridayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstFridaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstFridayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstFridayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSaturday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSaturdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSaturdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSaturdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSaturdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSaturdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSunday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSundaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSundayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSundaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 7)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstSundayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstSundayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7 && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekDaySameMonth(nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekDaySameMonth(DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.CurrentDate.Day == 1)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstDaySameMonth(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstDaySameMonth(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7 && (scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday | scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekendDaySameMonth(nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheFirstWeekendDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekendDaySameMonth(DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheFirstWeekendDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecond(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.Monday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondMonday(scheduler);
                    break;
                case DayOccurrency.Tuesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondTuesday(scheduler);
                    break;
                case DayOccurrency.Wednesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWednesday(scheduler);
                    break;
                case DayOccurrency.Thursday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondThursday(scheduler);
                    break;
                case DayOccurrency.Friday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondFriday(scheduler);
                    break;
                case DayOccurrency.Saturday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSaturday(scheduler);
                    break;
                case DayOccurrency.Sunday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSunday(scheduler);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekDay(scheduler);
                    break;
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondDay(scheduler);
                    break;
                case DayOccurrency.weekend_day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekendDay(scheduler);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondMonday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 14)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondMondaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondMondayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondMondaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondMondayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondMondayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondTuesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day <= 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondTuesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondTuesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondTuesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondTuesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondTuesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWednesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWednesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWednesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWednesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWednesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWednesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondThursday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondThursdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondThursdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondThursdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondThursdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondThursdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondFriday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondFridaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondFridayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondFridaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondFridayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondFridayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSaturday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSaturdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSaturdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSaturdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSaturdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSaturdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSunday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSundaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSundayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSundaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondSundayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondSundayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 7 && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekDaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekDaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekDayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.CurrentDate.Day <= 2)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondDaySameMonth(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondDaySameMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime();
            switch (scheduler.CurrentDate.Day)
            {
                case 1:
                    nextExecutionTime = scheduler.CurrentDate.AddDays(1);
                    break;
                case 2:
                    nextExecutionTime = scheduler.CurrentDate;
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 2);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 14 && (scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday | scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekendDaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekendDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekendDaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while ((nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday) | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 14)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSecondWeekendDayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSecondWeekendDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while ((nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday) | nextExecutionTime.Day <= 7)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThird(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.Monday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdMonday(scheduler);
                    break;
                case DayOccurrency.Tuesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdTuesday(scheduler);
                    break;
                case DayOccurrency.Wednesday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWednesday(scheduler);
                    break;
                case DayOccurrency.Thursday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdThursday(scheduler);
                    break;
                case DayOccurrency.Friday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdFriday(scheduler);
                    break;
                case DayOccurrency.Saturday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSaturday(scheduler);
                    break;
                case DayOccurrency.Sunday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSunday(scheduler);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekDay(scheduler);
                    break;
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdDay(scheduler);
                    break;
                case DayOccurrency.weekend_day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekendDay(scheduler);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdMonday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdMondaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdMondayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdMondaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdMondayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdMondayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Monday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdTuesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day <= 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdTuesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdTuesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdTuesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdTuesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdTuesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Tuesday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWednesday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWednesdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWednesdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWednesdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWednesdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWednesdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Wednesday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdThursday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdThursdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdThursdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdThursdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdThursdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdThursdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Thursday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdFriday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdFridaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdFridayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdFridaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdFridayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdFridayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Friday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSaturday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSaturdaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSaturdayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSaturdaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSaturdayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSaturdayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSunday(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSundaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSundayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSundaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdSundayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdSundayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Sunday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 21 && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday && scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekDaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekDaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekDayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.CurrentDate.Day <= 3)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdDaySameMonth(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdDaySameMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime();
            switch (scheduler.CurrentDate.Day)
            {
                case 1:
                    nextExecutionTime = scheduler.CurrentDate.AddDays(2);
                    break;
                case 2:
                    nextExecutionTime = scheduler.CurrentDate.AddDays(1);
                    break;
                case 3:
                    nextExecutionTime = scheduler.CurrentDate;
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 3);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.CurrentDate.Day < 14 && (scheduler.CurrentDate.DayOfWeek != DayOfWeek.Saturday | scheduler.CurrentDate.DayOfWeek != DayOfWeek.Sunday))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekendDaySameMonth(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekendDayOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekendDaySameMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while ((nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday) | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > 21)
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheThirdWeekendDayOtherMonth(scheduler);
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheThirdWeekendDayOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            while ((nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday) | nextExecutionTime.Day <= 14)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheForth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            throw new NotImplementedException();
        }

        private static DateTime CalculateRecurringFirstMonthlyTheLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            throw new NotImplementedException();
        }

        private static DateTime CalculateRecurringMoreMonthly(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.MonthlyConf == MonthlyConfType.Day)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyDay(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyThe(nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreNextDayMonthlyDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayMonthlyDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(scheduler.EveryMonths).Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Minute);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyThe(DateTime nextExecutionTime)
        {
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringWeekly(SchedulerConfiguration scheduler, DateTime nextExecutionTime, int i)
        {
            if (i == 0)
            {
                nextExecutionTime = CalculateRecurringFirstWeekly(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreWeekly(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstWeekly(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            nextExecutionTime = CalculateRecurringFirstCheckHourWeekly(scheduler);
            for (int i = 0; i < 6; i++)
            {
                var nextDay = nextExecutionTime.AddDays(i);
                if (ContainsDayOfWeek(scheduler.DayOfWeeks, nextDay.DayOfWeek))
                {
                    nextExecutionTime = nextDay;
                    i = 6;
                }
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstCheckHourWeekly(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.CurrentDate.Hour < scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.CurrentDate.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else
            {
                nextExecutionTime = scheduler.CurrentDate;
            }
            return nextExecutionTime;
        }

        private static bool ContainsDayOfWeek(List<DayOfWeek> daysSelected, DayOfWeek nextDay)
        {
            return daysSelected.Where(d => d == nextDay).Count() > 0;
        }

        private static DateTime CalculateRecurringMoreWeekly(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreNextDayWeekly(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayWeekly(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            for (int i = 1; i < 7; i++)
            {
                var nextDay = nextExecutionTime.AddDays(i);
                if (ContainsDayOfWeek(scheduler.DayOfWeeks, nextDay.DayOfWeek))
                {
                    nextExecutionTime = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
                    i = 7;
                }
            }
            nextExecutionTime = CalculateRecurringMoreNextDayCheckWeek(scheduler, nextExecutionTime);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayCheckWeek(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.DayOfWeek == scheduler.DayOfWeeks.First())
            {
                nextExecutionTime = nextExecutionTime.AddDays((scheduler.EveryWeeks - 1) * 7);
            }
            return nextExecutionTime;
        }

        private static void ValidateFields(SchedulerConfiguration scheduler)
        {
            if (scheduler.DayOfWeeks != null)
            {
                ValidateDayOfWeeks(scheduler);
            }
            ValidateCurrentDate(scheduler);
        }

        private static void ValidateDayOfWeeks(SchedulerConfiguration scheduler)
        {
            if (scheduler.DayOfWeeks.Count() == 0)
            {
                throw new Exception("Selecciona algun dia de la semana");
            }
        }

        private static void ValidateCurrentDate(SchedulerConfiguration scheduler)
        {
            try
            {
                scheduler.CurrentDate.AddDays(scheduler.Days);
            }
            catch (Exception)
            {
                throw new Exception("La Fecha introducida es demasiado grande");
            }
        }
    }
}
