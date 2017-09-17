namespace AnalyticalSkills.Programs.AddingThisAddingThat
{
    /// <summary> Represents the Exercise 3 – Adding this, adding that quesion from the technical test. </summary>
    public interface IBase256Calculator
    {
        /// <summary> Adds 2 byte arrays together. </summary>
        /// <param name="first">First array.</param>
        /// <param name="second">Second array.</param>
        /// <returns>Result array containing the sum of the two.</returns>
        byte[] AddRecursive(byte[] first, byte[] second);

        /// <summary>
        ///     This is another version of achieving the same result. This method however doesnt call anyother method than itself.
        /// </summary>
        /// <param name="first">First array.</param>
        /// <param name="second">Second array.</param>
        /// <param name="index">Starting index of the array to start the addition. Last item first.</param>
        /// <param name="result">Partial result of the addition.</param>
        /// <param name="carry">Any carry value.</param>
        /// <returns>Result.</returns>
        byte[] AddRecursiveV2(byte[] first, byte[] second, int index = -2, byte[] result = null, int carry = 0);
    }
}