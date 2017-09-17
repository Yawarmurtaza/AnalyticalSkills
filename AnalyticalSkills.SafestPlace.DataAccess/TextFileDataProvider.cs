using System;
using System.Collections.Generic;
using System.Reflection;
using AnalyticalSkills.SafestPlace.DomainModel;
using log4net;

namespace AnalyticalSkills.SafestPlace.DataAccess
{
    /// <summary> Allows access to the text file containing the test data. </summary>
    public class TextFileDataProvider : IDataProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IFileReadonlyAccess fileAccess;

        public TextFileDataProvider()
        {
            fileAccess = new FileReadOnlyAccess();
        }


        public TextFileDataProvider(IFileReadonlyAccess fileAccess)
        {
            this.fileAccess = fileAccess;
        }

        public string Extension => ".txt";

        /// <summary> Returns the test data from a text file. </summary>
        /// <param name="sourceFilePath">Path to the text file.</param>
        /// <returns>Test data.</returns>
        public IEnumerable<TestData> GetTestData(string sourceFilePath)
        {
            ValidatePath(sourceFilePath);
            var allData = new List<TestData>();

            try
            {
                foreach (var line in fileAccess.ReadAllLines(sourceFilePath))
                {
                    var testData = new TestData();
                    var dataItems = line.Split(',');
                    testData.TestNumber = int.Parse(dataItems[0]);
                    var totalMines = int.Parse(dataItems[1]);
                    testData.BombLocations = new List<Point>();
                    for (var index = 2; index < totalMines * 3; index += 3)
                    {
                        var p = new Point();
                        p.X = int.Parse(dataItems[index]);
                        p.Y = int.Parse(dataItems[index + 1]);
                        p.Z = int.Parse(dataItems[index + 2]);
                        testData.BombLocations.Add(p);
                    }

                    allData.Add(testData);
                }
            }
            catch (IndexOutOfRangeException indexOutOfrangeEx)
            {
                Logger.ErrorFormat("Source file {0} has invalid data: Details {1}", sourceFilePath, indexOutOfrangeEx);
            }

            return allData;
        }

        private void ValidatePath(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new ArgumentNullException(nameof(sourceFilePath));

            if (!sourceFilePath.ToLower().EndsWith(".txt"))
                throw new NotSupportedException(
                    "Extension not supported by TextFileDataProvider. Supported extension is \".txt\"");
        }
    }
}