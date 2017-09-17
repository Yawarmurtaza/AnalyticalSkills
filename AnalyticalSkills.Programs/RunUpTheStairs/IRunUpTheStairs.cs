namespace AnalyticalSkills.Programs.RunUpTheStairs
{
    /// <summary> Represents the Run up the stairs test question. </summary>
    public interface IRunUpTheStairs
    {
        /// <summary> Counts the strides to complete the stairs. </summary>
        /// <param name="flightsOfStairs">Flights</param>
        /// <param name="stride">Steps to cover in one stride.</param>
        /// <returns>Total strides take to complete all flights.</returns>
        int CalculateTotalStrides(byte[] flightsOfStairs, byte stride);
    }
}