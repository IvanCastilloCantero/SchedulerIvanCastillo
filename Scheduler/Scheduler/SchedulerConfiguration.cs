using System;
using System.Collections.Generic;

namespace Scheduler
{
    public class SchedulerConfiguration
    {
        public DateTime CurrentDate { get; set; }
        public ExecutionType Type { get; set; }
        public bool Enable { get; set; }
        public DateTime DateTime { get; set; }
        public OccursType Occurs { get; set; }
        public int Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OccursOnceAt { get; set; }
        public int OccursEvery { get; set; }
        public UnitTime TipoTiempo { get; set; }
        public DateTime StartingAt { get; set; }
        public DateTime EndingAt { get; set; }
        public List<DayOfWeek> DayOfWeeks { get; set; }
        public int EveryWeeks { get; set; }
        public int Day { get; set; }
        public int EveryMonths { get; set; }
        public Occurrency OrderDay { get; set; }
        public DayOccurrency OccursDay { get; set; }
        public MonthlyConfType MonthlyConf { get; set; }
        
    }

    public enum ExecutionType { Once, Recurring }

    public enum UnitTime { Hours, Minutes, Seconds}

    public enum OccursType { Daily, Weekly, Monthly}

    public enum Occurrency { First, Second, Third, Forth, Last}

    public enum DayOccurrency { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, day, weekday, weekend_day}

    public enum MonthlyConfType { Day, The}
}