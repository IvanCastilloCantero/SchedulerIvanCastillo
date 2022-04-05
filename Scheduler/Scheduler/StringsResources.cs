using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scheduler
{
    public class StringsResources
    {
        public static List<CultureResources> ResourcesList { get; set; }
        public CultureInfo Culture { get; set; }

        public StringsResources(CultureInfo cultureInfo)
        {
            ResourcesList = new List<CultureResources>();
            Culture = cultureInfo;
            if (Culture == new CultureInfo("es-ES"))
            {
                //AddStringES();
            } 
            else
            {
                AddStringUS();
            }
        }

        public static string GetResource(string code)
        {
            var resource = ResourcesList.Where(x => x.Code == code).First();
            return resource.Description;
        }

        private void AddStringUS()
        {
            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringMonthlyThe",
                Description = "Occurs the {0} {1} of every {2} months every {3} {4} between {5} and {6} starting on {7}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringMonthlyDay",
                Description = "Occurs day {0} every {1} months every {2} {3} between {4} and {5} starting on {6}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "Once",
                Description = "Occurs {0}. Schedule will be used on {1} at {2} starting on {3}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringWeekly",
                Description = "Occurs every {0} weeks {1} every {2} {3} between {4} and {5} starting on {6}"
            }
            );
        }
    }
}
