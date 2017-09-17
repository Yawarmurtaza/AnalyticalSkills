using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.DataAccess
{
    /// <summary> Responsible for providing the test from the source. </summary>
    public interface IDataProvider
    {
        /// <summary> Gets the file extension. </summary>
        string Extension { get; }

        /// <summary> Returns the test data from the source file. </summary>
        /// <param name="path">Path to the souece file.</param>
        /// <returns>Test data.</returns>
        IEnumerable<TestData> GetTestData(string path);
    }
}