using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnalyticalSkills.SafestPlace.DataAccess;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Provides the functionality that processes the data. </summary>
    public class DataProcessor : IDataProcessor
    {
        private const int EndCoordinateValue = 10;

        private readonly ICoordinatesManager coordManager;
        private readonly IDistanceCalculator distCalc;

        private readonly IEnumerable<Point> allCoords;

        private readonly IEnumerable<Point> baseCoords = new List<Point>
        {
            // face of cube
            new Point {X = 0, Y = 0, Z = 0},
            new Point {X = EndCoordinateValue, Y = 0, Z = 0},
            new Point {X = 0, Y = EndCoordinateValue, Z = 0},
            new Point {X = EndCoordinateValue, Y = EndCoordinateValue, Z = 0},

            // back of cube
            new Point {X = 0, Y = 0, Z = EndCoordinateValue},
            new Point {X = 0, Y = EndCoordinateValue, Z = EndCoordinateValue},
            new Point {X = EndCoordinateValue, Y = 0, Z = EndCoordinateValue},
            new Point {X = EndCoordinateValue, Y = EndCoordinateValue, Z = EndCoordinateValue}
        };

        
        private readonly IDictionary<string, IDataProvider> dataProviderMap;

        public DataProcessor(IDataProvider[] dataProviders, IDistanceCalculator distCalc,
            ICoordinatesManager coordManager)
        {
            dataProviderMap = dataProviders.ToDictionary(p => p.Extension, p => p);
            this.distCalc = distCalc;
            this.coordManager = coordManager;
            allCoords = coordManager.Generate3DCoords(EndCoordinateValue);
        }

        /// <summary> Finds a point in the cube that has maximum distance to the nearest bomb. </summary>
        /// <param name="dataFilePath">Path to the test data file.</param>
        /// <returns>Test results from given test data file.</returns>
        public IEnumerable<TestResult> ProcessData(string dataFilePath)
        {
            // get the test data from the source 
            IEnumerable<TestData> allData = GetTestData(dataFilePath);

            IList<TestResult> testResults = new List<TestResult>();

            // loop through each test number and bomb locations.
            foreach (TestData testData in allData)
            {
                // Get the radius of the nearest bomb for the furtherest reference point.
                ReferencePointBombLocationDistance keyReferencePoint = distCalc.GetNearestBombDistanceFromFurtherest(testData.BombLocations, baseCoords);

                // discount all points within that radius.
                IEnumerable<Point> discountedPoints = coordManager.GetAllDiscountedPoints(keyReferencePoint, testData.BombLocations, EndCoordinateValue);

                // Now we only need to check those points outside the discounted.
                IEnumerable<Point> coordsToCheck = allCoords.Except(discountedPoints, new PointComparer());

                keyReferencePoint = distCalc.GetNearestBombDistanceFromFurtherest(testData.BombLocations, coordsToCheck);

                var tr = new TestResult
                {
                    ReferencePoint = keyReferencePoint.ReferencePoint,
                    BombLocation = keyReferencePoint.BombLocation,
                    Distance = keyReferencePoint.Distance,
                    TestNumber = testData.TestNumber
                };

                testResults.Add(tr);
            }

            return testResults;
        }


        /// <summary>
        ///     Returns the bomb locations and test number from the source file.
        /// </summary>
        /// <param name="sourceFilePath">Path to the source file containing the test data.</param>
        /// <returns>Test data object.</returns>
        private IEnumerable<TestData> GetTestData(string sourceFilePath)
        {
            IDataProvider provider = GetDataProvider(sourceFilePath);
            return provider.GetTestData(sourceFilePath);
        }

        /// <summary>
        ///     Gets the data provider that handles the calls to the data source.
        /// </summary>
        /// <param name="filePath">Soure file path with extension.</param>
        /// <returns>The data provider that allows access to the source test data.</returns>
        private IDataProvider GetDataProvider(string filePath)
        {
            var file = new FileInfo(filePath);
            return dataProviderMap[file.Extension];
        }
    }
}