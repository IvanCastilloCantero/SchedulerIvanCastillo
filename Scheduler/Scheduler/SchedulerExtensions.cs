using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public static class SchedulerExtensions
    {
        public static SchedulerInfo[] CalculateNextExecution(this SchedulerConfiguration scheduler, int times)
        {
            return scheduler.Type == ExecutionType.Once
                ? CalculateNextExecutionOnce(scheduler)
                : CalculateNextExecutionRecurring(scheduler, times);
        }

        private static SchedulerInfo[] CalculateNextExecutionRecurring(SchedulerConfiguration scheduler, int times)
        {
            DateTime nextExecutionTime = new DateTime();
            IEnumerable<SchedulerInfo> informations = new List<SchedulerInfo>();
            for (int orden = 0; orden < times; orden++)
            {
                nextExecutionTime = CalculateNextExecutionCheckType(scheduler, orden, nextExecutionTime);
                string description = DescriptionManager.CalculateDescription(scheduler, nextExecutionTime);
                SchedulerInfo information = new SchedulerInfo(nextExecutionTime, description);

                informations = informations.Concat(new[] { information });
            }
            return informations.ToArray();
        }

        private static SchedulerInfo[] CalculateNextExecutionOnce(SchedulerConfiguration scheduler)
        {
            IEnumerable<SchedulerInfo> informations = new List<SchedulerInfo>();
            DateTime nextExecutionTime = CalculateOnceExecution(scheduler);
            string description = DescriptionManager.CalculateDescription(scheduler, nextExecutionTime);
            SchedulerInfo information = new SchedulerInfo(nextExecutionTime, description);

            return informations.Concat(new[] { information }).ToArray();
        }

        public static DateTime CalculateNextExecutionCheckType(SchedulerConfiguration scheduler, int orden, DateTime currentDate)
        {            
            return CalculateRecurringExecution(scheduler, orden, currentDate);            
        }

        private static DateTime CalculateOnceExecution(SchedulerConfiguration scheduler)
        {
            return scheduler.DateTime;
        }

        private static DateTime CalculateRecurringExecution(SchedulerConfiguration scheduler, int ordna, DateTime nextExecutionTime)
        {
            ValidateFields(scheduler);
            return scheduler.Occurs == OccursType.Weekly
                ? CalculateRecurringWeekly(scheduler, nextExecutionTime, ordna)
                : CalculateRecurringMonthly(scheduler, nextExecutionTime, ordna);
        }

        private static DateTime CalculateRecurringMonthly(SchedulerConfiguration scheduler, DateTime nextExecutionTime, int orden)
        {
            return orden == 0 
                ? CalculateRecurringFirstMonthly(scheduler) 
                : CalculateRecurringMoreMonthly(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthly(SchedulerConfiguration scheduler)
        {
            return scheduler.MonthlyConf == MonthlyConfType.Day
                ? CalculateRecurringFirstMonthlyDay(scheduler)
                : CalculateRecurringFirstMonthlyThe(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyDay(SchedulerConfiguration scheduler)
        {
            var nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            if (scheduler.CurrentDate.Day > (int)scheduler.OrderDay + 1)
            {
                nextExecutionTime = nextExecutionTime.AddMonths(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyThe(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.CurrentDate.Day < (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonth(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonth(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if ((int)scheduler.OccursDay < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDay(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthNotEspecificDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthNotEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.OccursDay == DayOccurrency.day)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthDay(scheduler);
            }
            else if (scheduler.OccursDay == DayOccurrency.weekday)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDay(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthDayNotLast(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMontlhyTheSameMonthDayLast(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayNotLast(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (nextExecutionTime.Day < (int)scheduler.OrderDay + 1)
            {
                var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else if (nextExecutionTime.Day == (int)scheduler.OrderDay + 1)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMontlhyTheSameMonthDayLast(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (nextExecutionTime < nextExecutionTime.LastDayOfTheMonth())
            {
                nextExecutionTime = new DateTime(nextExecutionTime.LastDayOfTheMonth().Year, nextExecutionTime.LastDayOfTheMonth().Month, nextExecutionTime.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.Hour <= scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else if (nextExecutionTime.Hour >= scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime weekDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                weekDay = CalculateRecurringFirstMonthlyTheSameMonthWeekDayNotLast(scheduler, weekDay);
            } 
            else
            {
                weekDay = CalculateRecurringFirstMonthlyTheSameMonthWeekDayLast(weekDay);
            }
            DateTime nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(scheduler, weekDay);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayLast(DateTime weekDay)
        {
            weekDay = weekDay.LastDayOfTheMonth();
            while (weekDay.DayOfWeek == DayOfWeek.Saturday | weekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDay = weekDay.AddDays(-1);
            }
            return weekDay;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayNotLast(SchedulerConfiguration scheduler, DateTime weekDay)
        {
            weekDay = weekDay.AddDays(7 * (int)scheduler.OrderDay);
            while (weekDay.DayOfWeek == DayOfWeek.Saturday | weekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDay = weekDay.AddDays(1);
            }
            return weekDay;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(SchedulerConfiguration scheduler, DateTime weekDay)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (weekDay.Day > nextExecutionTime.Day)
            {
                nextExecutionTime = weekDay;
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(scheduler, nextExecutionTime);
            }
            else if (weekDay.Day == nextExecutionTime.Day)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.Hour <= scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else if (nextExecutionTime.Hour >= scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekDayNotLast(scheduler, nextExecutionTime);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekDayLast(scheduler, nextExecutionTime);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            DateTime weekendDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                weekendDay = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayNotLast(scheduler, weekendDay);
                
            }
            else
            {
                weekendDay = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayLast(weekendDay);
            }
            nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(scheduler, weekendDay);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayLast(DateTime weekendDay)
        {
            weekendDay = weekendDay.LastDayOfTheMonth();
            while (weekendDay.DayOfWeek != DayOfWeek.Saturday && weekendDay.DayOfWeek != DayOfWeek.Sunday)
            {
                weekendDay = weekendDay.AddDays(-1);
            }
            return weekendDay;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayNotLast(SchedulerConfiguration scheduler, DateTime weekendDay)
        {
            weekendDay = weekendDay.AddDays(7 * (int)scheduler.OrderDay);
            while (weekendDay.DayOfWeek != DayOfWeek.Saturday && weekendDay.DayOfWeek != DayOfWeek.Sunday)
            {
                weekendDay = weekendDay.AddDays(1);
            }
            return weekendDay;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(SchedulerConfiguration scheduler, DateTime weekendDay)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (weekendDay.Day > nextExecutionTime.Day)
            {
                nextExecutionTime = weekendDay;
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(scheduler, nextExecutionTime);
            }
            else if (weekendDay.Day == nextExecutionTime.Day)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(scheduler, nextExecutionTime);

            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.Hour <= scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            else if (nextExecutionTime.Hour >= scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayNotLast(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayLast(nextExecutionTime);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayLast(DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDayNotLast(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLast(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if ((int)nextExecutionTime.DayOfWeek == (int)scheduler.OccursDay && nextExecutionTime.Day > (nextExecutionTime.LastDayOfTheMonth().Day - 7))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDay(scheduler, nextExecutionTime);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastOtherDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastOtherDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            DateTime day = nextExecutionTime.LastDayOfTheMonth();
            while ((int)day.DayOfWeek != (int)scheduler.OccursDay)
            {
                day = day.AddDays(-1);
                if (day.Day < nextExecutionTime.Day)
                {
                    day = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler);
                }
            }
            nextExecutionTime = new DateTime(day.Year, day.Month, day.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.Hour < scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            } 
            else if (nextExecutionTime.Hour > scheduler.StartingAt.Hour && nextExecutionTime.Hour < scheduler.EndingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, nextExecutionTime.Hour, nextExecutionTime.Minute, nextExecutionTime.Second);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            if ((int)nextExecutionTime.DayOfWeek == (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDaySameDay(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthEspecificDayOtherDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDaySameDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (nextExecutionTime.Hour < scheduler.StartingAt.Hour)
            {
                nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayOtherDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
                if (nextExecutionTime.Day > (((int)scheduler.OrderDay + 1) * 7))
                {
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler);
                }
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonth(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if ((int)scheduler.OccursDay < 7)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthNotEspecificDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthNotEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            if (scheduler.OccursDay == DayOccurrency.day)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler);
            }
            else if (scheduler.OccursDay == DayOccurrency.weekday)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDay(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(scheduler);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDayNotLast(scheduler, nextExecutionTime);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDayLast(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.LastDayOfTheMonth().Year, nextExecutionTime.LastDayOfTheMonth().Month, nextExecutionTime.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayNotLast(scheduler, nextExecutionTime);  
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayLast(scheduler, nextExecutionTime);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthly(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.MonthlyConf == MonthlyConfType.Day)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyDay(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyThe(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreNextDayMonthlyDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayMonthlyDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Minute); 
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyThe(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if ((int)scheduler.OccursDay < 7)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyEspecificDay(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoretMonthlyNotEspecificDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyEspecificDayNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyEspecificDayNotLast(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyEspecificDayLast(scheduler, nextExecutionTime);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoretMonthlyNotEspecificDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.OccursDay == DayOccurrency.day)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyTheDay(scheduler, nextExecutionTime);
            }
            else if (scheduler.OccursDay == DayOccurrency.weekday)
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyWeekDay(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyWeekendDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyWeekendDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekendDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekendDayMonthlyNextDayNotLast(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekendDayMonthlyNextDayLast(nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDayLast(DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyWeekDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekDayMonthlyNextDayNotLast(scheduler, nextExecutionTime);
            }
            else
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekDayMonthlyNextDayLast(nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDayLast(DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday || nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday || nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyTheDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreTheDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            if (scheduler.OrderDay != Occurrency.Last)
            {
                nextExecutionTime = CalculateRecurringMoreTheDayMonthlyNextDayNotLast(scheduler, nextExecutionTime);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringMoreTheDayMonthlyNextDayLast(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDayLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.LastDayOfTheMonth().Year, nextExecutionTime.LastDayOfTheMonth().Month, nextExecutionTime.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringWeekly(SchedulerConfiguration scheduler, DateTime nextExecutionTime, int orden)
        {
            if (orden == 0)
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
            if (scheduler.CurrentDate.TimeOfDay < scheduler.StartingAt.TimeOfDay)
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
            nextExecutionTime = CalculateNextUnitTime(scheduler, nextExecutionTime);
            if (nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                nextExecutionTime = CalculateRecurringMoreNextDayWeekly(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateNextUnitTime(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            switch (scheduler.UnitTime)
            {
                case UnitTime.Hours:
                    nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
                    break;
                case UnitTime.Minutes:
                    nextExecutionTime = nextExecutionTime.AddMinutes(scheduler.OccursEvery);
                    break;
                case UnitTime.Seconds:
                    nextExecutionTime = nextExecutionTime.AddSeconds(scheduler.OccursEvery);
                    break;
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
                nextExecutionTime = nextExecutionTime.AddDays((scheduler.Frequency - 1) * 7);
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
            ValidateDailyFrecuency(scheduler);
        }

        private static void ValidateDailyFrecuency(SchedulerConfiguration scheduler)
        {
            if (scheduler.StartingAt.TimeOfDay > scheduler.EndingAt.TimeOfDay)
            {
                throw new Exception("La fecha de inicio no puede ser mayor que la fecha de fin");
            }
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
                scheduler.CurrentDate.AddDays(scheduler.Frequency);
            }
            catch (Exception)
            {
                throw new Exception("La Fecha introducida es demasiado grande");
            }
        }
    }
}
