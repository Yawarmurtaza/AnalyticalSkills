using System.Collections.Generic;
using System.Linq;
using AnalyticalSkills.SafestPlace.DomainModel;
using NUnit.Framework;

namespace AnalyticalSkills.SafestPlace.Business.Tests
{
    [TestFixture]
    public class CoordinatesManagerTests
    {
        private IEnumerable<Point> GetBombLocations()
        {
            return new List<Point>
            {
                new Point {X = 3, Y = 3, Z = 3}
            };
        }

        [Test]
        public void Generate3DCoords_Should_generate_coordinates_uptil_given_limit()
        {
            //
            // Arrange.
            //
            const int MaxCoords = 10;
            ICoordinatesManager coordsGanerator = new CoordinatesManager();

            //
            // Act.
            //

            var coords = coordsGanerator.Generate3DCoords(MaxCoords);

            //
            // Assert.
            //

            // total coords must be 1331 for 10 X 10 X 10 cube
            Assert.That(coords.Count(), Is.EqualTo(1331));
            // there should only be 11 x
            Assert.That(coords.Count(c => c.Y == 0 && c.Z == 0), Is.EqualTo(11));

            // there should only be 11 y
            Assert.That(coords.Count(c => c.X == 0 && c.Z == 0), Is.EqualTo(11));

            // there should only be 11 z
            Assert.That(coords.Count(c => c.X == 0 && c.Z == 0), Is.EqualTo(11));
        }


        [Test]
        public void GetAllDiscardedPoints_should_discount_all_coordinates_that_occure_within_bomb_sphares()
        {
            //
            // Arrange.
            //
            const int MaxCoords = 10;
            var targetLocation = new ReferencePointBombLocationDistance
            {
                ReferencePoint = new Point {X = 10, Y = 10, Z = 10},
                BombLocation = new Point {X = 3, Y = 3, Z = 3},
                Distance = 147
            };

            var bombLocations = GetBombLocations();
            ICoordinatesManager manager = new CoordinatesManager();

            //
            // Act.
            //

            IEnumerable<Point> discountedCoords =
                manager.GetAllDiscardedPoints(targetLocation, bombLocations, MaxCoords).ToList();

            //
            // Assert.
            //

            Assert.That(discountedCoords.Contains(targetLocation.BombLocation, new PointComparer()), Is.True);
            //because the radius is 12, the starting coordinates must be discounted.
            Assert.That(discountedCoords.Contains(new Point {Y = 0, X = 0, Z = 0}, new PointComparer()), Is.True);

            Assert.That(discountedCoords.Contains(targetLocation.ReferencePoint, new PointComparer()), Is.False);
            Assert.That(discountedCoords.Contains(new Point {Y = 9, X = 10, Z = 9}, new PointComparer()), Is.False);
        }
    }
}