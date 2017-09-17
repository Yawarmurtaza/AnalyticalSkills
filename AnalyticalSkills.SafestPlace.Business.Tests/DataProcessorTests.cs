using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using AnalyticalSkills.SafestPlace.DataAccess;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business.Tests
{
    [TestFixture]
    public class DataProcessorTests
    {
        private ICoordinatesManager mockCoordManager;
        private IDistanceCalculator mockDistCalc;

        [SetUp]
        public void Setup()
        {
            this.mockCoordManager = MockRepository.GenerateMock<ICoordinatesManager>();
            this.mockDistCalc = MockRepository.GenerateMock<IDistanceCalculator>();
        }

        [TearDown]
        public void TearDown()
        {
            this.mockCoordManager = null;
            this.mockDistCalc = null;
        }

        [Test]
        public void ProcessDataShould_return_test_number_distance_bomb_and_safest_point_location()
        {
            //
            // Arrange.
            //
            const string FileExtension = ".mockExtension";
            string sourceFileName = "myDataFile" + FileExtension;

            IEnumerable<TestData> testData = this.GetTestData();
            ReferencePointBombLocationDistance targetPoint = this.GetKeyRefPoint();
            IEnumerable<Point> discountedPoints = this.GetCoords(5);
            IEnumerable<Point> allCoords = this.GetCoords();
            
            this.mockDistCalc.Stub(
                distCalc =>
                    distCalc.GetNearestBombDistanceFromFurtherest(
                        Arg<IEnumerable<Point>>.Is.Equal(testData.First().BombLocations),
                        Arg<IEnumerable<Point>>.Is.Anything)).Return(targetPoint);

            this.mockCoordManager.Stub(
                    coordsMngr => coordsMngr.GetAllDiscountedPoints(targetPoint, testData.First().BombLocations, 10))
                .Return(discountedPoints);

            this.mockCoordManager.Stub(coordMngr => coordMngr.Generate3DCoords(10)).Return(allCoords);

            IDataProvider mockProvider = MockRepository.GenerateMock<IDataProvider>();
            mockProvider.Stub(p => p.Extension).Return(FileExtension);
            mockProvider.Stub(p => p.GetTestData(sourceFileName)).Return(testData);

            IDataProvider[] providers = { mockProvider };

            IDataProcessor processor = new DataProcessor(providers, this.mockDistCalc, this.mockCoordManager);

            //
            // Act.
            //

            IEnumerable<TestResult> result = processor.ProcessData(sourceFileName);

            //
            // Assert.
            //
            this.mockDistCalc.AssertWasCalled(distCalc =>
                   distCalc.GetNearestBombDistanceFromFurtherest(
                       Arg<IEnumerable<Point>>.Is.Equal(testData.First().BombLocations),
                       Arg<IEnumerable<Point>>.Is.Anything));
            this.mockDistCalc.AssertWasNotCalled(
                distCalc => distCalc.CalculateSquaredDistance(Arg<Point>.Is.Anything, Arg<Point>.Is.Anything));
            this.mockCoordManager.AssertWasCalled(coordsMngr => coordsMngr.GetAllDiscountedPoints(targetPoint, testData.First().BombLocations, 10));

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().TestNumber, Is.EqualTo(44));
            Assert.That(result.First().BombLocation.X, Is.EqualTo(4));
            Assert.That(result.First().BombLocation.Y, Is.EqualTo(4));
            Assert.That(result.First().BombLocation.Z, Is.EqualTo(4));
            Assert.That(result.First().Distance, Is.EqualTo(120));

            Assert.That(result.First().ReferencePoint.X, Is.EqualTo(10));
            Assert.That(result.First().ReferencePoint.Y, Is.EqualTo(10));
            Assert.That(result.First().ReferencePoint.Z, Is.EqualTo(10));

        }

        private IEnumerable<Point> GetCoords(int limit = 10)
        {
            List<Point> discountedPoints = new List<Point>();

            for (int z = 0; z < limit; z++)
                for (int y = 0; y < limit; y++)
                    for (int x = 0; x < limit; x++)
                    {
                discountedPoints.Add(new Point() { X = x , Y = y +1, Z = z  });
            }

            return discountedPoints;
        }

        private ReferencePointBombLocationDistance GetKeyRefPoint()
        {
            return new ReferencePointBombLocationDistance()
            {
                   ReferencePoint = new Point() { X = 10, Y = 10, Z = 10},
                   BombLocation = new Point() { X = 4, Y = 4, Z = 4 },
                   Distance = 120
            };
        }

        private IEnumerable<TestData> GetTestData()
        {
            return new List<TestData>()
            {
                new TestData(){TestNumber = 44, BombLocations = new List<Point>()
                {
                    new Point(){ X = 5, Y = 6, Z = 7 }
                }}
            };
        }
    }
}
