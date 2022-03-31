using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public static class SchedulerExtensions
    {
        public static SchedulerInfo[] CalculateNextExecution(this SchedulerConfiguration scheduler, int times)
        {
            var nextExecutionTime = new DateTime();
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
            var nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, scheduler.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            if (scheduler.CurrentDate.Day > (int)scheduler.OrderDay + 1)
            {
                nextExecutionTime = nextExecutionTime.AddMonths(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyThe(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            if (scheduler.CurrentDate.Day < (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonth(scheduler);
            }
            else
            {
                nextExecutionTime = CalculateRecurringFirstMonthlyTheOtherMonth(scheduler, nextExecutionTime);
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
            weekDay = weekDay.AddDays(7 * (int)scheduler.OrderDay);
            while (weekDay.DayOfWeek == DayOfWeek.Saturday | weekDay.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDay = weekDay.AddDays(1);
            }
            DateTime nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekDayCheck(scheduler, weekDay);
            return nextExecutionTime;
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
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday | nextExecutionTime.DayOfWeek == DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthWeekendDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime;
            DateTime weekendDay = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            weekendDay = weekendDay.AddDays(7 * (int)scheduler.OrderDay);
            while (weekendDay.DayOfWeek != DayOfWeek.Saturday && weekendDay.DayOfWeek != DayOfWeek.Sunday)
            {
                weekendDay = weekendDay.AddDays(1);
            }
            nextExecutionTime = CalculateRecurringFirstMonthlyTheSameMonthWeekendDayCheck(scheduler, weekendDay);
            return nextExecutionTime;
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
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheSameMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = scheduler.CurrentDate;
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

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonth(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
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
            var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringFirstMonthlyTheOtherMonthEspecificDay(SchedulerConfiguration scheduler)
        {
            DateTime nextExecutionTime = new DateTime(scheduler.CurrentDate.Year, scheduler.CurrentDate.Month, 1);
            nextExecutionTime = nextExecutionTime.AddMonths(1);
            nextExecutionTime = nextExecutionTime.AddDays(7 * ((int)scheduler.OrderDay));
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay && nextExecutionTime.Day <= (7 * ((int)scheduler.OrderDay + 1)))
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
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
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreNextDayMonthlyDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreNextDayMonthlyDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(scheduler.Frequency).Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Minute);
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
            if (nextExecutionTime.Hour < scheduler.EndingAt.Hour)
            {
                nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            } 
            else
            {
                nextExecutionTime = CalculateRecurringMoreMonthlyEspecificDayNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyEspecificDayNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(scheduler.Frequency).Month, 1);
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while ((int)nextExecutionTime.DayOfWeek != (int)scheduler.OccursDay)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.Month, nextExecutionTime.Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
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
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekendDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekendDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(1).Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek != DayOfWeek.Saturday && nextExecutionTime.DayOfWeek != DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyWeekDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreTheWeekDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheWeekDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(1).Month, 1, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
            nextExecutionTime = nextExecutionTime.AddDays(7 * (int)scheduler.OrderDay);
            while (nextExecutionTime.DayOfWeek == DayOfWeek.Saturday || nextExecutionTime.DayOfWeek == DayOfWeek.Sunday)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreMonthlyTheDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            nextExecutionTime = nextExecutionTime.AddHours(scheduler.OccursEvery);
            if (nextExecutionTime.Hour > scheduler.EndingAt.Hour)
            {
                nextExecutionTime = CalculateRecurringMoreTheDayMonthlyNextDay(scheduler, nextExecutionTime);
            }
            return nextExecutionTime;
        }

        private static DateTime CalculateRecurringMoreTheDayMonthlyNextDay(SchedulerConfiguration scheduler, DateTime nextExecutionTime)
        {
            var dayDifference = (int)scheduler.OrderDay + 1 - nextExecutionTime.Day;
            nextExecutionTime = new DateTime(nextExecutionTime.Year, nextExecutionTime.AddMonths(1).Month, nextExecutionTime.AddDays(dayDifference).Day, scheduler.StartingAt.Hour, scheduler.StartingAt.Minute, scheduler.StartingAt.Second);
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
