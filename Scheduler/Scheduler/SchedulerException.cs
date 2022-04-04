using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    [Serializable]
    public class SchedulerException : Exception
    {

        public SchedulerException(string message) : base(message)
        {
            
        }
    }
}
