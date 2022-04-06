using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scheduler
{
    public class StringsResources
    {
        public static List<CultureResources> ResourcesList { get; set; }
        public static CultureInfo Culture { get; set; }
        public static Dictionary<UnitTime, string> UnitTimeResources { get; set; }
        public static Dictionary<DayOccurrency, string> DayOccurrencyResources { get; set; }
        public static Dictionary<Occurrency, string> OccurrencyResources { get; set; }
        public static Dictionary<DayOfWeek, string> DayofWeekResources { get; set; }
        public static Dictionary<ExecutionType, string> ExecutionTypeResources { get; set; }

        public static void InicializateResouces(CultureInfo cultureInfo)
        {
            ResourcesList = new List<CultureResources>();
            UnitTimeResources = new Dictionary<UnitTime, string>();
            DayOccurrencyResources = new Dictionary<DayOccurrency, string>();
            OccurrencyResources = new Dictionary<Occurrency, string>();
            DayofWeekResources = new Dictionary<DayOfWeek, string>();
            ExecutionTypeResources = new Dictionary<ExecutionType, string>();
            Culture = cultureInfo;
            if (Culture.Name.Equals("es-ES"))
            {
                AddStringES();
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

        private static void AddStringES()
        {
            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "RecurringMonthlyThe",
                Description = "Ocurre el {0} {1} cada {2} meses cada {3} {4} entre las {5} {6} y las {7} {8} empezando el {9}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "RecurringMonthlyDay",
                Description = "Ocurre el dia {0} cada {1} meses cada {2} {3} entre las {4} {5} y las {6} {7} empezando el {8}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "Once",
                Description = "Ocurre {0}. El calendario sera usado el {1} a las {2} {3} empezando el {4}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "RecurringWeekly",
                Description = "Ocurre cada {0} semanas {1} cada {2} {3} entre las {4} {5} y las {6} {7} empezando el {8}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "on",
                Description = "en"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("es-ES"),
                Code = "and",
                Description = "y"
            }
            );
            
            AddEnumResourcesES();
        }

        private static void AddEnumResourcesES()
        {
            AddUnitTimeResourcesES();
            AddDayOccurrencyResourcesES();
            AddOccurrencyResourcesES();
            AddDayOfWeekResourcesES();
            AddExecutionTypeResourcesES();
        }
        
        private static void AddUnitTimeResourcesES()
        {
            UnitTimeResources.Add(UnitTime.Hours, "horas");
            UnitTimeResources.Add(UnitTime.Minutes, "minutos");
            UnitTimeResources.Add(UnitTime.Seconds, "segundos");
        }

        private static void AddDayOccurrencyResourcesES()
        {
            DayOccurrencyResources.Add(DayOccurrency.Monday, "lunes");
            DayOccurrencyResources.Add(DayOccurrency.Tuesday, "martes");
            DayOccurrencyResources.Add(DayOccurrency.Wednesday, "miercoles");
            DayOccurrencyResources.Add(DayOccurrency.Thursday, "jueves");
            DayOccurrencyResources.Add(DayOccurrency.Friday, "viernes");
            DayOccurrencyResources.Add(DayOccurrency.Saturday, "sabado");
            DayOccurrencyResources.Add(DayOccurrency.Sunday, "domingo");
            DayOccurrencyResources.Add(DayOccurrency.weekend_day, "dia de fin de semana");
            DayOccurrencyResources.Add(DayOccurrency.day, "dia");
            DayOccurrencyResources.Add(DayOccurrency.weekday, "dia de la semana");
        }

        private static void AddOccurrencyResourcesES()
        {
            OccurrencyResources.Add(Occurrency.First, "primer");
            OccurrencyResources.Add(Occurrency.Second, "segundo");
            OccurrencyResources.Add(Occurrency.Third, "tercero");
            OccurrencyResources.Add(Occurrency.Forth, "cuarto");
            OccurrencyResources.Add(Occurrency.Last, "ultimo");
        }

        private static void AddDayOfWeekResourcesES()
        {
            DayofWeekResources.Add(DayOfWeek.Monday, "lunes");
            DayofWeekResources.Add(DayOfWeek.Tuesday, "martes");
            DayofWeekResources.Add(DayOfWeek.Wednesday, "miercoles");
            DayofWeekResources.Add(DayOfWeek.Thursday, "jueves");
            DayofWeekResources.Add(DayOfWeek.Friday, "viernes");
            DayofWeekResources.Add(DayOfWeek.Saturday, "sabado");
            DayofWeekResources.Add(DayOfWeek.Sunday, "domingo");
        }

        private static void AddExecutionTypeResourcesES()
        {
            ExecutionTypeResources.Add(ExecutionType.Once, "una vez");
        }

        private static void AddStringUS()
        {
            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringMonthlyThe",
                Description = "Occurs the {0} {1} of every {2} months every {3} {4} between {5}{6} and {7}{8} starting on {9}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringMonthlyDay",
                Description = "Occurs day {0} every {1} months every {2} {3} between {4}{5} and {6}{7} starting on {8}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "Once",
                Description = "Occurs {0}. Schedule will be used on {1} at {2}{3} starting on {4}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "RecurringWeekly",
                Description = "Occurs every {0} weeks {1} every {2} {3} between {4}{5} and {6}{7} starting on {8}"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "on",
                Description = "on"
            }
            );

            ResourcesList.Add(new CultureResources
            {
                CultureInfo = new CultureInfo("en-US"),
                Code = "and",
                Description = "and"
            }
            );

            AddEnumResourcesUS();
        }

        private static void AddEnumResourcesUS()
        {
            AddUnitTimeResourcesUS();
            AddDayOccurrencyResourcesUS();
            AddOccurrencyResourcesUS();
            AddDayOfWeekResourcesUS();
            AddExecutionTypeResourcesUS();            
        }
        
        private static void AddUnitTimeResourcesUS()
        {
            UnitTimeResources.Add(UnitTime.Hours, "hours");
            UnitTimeResources.Add(UnitTime.Minutes, "minutes");
            UnitTimeResources.Add(UnitTime.Seconds, "seconds");
        }

        private static void AddDayOccurrencyResourcesUS()
        {
            DayOccurrencyResources.Add(DayOccurrency.Monday, "monday");
            DayOccurrencyResources.Add(DayOccurrency.Tuesday, "tuesday");
            DayOccurrencyResources.Add(DayOccurrency.Wednesday, "wednesday");
            DayOccurrencyResources.Add(DayOccurrency.Thursday, "thursday");
            DayOccurrencyResources.Add(DayOccurrency.Friday, "friday");
            DayOccurrencyResources.Add(DayOccurrency.Saturday, "saturday");
            DayOccurrencyResources.Add(DayOccurrency.Sunday, "sunday");
            DayOccurrencyResources.Add(DayOccurrency.weekend_day, "weekend day");
            DayOccurrencyResources.Add(DayOccurrency.day, "day");
            DayOccurrencyResources.Add(DayOccurrency.weekday, "weekday");
        }

        private static void AddOccurrencyResourcesUS()
        {
            OccurrencyResources.Add(Occurrency.First, "first");
            OccurrencyResources.Add(Occurrency.Second, "second");
            OccurrencyResources.Add(Occurrency.Third, "third");
            OccurrencyResources.Add(Occurrency.Forth, "forth");
            OccurrencyResources.Add(Occurrency.Last, "last");
        }

        private static void AddDayOfWeekResourcesUS()
        {
            DayofWeekResources.Add(DayOfWeek.Monday, "monday");
            DayofWeekResources.Add(DayOfWeek.Tuesday, "tuesday");
            DayofWeekResources.Add(DayOfWeek.Wednesday, "wednesday");
            DayofWeekResources.Add(DayOfWeek.Thursday, "thursday");
            DayofWeekResources.Add(DayOfWeek.Friday, "friday");
            DayofWeekResources.Add(DayOfWeek.Saturday, "saturday");
            DayofWeekResources.Add(DayOfWeek.Sunday, "sunday");
        }
        private static void AddExecutionTypeResourcesUS()
        {
            ExecutionTypeResources.Add(ExecutionType.Once, "once");
        }

    }
}
