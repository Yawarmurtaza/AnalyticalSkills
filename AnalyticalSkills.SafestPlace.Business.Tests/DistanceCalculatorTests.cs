using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;
using NUnit.Framework;

namespace AnalyticalSkills.SafestPlace.Business.Tests
{
    [TestFixture]
    public class DistanceCalculatorTests
    {
        [TestCase(3, 3, 3, 10, 10, 10, 147)]
        [TestCase(100, 150, 200, 999, 999, 999, 2167403)]
        [TestCase(0, 0, 0, 0, 0, 0, 0)]

        public void CalculateSquaredDistance_should_return_squared_distance_between_two_points(int x1, int y1, int z1,
            int x2, int y2, int z2, int expectedDist)
        {
            //
            // Arrange.
            //

            var p1 = new Point {X = x1, Y = y1, Z = z1};
            var p2 = new Point {X = x2, Y = y2, Z = z2};

            IDistanceCalculator calc = new DistanceCalculator();

            //
            // Act.
            //

            int result = calc.CalculateSquaredDistance(p1, p2);

            //
            // Assert.
            //

            Assert.That(result, Is.EqualTo(expectedDist));
        }

        [Test]
        public void GetNearestBombDistanceFromFurtherest_should_return_the_nearest_ref_point_and_bomb_location()
        {
            //
            // Arrange.
            //

            IEnumerable<Point> bombLocations = new List<Point>
            {
                new Point {X = 3, Y = 3, Z = 3},
                new Point {X = 1, Y = 1, Z = 1},
                new Point {X = 4, Y = 4, Z = 4},
                new Point {X = 6, Y = 6, Z = 6}
                // this bomb is should be picked up because this is the one which is the nearest from 10,10,10 for 3,3,3 being furtherest.
            };

            var baseCoords = GetBaseCoords(10);

            IDistanceCalculator calc = new DistanceCalculator();

            //
            // Act.
            //

            var refPointDist = calc.GetNearestBombDistanceFromFurtherest(bombLocations, baseCoords);

            //
            // Assert.
            //

            Assert.That(refPointDist.Distance, Is.EqualTo(48));
            Assert.That(refPointDist.BombLocation.X, Is.EqualTo(6));
            Assert.That(refPointDist.BombLocation.Y, Is.EqualTo(6));
            Assert.That(refPointDist.BombLocation.Z, Is.EqualTo(6));

            Assert.That(refPointDist.ReferencePoint.X, Is.EqualTo(10));
            Assert.That(refPointDist.ReferencePoint.Y, Is.EqualTo(10));
            Assert.That(refPointDist.ReferencePoint.Z, Is.EqualTo(10));
        }

        private IEnumerable<Point> GetBaseCoords(int endCoordinateValue)
        {
            IEnumerable<Point> baseCoords = new List<Point>
            {
                // face of cube
                new Point {X = 0, Y = 0, Z = 0},
                new Point {X = endCoordinateValue, Y = 0, Z = 0},
                new Point {X = 0, Y = endCoordinateValue, Z = 0},
                new Point {X = endCoordinateValue, Y = endCoordinateValue, Z = 0},

                // back of cube
                new Point {X = 0, Y = 0, Z = endCoordinateValue},
                new Point {X = 0, Y = endCoordinateValue, Z = endCoordinateValue},
                new Point {X = endCoordinateValue, Y = 0, Z = endCoordinateValue},
                new Point {X = endCoordinateValue, Y = endCoordinateValue, Z = endCoordinateValue}
            };
            return baseCoords;
        }
    }
}