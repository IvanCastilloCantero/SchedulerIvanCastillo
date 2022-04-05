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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 3,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 4,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
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
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.Day,
                Day = 8,
                Frequency = 3,
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
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.Day,
                Day = 8,
                Frequency = 3,
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
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 6, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 7, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Wednesday_Same_Month_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 2, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 5, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 4, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Saturday_Same_Month_Between_Starting_Ending()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 25, 5, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 5, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Saturday_Same_Month_High_Hour()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 25, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Three_Time_Monthly_The_Last_Saturday_Between_Starting_Ending()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 25, 5, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(3);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 5, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 7, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 4, 25, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Saturday_Same_Month_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 26, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Saturday_Same_Month_Same_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 25, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Last_Saturday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 25, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 25, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 4, 25, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 5, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_First_WeekDay()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 5, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 5, 1, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 5, 1, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 5, 1, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 8, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Last_WeekDay()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 28, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 2, 28, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 2, 28, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 5, 29, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Same_Month_High_Hour()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Same_Month_When_First_Day_Is_Weekend()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Day_Same_Month_Hour_High()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_First_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 31, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 5, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Last_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 31, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 5, 31, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Times_Monthly_The_First_Day_One_Hour()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 1,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs the first day of every 3 months every 1 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 4, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Weekend_Day_Same_Month_()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 29, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 3, 29, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Last_Weekend_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 29, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 3, 29, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 3, 29, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 3, 29, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 6, 28, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_First_Weekend_Day()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 3, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 3, 1, 4, 0, 0));
            informations[0].Description.Should().Be("Occurs the first weekend_day of every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 3, 1, 6, 0, 0));
            informations[1].Description.Should().Be("Occurs the first weekend_day of every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 3, 1, 8, 0, 0));
            informations[2].Description.Should().Be("Occurs the first weekend_day of every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 6, 6, 4, 0, 0));
            informations[3].Description.Should().Be("Occurs the first weekend_day of every 3 months every 2 hours between 4:00 and 8:00 starting on 01/01/2020");
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Same_Month_High_Hour()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 1, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 3, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 31, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 24, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 4, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 5, 4 ,0 ,0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 6, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 7, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 2, 4 ,0 ,0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 1, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_First_Weekend_Day_Other_Month_When_First_Day_Is_WeekDay()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 3, 9, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.First,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 4, 4, 4, 0, 0));
        }
        
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Monday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 13, 4, 0, 0));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 14, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 8, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 9, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 10, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 11, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 12, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 8, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 2, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 28, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Same_Month_Day_One()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 31, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_Day_Same_Month_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 31, 8, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 29, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Same_Month_Day_Two()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 2, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 2, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 11, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10, 4, 0, 0));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 11, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 12, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 13, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 14, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 9, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 10, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Last_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 31, 9, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Last,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 28, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 2, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Second_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 15, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Second,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 8, 4, 0, 0));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Monday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 20, 4, 0 ,0));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Tuesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 21, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Wednesday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Third_Wednesday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 4, 15, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Thursday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Third_Thursday()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 6, 0, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 8, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 4, 16, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Third_Thursday_Every_30Min()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 30,
                UnitTime = UnitTime.Minutes,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 5, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 30, 0));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 5, 0, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 4, 16, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_Four_Times_Monthly_The_Third_Thursday_Every_30Seg()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 30,
                UnitTime = UnitTime.Seconds,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 4, 1, 0)
            };

            var informations = scheduler.CalculateNextExecution(4);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 0));
            informations[1].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 0, 30));
            informations[2].NextExecutionTime.Should().Be(new DateTime(2020, 1, 16, 4, 1, 0));
            informations[3].NextExecutionTime.Should().Be(new DateTime(2020, 4, 16, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Friday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 17, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Saturday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 18, 4 ,0 ,0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Sunday_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 19, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_WeekDay_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 15, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_One()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0 ,0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_Two()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 2, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Same_Month_Day_Three()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 3, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Weekend_Day_Same_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 1, 18, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Monday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Monday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 17, 4, 0, 0));
        }
        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Tuesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Tuesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 18, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Wednesday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Wednesday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 19, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Thursday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 21, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Thursday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 20, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Friday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Friday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 21, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Saturday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 21, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Saturday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 15, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Sunday_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 21, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.Sunday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 16, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_WeekDay_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 22, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekday,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 17, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 4, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 3, 4, 0, 0));
        }

        [Fact]
        public void Calculate_Recurring_Execution_One_Time_Monthly_The_Third_Weekend_Day_Other_Month()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 1, 20, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Third,
                OccursDay = DayOccurrency.weekend_day,
                Frequency = 3,
                StartDate = new DateTime(2020, 1, 1),
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            var informations = scheduler.CalculateNextExecution(1);

            informations[0].NextExecutionTime.Should().Be(new DateTime(2020, 2, 15, 4, 0, 0));
        }

        [Fact]
        public void CalculateRecurringExecutionExceptionDateTooHigh()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(9999, 12, 31, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
                DayOfWeeks = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Thursday, DayOfWeek.Friday },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            scheduler.Invoking(x => x.CalculateNextExecution(1)).Should().Throw<SchedulerException>().WithMessage("Date introduced is too high");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Exception_Wihout_Day_Of_Week()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2020, 12, 31, 0, 0, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Weekly,
                StartDate = new DateTime(2020, 1, 1),
                Frequency = 2,
                DayOfWeeks = new List<DayOfWeek> { },
                OccursEvery = 2,
                StartingAt = new DateTime(2020, 1, 1, 4, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 8, 0, 0)
            };

            scheduler.Invoking(x => x.CalculateNextExecution(1)).Should().Throw<Exception>().WithMessage("Select at least one day of week");
        }

        [Fact]
        public void Calculate_Recurring_Execution_Exception_888()
        {
            SchedulerConfiguration scheduler = new()
            {
                CurrentDate = new DateTime(2022, 4, 1, 9, 30, 0),
                Type = ExecutionType.Recurring,
                Occurs = OccursType.Monthly,
                MonthlyConf = MonthlyConfType.The,
                OrderDay = Occurrency.Forth,
                OccursDay = DayOccurrency.Thursday,
                StartDate = new DateTime(2022, 4, 1),
                Frequency = 5,
                UnitTime = UnitTime.Minutes,
                OccursEvery = 10,
                StartingAt = new DateTime(2020, 1, 1, 12, 0, 0),
                EndingAt = new DateTime(2020, 1, 1, 12, 30, 0)
            };

            var a = scheduler.CalculateNextExecution(30);

            
        }

    }
}