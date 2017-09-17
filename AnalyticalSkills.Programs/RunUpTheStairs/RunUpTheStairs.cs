using System;
using System.Linq;

namespace AnalyticalSkills.Programs.RunUpTheStairs
{
    /// <summary> Represents the Run up the stairs test question. </summary>
    public class RunUpTheStairs : IRunUpTheStairs
    {
        private const int MinFlights = 1;
        private const int MaxFlights = 50;

        private const int MinStepsInFlight = 5;
        private const int MaxStepsInFlight = 30;

        private const int MinStrides = 2;
        private const int MaxStrides = 5;

        /// <summary> Counts the strides to complete the stairs. </summary>
        /// <param name="flightsOfStairs">Flights</param>
        /// <param name="stride">Steps to cover in one stride.</param>
        /// <returns>Total strides take to complete all flights.</returns>
        public int CalculateTotalStrides(byte[] flightsOfStairs, byte stride)
        {
            ValidateNumberOfFlightsOfStairs(flightsOfStairs);
            ValidateFlightsValues(flightsOfStairs);
            ValidateStrides(stride);

            int numberOfStrides = 0;
            foreach (byte flight in flightsOfStairs)
            {
                numberOfStrides += Math.DivRem(flight, stride, out int remainder);
                if (remainder != 0)
                    numberOfStrides++;
            }

            return numberOfStrides + (flightsOfStairs.Length - 1) * 2;
        }

        /// <summary> The staircase has between 1 and 50 flights of stairs. Throws ArgumentOutOfRangeException if invalid. </summary>
        /// <param name="staircase">Stair case to validate.</param>
        private void ValidateNumberOfFlightsOfStairs(byte[] staircase)
        {
            if (staircase.Length < 1 || staircase.Length > 50)
                throw new ArgumentOutOfRangeException(
                    nameof(staircase),
                    $"{nameof(staircase)} must have between {MinFlights} and {MaxFlights} flights.");
        }

        /// <summary> Each flight of stairs has between 5 and 30 steps, inclusive. Throws ArgumentOutOfRangeException if invalid. </summary>
        /// <param name="flightsOfStairs">Flights of stairs to validate.</param>
        private void ValidateFlightsValues(byte[] flightsOfStairs)
        {
            if (flightsOfStairs.Any(flight => flight < 5 || flight > 30))
                throw new ArgumentOutOfRangeException(
                    nameof(flightsOfStairs),
                    $"Steps in flight must be between {MinStepsInFlight} and {MaxStepsInFlight}.");
        }

        /// <summary>Steps per stride is between 2 and 5, inclusive. Throws ArgumentOutOfRangeException if invalid.</summary>
        /// <param name="stride">Steps per stride to validate.</param>
        private void ValidateStrides(byte stride)
        {
            if (2 > stride || stride > 5)
                throw new ArgumentOutOfRangeException(
                    nameof(stride),
                    $"Stride must be between {MinStrides} and {MaxStrides} steps.");
        }
    }
}