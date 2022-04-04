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
            for (int order = 0; order < times; order++)
            {
                nextExecutionTime = CalculateNextExecutionCheckType(scheduler, order, nextExecutionTime);
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

        private static DateTime CalculateRecurringExecution(SchedulerConfiguration scheduler, int ordna, DateTime previousExecutionTime)
        {
            ValidateFields(scheduler);
            return scheduler.Occurs == OccursType.Weekly
                ? CalculateRecurringWeekly(scheduler, previousExecutionTime, ordna)
                : CalculateRecurringMonthly(scheduler, previousExecutionTime, ordna);
        }

        private static DateTime CalculateRecurringMonthly(SchedulerConfiguration scheduler, DateTime previousExecutionTime, int orden)
        {
            return orden == 0 
                ? CalculateRecurringFirstMonthly(scheduler) 
                : CalculateRecurringMoreMonthly(scheduler, previousExecutionTime);
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
            return scheduler.CurrentDate.Day > ((int)scheduler.OrderDay + 1)
                ? nextExecutionTime.AddMonths(1)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyThe(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate.Day < (7 * ((int)scheduler.OrderDay + 1))
                ? CalculateRecurringFirstMonthlyTheSameMonth(scheduler)
                : CalculateRecurringFirstMonthlyTheOtherMonth(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonth(SchedulerConfiguration scheduler)
        {
            return (int)scheduler.OccursDay < 7
                ? CalculateRecurringFirstMonthlyTheSameMonthEspecificDay(scheduler)
                : CalculateRecurringFirstMonthlyTheSameMonthNotEspecificDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthNotEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthDay(scheduler);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDay(scheduler);
                    break;
                default:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(scheduler);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDay(SchedulerConfiguration scheduler)
        {
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheSameMonthDayNotLast(scheduler)
                : CalculateRecurringFirstMontlhyTheSameMonthDayLast(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayNotLast(SchedulerConfiguration scheduler)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
            return scheduler.CurrentDate.Day < ((int)scheduler.OrderDay + 1)
                ? new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthDayNotLastSameDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayNotLastSameDay(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate.Day == ((int)scheduler.OrderDay + 1)
                ? CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(scheduler)
                : CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMontlhyTheSameMonthDayLast(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate < scheduler.CurrentDate.LastDayOfTheMonth()
                ? new DateTime(scheduler.CurrentDate.LastDayOfTheMonth().Year, scheduler.CurrentDate.LastDayOfTheMonth().Month, scheduler.CurrentDate.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayCheckStart(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate.TimeOfDay <= scheduler.StartingAt.TimeOfDay
                ? new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.CurrentDate.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthDayCheckStartHourHigherStartingDate(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthDayCheckStartHourHigherStartingDate(SchedulerConfiguration scheduler)
        {
            return scheduler.CurrentDate.TimeOfDay >= scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler)
                : scheduler.CurrentDate;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDay(SchedulerConfiguration scheduler)
        {
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekDayNotLast(scheduler)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekDayLast(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayLast(SchedulerConfiguration scheduler)
        {
            var weekDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1).LastDayOfTheMonth();
            while (weekDay.DayOfWeek == DayOfWeek.Saturday | weekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDay = weekDay.AddDays(-1);
            }
            return CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(scheduler, weekDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayNotLast(SchedulerConfiguration scheduler)
        {
            var weekDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1).AddDays(7 * (int)scheduler.OrderDay);
            while (weekDay.DayOfWeek == DayOfWeek.Saturday | weekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDay = weekDay.AddDays(1);
            }
            return CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(scheduler, weekDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(SchedulerConfiguration scheduler, DateTime weekDay)
        {
            return weekDay.Day > scheduler.CurrentDate.Day
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(scheduler, weekDay)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckSameDay(scheduler, weekDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckSameDay(SchedulerConfiguration scheduler, DateTime weekDay)
        {
            return weekDay.Day == scheduler.CurrentDate.Day
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(scheduler, scheduler.CurrentDate)
                : CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStart(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay <= scheduler.StartingAt.TimeOfDay
                ? new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStartHourHigherStartingDate(scheduler, previousExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheckStartHourHigherStartingDate(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay >= scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(scheduler)
                : previousExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            nextExecutionTime = scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheOtherMonthWeekDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringFirstMonthlyTheOtherMonthWeekDayLast(scheduler, nextExecutionTime);
            return new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(SchedulerConfiguration scheduler)
        {
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekendDayNotLast(scheduler)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekendDayLast(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayLast(SchedulerConfiguration scheduler)
        {
            var weekendDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1).LastDayOfTheMonth();
            while (weekendDay.DayOfWeek != DayOfWeek.Saturday && weekendDay.DayOfWeek != DayOfWeek.Sunday)
            {
                weekendDay = weekendDay.AddDays(-1);
            }
            return CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(scheduler, weekendDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayNotLast(SchedulerConfiguration scheduler)
        {
            var weekendDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1).AddDays(7 * (int)scheduler.OrderDay);
            while (weekendDay.DayOfWeek != DayOfWeek.Saturday && weekendDay.DayOfWeek != DayOfWeek.Sunday)
            {
                weekendDay = weekendDay.AddDays(1);
            }
            return CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(scheduler, weekendDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(SchedulerConfiguration scheduler, DateTime weekendDay)
        {
            return weekendDay.Day > scheduler.CurrentDate.Day
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(scheduler, weekendDay)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckSameDay(scheduler, weekendDay);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckSameDay(SchedulerConfiguration scheduler, DateTime weekendDay)
        {
            return weekendDay.Day == scheduler.CurrentDate.Day
                ? CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(scheduler, scheduler.CurrentDate)
                : CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStart(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay <= scheduler.StartingAt.TimeOfDay
                ? new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStartHourHigherStartingDate(scheduler, previousExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheckStartHourHigherStartingDate(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay >= scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(scheduler)
                : previousExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDay(SchedulerConfiguration scheduler)
        {
            var nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            nextExecutionTime = scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayLast(nextExecutionTime);
            return new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayLast(DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthWeekendDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheSameMonthEspecificDayNotLast(scheduler, scheduler.CurrentDate)
                : CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLast(scheduler, scheduler.CurrentDate);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return (int)previousExecutionTime.DayOfWeek == (int)scheduler.OccursDay && previousExecutionTime.Day > (previousExecutionTime.LastDayOfTheMonth().Day - 7)
                ? CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDay(scheduler, previousExecutionTime)
                : CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastOtherDay(scheduler, previousExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastOtherDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var day = previousExecutionTime.LastDayOfTheMonth();
            while ((int)day.DayOfWeek != (int)scheduler.OccursDay)
            {
                day = day.AddDays(-1);
                if (day.Day < previousExecutionTime.Day)
                {
                    day = CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler);
                }
            }
            return new DateTime(day.Year, day.Month, day.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay < scheduler.StartingAt.TimeOfDay
                ? new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDayHourHigherStartingDate(scheduler, previousExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayLastSameDayHourHigherStartingDate(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.TimeOfDay >= scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler)
                : new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.Day, previousExecutionTime.Hour, previousExecutionTime.Minute, previousExecutionTime.Second);
        }
    
        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            return (int)nextExecutionTime.DayOfWeek == (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1))
                ? CalculateRecurringFirstMonthlyTheSameMonthEspecificDaySameDay(scheduler, nextExecutionTime)
                : CalculateRecurringFirstMonthlyTheSameMonthEspecificDayOtherDay(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDaySameDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.Hour < scheduler.StartingAt.Hour
                ? new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : previousExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDayOtherDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime;
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
            return (int)scheduler.OccursDay < 7
                ? CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(scheduler)
                : CalculateRecurringFirstMonthlyTheOtherMonthNotEspecificDay(scheduler);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthNotEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonthDay(scheduler);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDay(scheduler);
                    break;
                default:
                    nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(scheduler);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDay(SchedulerConfiguration scheduler)
        {
            var nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheOtherMonthDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringFirstMonthlyTheOtherMonthDayLast(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - previousExecutionTime.Day;
            var nextExecutionTime = new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = new DateTime(previousExecutionTime.LastDayOfTheMonth().Year, previousExecutionTime.LastDayOfTheMonth().Month, previousExecutionTime.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            var nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            nextExecutionTime = scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayLast(scheduler, nextExecutionTime);
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthly(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return scheduler.MonthlyConf == MonthlyConfType.Day
                ? CalculateRecurringMoreMonthlyDay(scheduler, previousExecutionTime)
                : CalculateRecurringMoreMonthlyThe(scheduler, previousExecutionTime);
        }

        private static DateTime CalculateRecurringMoreMonthlyDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? nextExecutionTime = CalculateRecurringMoreNextDayMonthlyDay(scheduler, previousExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayMonthlyDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddMonths(scheduler.Frequency);
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Minute); 
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyThe(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            return (int)scheduler.OccursDay < 7
                ? CalculateRecurringMoreMonthlyEspecificDay(scheduler, nextExecutionTime)
                : CalculateRecurringMoretMonthlyNotEspecificDay(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringMoreMonthlyEspecificDayNextDay(scheduler, nextExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayNextDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            nextExecutionTime = scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringMoreMonthlyEspecificDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringMoreMonthlyEspecificDayLast(scheduler, nextExecutionTime);
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoretMonthlyNotEspecificDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            DateTime nextExecutionTime;
            switch (scheduler.OccursDay)
            {
                case DayOccurrency.day:
                    nextExecutionTime = CalculateRecurringMoreMonthlyTheDay(scheduler, previousExecutionTime);
                    break;
                case DayOccurrency.weekday:
                    nextExecutionTime = CalculateRecurringMoreMonthlyWeekDay(scheduler, previousExecutionTime);
                    break;
                default:
                    nextExecutionTime = CalculateRecurringMoreMonthlyWeekendDay(scheduler, previousExecutionTime);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyWeekendDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringMoreTheWeekendDayMonthlyNextDay(scheduler, nextExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            nextExecutionTime = scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringMoreTheWeekendDayMonthlyNextDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringMoreTheWeekendDayMonthlyNextDayLast(nextExecutionTime);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDayLast(DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyWeekDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringMoreTheWeekDayMonthlyNextDay(scheduler, nextExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddMonths(scheduler.Frequency);
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringMoreTheWeekDayMonthlyNextDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringMoreTheWeekDayMonthlyNextDayLast(nextExecutionTime);
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDayLast(DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.LastDayOfTheMonth();
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday ||
                nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(-1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday || nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyTheDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringMoreTheDayMonthlyNextDay(scheduler, nextExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = previousExecutionTime.AddMonths(scheduler.Frequency);
            return scheduler.OrderDay != Occurrency.Last
                ? CalculateRecurringMoreTheDayMonthlyNextDayNotLast(scheduler, nextExecutionTime)
                : CalculateRecurringMoreTheDayMonthlyNextDayLast(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDayLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return new DateTime(previousExecutionTime.LastDayOfTheMonth().Year, previousExecutionTime.LastDayOfTheMonth().Month, previousExecutionTime.LastDayOfTheMonth().Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDayNotLast(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - previousExecutionTime.Day;
            return new DateTime(previousExecutionTime.Year, previousExecutionTime.Month, previousExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
        }

        private static DateTime CalculateRecurringWeekly(SchedulerConfiguration scheduler, DateTime nextExecutionTime, int orden)
        {
            return orden == 0 
                ? CalculateRecurringFirstWeekly(scheduler) 
                : CalculateRecurringMoreWeekly(scheduler, nextExecutionTime);
        }

        private static DateTime CalculateRecurringFirstWeekly(SchedulerConfiguration scheduler)
        {
            var nextExecutionTime = CalculateRecurringFirstCheckHourWeekly(scheduler);
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
            return scheduler.CurrentDate.TimeOfDay < scheduler.StartingAt.TimeOfDay
                ? new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.CurrentDate.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second)
                : scheduler.CurrentDate;
        }

        private static bool ContainsDayOfWeek(List<DayOfWeek> daysSelected, DayOfWeek nextDay)
        {
            return daysSelected.Where(d => d == nextDay).Count() > 0;
        }

        private static DateTime CalculateRecurringMoreWeekly(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            var nextExecutionTime = CalculateNextUnitTime(scheduler, previousExecutionTime);
            return nextExecutionTime.TimeOfDay > scheduler.EndingAt.TimeOfDay
                ? CalculateRecurringMoreNextDayWeekly(scheduler, nextExecutionTime)
                : nextExecutionTime;
        }

        private static DateTime CalculateNextUnitTime(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            DateTime nextExecutionTime = new DateTime();
            switch (scheduler.UnitTime)
            {
                case UnitTime.Hours:
                    nextExecutionTime = previousExecutionTime.AddHours(scheduler.OccursEvery);
                    break;
                case UnitTime.Minutes:
                    nextExecutionTime = previousExecutionTime.AddMinutes(scheduler.OccursEvery);
                    break;
                case UnitTime.Seconds:
                    nextExecutionTime = previousExecutionTime.AddSeconds(scheduler.OccursEvery);
                    break;
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayWeekly(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            DateTime nextExecutionTime = new DateTime();
            for (int i = 1; i < 7; i++)
            {
                var nextDay = previousExecutionTime.AddDays(i);
                if (ContainsDayOfWeek(scheduler.DayOfWeeks, nextDay.DayOfWeek))
                {
                    nextExecutionTime = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
                    i = 7;
                }
            }
            nextExecutionTime = CalculateRecurringMoreNextDayCheckWeek(scheduler, nextExecutionTime);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayCheckWeek(SchedulerConfiguration scheduler, DateTime previousExecutionTime)
        {
            return previousExecutionTime.DayOfWeek == scheduler.DayOfWeeks.First()
                ? previousExecutionTime.AddDays((scheduler.Frequency - 1) * 7)
                : previousExecutionTime;
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
                throw new SchedulerException("Starting date can't be higher than ending date");
            }
        }

        private static void ValidateDayOfWeeks(SchedulerConfiguration scheduler)
        {
            if (scheduler.DayOfWeeks.Count() == 0)
            {
                throw new SchedulerException("Select at least one day of week");
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
                throw new SchedulerException("Date introduced is too high");
            }
        }
    }
}
