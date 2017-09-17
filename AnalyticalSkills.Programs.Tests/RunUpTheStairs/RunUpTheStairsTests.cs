using System;
using System.Collections;
using AnalyticalSkills.Programs.RunUpTheStairs;
using NUnit.Framework;

namespace AnalyticalSkills.Programs.Tests.RunUpTheStairs
{
    [TestFixture]
    public class RunUpTheStairsTests
    {
        [TestCaseSource(nameof(TestData))]
        public void RunUpStairsShouldReturnAValidValue(byte[] stairs, byte strides, int expectResult)
        {
            // Arrange.

            IRunUpTheStairs runner = new Programs.RunUpTheStairs.RunUpTheStairs();

            // Act.

            int result = runner.CalculateTotalStrides(stairs, strides);

            // Assert.

            Assert.That(result, Is.EqualTo(expectResult));
        }


        [TestCaseSource(nameof(InvalidFlightsData))]
        public void RunUpStairs_Should_Throw_Expcetion_When_Out_Of_Range_Flights_Are_Passed_In(byte[] stairs,
            byte strides)
        {
            //
            // Arrange.
            //

            IRunUpTheStairs runner = new Programs.RunUpTheStairs.RunUpTheStairs();

            //
            // Act.
            //

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => runner.CalculateTotalStrides(stairs, strides));


            //
            // Assert.
            //

            Assert.That(ex.Message,
                Is.EqualTo("staircase must have between 1 and 50 flights.\r\nParameter name: staircase"));
        }

        [TestCaseSource(nameof(InvalidStridesData))]
        public void CalculateTotalStrides_should_throw_ArgumentOutOfRangeException_when_Invalid_strides_are_given(
            byte[] stairs, byte strides)
        {
            //
            // Arrange.
            //

            IRunUpTheStairs runner = new Programs.RunUpTheStairs.RunUpTheStairs();

            //
            // Act.
            //

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => runner.CalculateTotalStrides(stairs, strides));

            //
            // Assert.
            //

            Assert.That(ex.Message, Is.EqualTo("Stride must be between 2 and 5 steps.\r\nParameter name: stride"));
        }

        [TestCaseSource(nameof(InvalidNumberOfStepsInFlihgts))]
        public void
            CalculateTotalStrides_should_throw_ArgumentOutOfRangeException_when_Invalid_NumberOf_Steps_Are_Passed_In(
                byte[] stairs, byte strides)
        {
            //
            // Arrange.
            //

            IRunUpTheStairs runner = new Programs.RunUpTheStairs.RunUpTheStairs();

            //
            // Act.
            //

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => runner.CalculateTotalStrides(stairs, strides));

            // 
            // Assert.
            //

            Assert.That(ex.Message,
                Is.EqualTo("Steps in flight must be between 5 and 30.\r\nParameter name: flightsOfStairs"));
        }

        private static IEnumerable InvalidStridesData()
        {
            // valid stairs, invalid stride
            yield return new TestCaseData(new byte[] {15, 16, 17, 18}, (byte) 6);
            yield return new TestCaseData(new byte[] {5, 6, 7, 8}, (byte) 1);
            yield return new TestCaseData(new byte[] {25, 10, 5, 10}, (byte) 0);
        }

        private static IEnumerable InvalidNumberOfStepsInFlihgts()
        {
            yield return new TestCaseData(new byte[] {4}, (byte) 2);
            yield return new TestCaseData(new byte[] {0, 1, 2, 3, 4, 5}, (byte) 2);
            yield return new TestCaseData(new byte[] {31}, (byte) 2);
        }

        private static IEnumerable InvalidFlightsData()
        {
            // invalid stairs, valid stride
            yield return new TestCaseData(new byte[] {}, (byte) 2);
            // flights more than 50, valid stride
            yield return
                new TestCaseData(
                    new byte[]
                    {
                        10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                        10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                        10, 10, 10
                    }, (byte) 2);
        }

        private static IEnumerable TestData()
        {
            yield return new TestCaseData(new byte[] {5, 6, 7, 8}, (byte) 5, (ushort) 13);
            yield return new TestCaseData(new byte[] {5, 10, 5, 20, 30, 10, 5}, (byte) 5, (ushort) 29);
            yield return new TestCaseData(new byte[] {15}, (byte) 2, (ushort) 8);
            yield return new TestCaseData(new byte[] {15, 15}, (byte) 2, (ushort) 18);
            yield return new TestCaseData(new byte[] {5, 11, 9, 13, 8, 30, 14}, (byte) 3, (ushort) 44);

            // 50 flights..
            yield return
                new TestCaseData(
                    new byte[]
                    {
                        30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
                        30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30,
                        30, 30
                    }, (byte) 2, (ushort) 848);
        }
    }
}