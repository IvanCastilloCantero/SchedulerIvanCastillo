using Xunit;
using Scheduler;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace SchedulerTest
{
    public class SchedulerTest
    {
        [Fact]
        public void Calculate_Once_Execution()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 4),
                Type = ExecutionType.Once,
                DateTime = new DateTime(2020, 1, 8, 14, 0, 0),
                Days = 0,
                StartDate = new DateTime(2020, 1, 1)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(scheduler.DateTime);
            informations[0].Description.Should().Be("Occurs Once. Schedule will be used on 08/01/2020 at 14:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Tuesday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday and tuesday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Wednesday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday and wednesday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday and thursday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Tuesday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, tuesday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Wednesday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, wednesday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Thursday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Tuesday_Thursday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Tuesday_Wednesday_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday and thursday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Monday_Wednesday_Thursday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Tuesday_Wednesday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on tuesday and wednesday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Tuesday_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on tuesday and thursday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Tuesday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on tuesday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Tuesday_Wednesday_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on tuesday, wednesday and thursday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Tuesday_Wednesday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on tuesday, wednesday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Wednesday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on wednesday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Wednesday_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Thursday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on wednesday and thursday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Wednesday_Thursday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Thursday_Friday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_AllWeek()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_AllWeek_Six_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(6);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");

            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");

            informations[4].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 6, 0, 0));
            informations[4].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");

            informations[5].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 8, 0, 0));
            informations[5].Description.Should().Be("Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 15/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Two_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 5, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(2);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 5, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 7, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Three_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 6, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(3);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 6, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 8, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 13, 4, 0, 0));
            informations[2].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Three_Times_Every_Three_Weeks()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 6, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 3,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(3);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 6, 0, 0));
            informations[0].Description.Should().Be("Occurs every 3 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 8, 0, 0));
            informations[1].Description.Should().Be("Occurs every 3 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 20, 4, 0, 0));
            informations[2].Description.Should().Be("Occurs every 3 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Three_Times_Every_Four_Weeks()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 6, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 4,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(3);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 6, 0, 0));
            informations[0].Description.Should().Be("Occurs every 4 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 8, 0, 0));
            informations[1].Description.Should().Be("Occurs every 4 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 27, 4, 0, 0));
            informations[2].Description.Should().Be("Occurs every 4 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 03/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Five_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(5);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Six_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(6);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[4].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 6, 0, 0));
            informations[4].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[5].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 8, 0, 0));
            informations[5].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Seven_Times()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 5, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(7);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 1, 9, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[4].NextExecutionTime.Should().Be(new DateTime(2020, 1, 9, 6, 0, 0));
            informations[4].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[5].NextExecutionTime.Should().Be(new DateTime(2020, 1, 9, 8, 0, 0));
            informations[5].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

            informations[6].NextExecutionTime.Should().Be(new DateTime(2020, 1, 10, 4, 0, 0));
            informations[6].Description.Should().Be("Occurs every 2 weeks on monday, thursday and friday every 2 hours between 4:00 and 8:00 starting on 05/01/2020");

        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.Day,
                Day = 8,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2 ,8, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs day 8 every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.Day,
                Day = 8,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)

            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs day 8 every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs day 8 every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs day 8 every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");

            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 5, 8, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs day 8 every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Monday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 7));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 4));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 5));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 4));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 4));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 5));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 6));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 7));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 2));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Monday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 13));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 14));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 8));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 9));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 10));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 11));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 12));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 8));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Same_Month_Day_One()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Same_Month_Day_Two()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 2, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 11));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 11));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 12));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 13));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 14));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 9));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 2));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Monday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 20));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 21));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 17));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 18));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 19));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 20));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_One()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_Two()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 2, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_Three()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 11));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Monday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Tuesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 11));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Wednesday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 12));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 13));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Friday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 21));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Saturday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Sunday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 9));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekday,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 4, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekend_day,
                EveryMonths = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 15));
        }

        [Fact]
        public void CalculateRecurringExecutionExceptionFecha()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(9999, 12, 31, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            scheduler.Invoking(x => x.CalculateNextExecution(1)).Should().Throw<Exception>().WithMessage("La fecha introducida es demasiado grande");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Exception_Wihout_Day_Of_Week()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 12, 31, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Days = 1,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                EveryWeeks = 2,
                DayOfWeeks = new List<DayOfWeek> { },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            scheduler.Invoking(x => x.CalculateNextExecution(1)).Should().Throw<Exception>().WithMessage("Selecciona algun dia de la semana");
        }
    }
}