using System;

namespace Scheduler
{
    public struct SchedulerInfo
    {
        public DateTime NextExecutionTime { get; set; }
        public string Description { get; set; }

        public SchedulerInfo(DateTime NextExecutionTime, string Description)
        {
            this.NextExecutionTime = NextExecutionTime;
            this.Description = Description;
        }
    }
}
