using System;

namespace AnalyticalSkills.Programs.AddingThisAddingThat
{
    /// <summary> Represents the Exercise 3 – Adding this, adding that quesion from the technical test. </summary>
    public class Base256Calculator : IBase256Calculator
    {
        private const int Base = 256;

        /// <summary> Adds 2 byte arrays together. </summary>
        /// <param name="first">First array.</param>
        /// <param name="second">Second array.</param>
        /// <returns>Result array containing the sum of the two.</returns>
        public byte[] AddRecursive(byte[] first, byte[] second)
        {
            var result = new byte[first.Length];
            return RecursiveAdd(first, second, first.Length - 1, result);
        }

        /// <summary>
        ///     This is another version of achieving the same result. This method however doesnt call anyother method than itself.
        /// </summary>
        /// <param name="first">First array.</param>
        /// <param name="second">Second array.</param>
        /// <param name="index">Starting index of the array to start the addition. Last item first.</param>
        /// <param name="result">Partial result of the addition.</param>
        /// <param name="carry">Any carry value.</param>
        /// <returns>Result.</returns>
        public byte[] AddRecursiveV2(byte[] first, byte[] second, int index = -2, byte[] result = null, int carry = 0)
        {
            if (index == -1)
            {
                if (carry != 1)
                    return result;

                var carryResult = new byte[result.Length + 1];

                result.CopyTo(carryResult, 1);
                carryResult[0] = 1;
                return carryResult;
            }

            if (index == -2)
                index = first.Length - 1;

            if (result == null)
                result = new byte[first.Length];

            int sum = first[index] + second[index] + carry;
            carry = Math.DivRem(sum, Base, out int remainder);
            result[index] = (byte) remainder;
            return AddRecursiveV2(first, second, index - 1, result, carry);
        }

        private byte[] RecursiveAdd(byte[] first, byte[] second, int index, byte[] result, int carry = 0)
        {
            if (index < 0)
            {
                if (carry != 1)
                    return result;

                var carryResult = new byte[result.Length + 1];

                result.CopyTo(carryResult, 1);
                carryResult[0] = 1;
                return carryResult;
            }

            int sum = first[index] + second[index] + carry;
            carry = Math.DivRem(sum, Base, out int remainder);
            result[index] = (byte) remainder;
            return RecursiveAdd(first, second, index - 1, result, carry);
        }
    }
}