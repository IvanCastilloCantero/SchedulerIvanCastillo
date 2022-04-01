using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Scheduler
{
    public class SchedulerConfiguration
    {
        public DateTime CurrentDate { get; set; }
        public ExecutionType Type { get; set; }
        public DateTime DateTime { get; set; }
        public OccursType Occurs { get; set; }
        public int Frequency { get; set; }
        public DateTime StartDate { get; set; }        
        public int OccursEvery { get; set; }
        public UnitTime UnitTime { get; set; }
        public DateTime StartingAt { get; set; }
        public DateTime EndingAt { get; set; }
        public List<DayOfWeek> DayOfWeeks { get; set; }
        public int Day { get; set; }
        public Occurrency OrderDay { get; set; }
        public DayOccurrency OccursDay { get; set; }
        public MonthlyConfType MonthlyConf { get; set; }
        
    }

    public enum ExecutionType { Once, Recurring }

    public enum UnitTime { Hours, Minutes, Seconds}

    public enum OccursType { Daily, Weekly, Monthly}

    public enum Occurrency { First, Second, Third, Forth, Last}

    public enum DayOccurrency { Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 0, day = 7, weekday = 8, weekend_day = 9}

    public enum MonthlyConfType { Day, The}
}