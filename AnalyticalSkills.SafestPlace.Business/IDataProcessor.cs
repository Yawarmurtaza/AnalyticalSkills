using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Provides the functionality that processes the data. </summary>
    public interface IDataProcessor
    {
        /// <summary> Finds a point in the cube that has maximum distance to the nearest bomb. </summary>
        /// <param name="dataFilePath">Path to the test data file.</param>
        /// <returns></returns>
        IEnumerable<TestResult> ProcessData(string dataFilePath);
    }
}