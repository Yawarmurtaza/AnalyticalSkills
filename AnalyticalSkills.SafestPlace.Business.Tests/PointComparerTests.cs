using System.Collections;
using AnalyticalSkills.SafestPlace.DomainModel;
using NUnit.Framework;

namespace AnalyticalSkills.SafestPlace.Business.Tests
{
    [TestFixture]
    public class PointComparerTests
    {
        [TestCase(3,10,100)]
        [TestCase(30, 100, 1000)]
        [TestCase(0, 0, 0)]
        public void Equals_should_Return_true_when_two_point_object_values_are_the_same(int x, int y, int z)
        {
            //
            // Arrange.
            //

            Point p1 = new Point() { X = x, Y = y, Z = z};
            Point p2 = new Point() { X = x, Y = y, Z = z };
            
            PointComparer comparer = new PointComparer();

            //
            // Act.
            //

            bool result = comparer.Equals(p1, p2);

            //
            // Assert.
            //

            Assert.That(result, Is.True);
        }


        [TestCaseSource(nameof(SamePointObjectReferenceSource))]
        public void Equals_should_Return_true_when_two_point_object_references_are_the_same(Point p1, Point p2)
        {
            //
            // Arrange.
            //

          

            PointComparer comparer = new PointComparer();

            //
            // Act.
            //

            bool result = comparer.Equals(p1, p2);

            //
            // Assert.
            //

            Assert.That(result, Is.True);
        }

        [TestCaseSource(nameof(NullPointObjectsSource))]
        public void Equals_should_return_false_when_any_point_object_is_null(Point p1, Point p2)
        {
            //
            // Arrange.
            //

            PointComparer comparer = new PointComparer();

            //
            // Act.
            //

            bool result = comparer.Equals(p1, p2);

            //
            // Assert.
            //

            Assert.That(result, Is.False);
        }

        [TestCase(3, 10, 100)]
        [TestCase(30, 100, 1000)]
        [TestCase(0, 0, 0)]
        public void Equals_should_Return_false_when_two_point_object_values_are_different(int x, int y, int z)
        {
            //
            // Arrange.
            //

            Point p1 = new Point() { X = x, Y = y, Z = z };
            Point p2 = new Point() { X = x + 1, Y = y + 2, Z = z + 3 };

            PointComparer comparer = new PointComparer();

            //
            // Act.
            //

            bool result = comparer.Equals(p1, p2);

            //
            // Assert.
            //

            Assert.That(result, Is.False);
        }

        [TestCase(1, 2, 3, 6)]
        [TestCase(6, 6, 6, 18)]
        [TestCase(1000, 1000, 1000, 3000)]
        [TestCase(0, 0, 0, 0)]
        public void GetHashCode_should_return_the_sum_of_the_values_of_x_y_and_z(int x, int y, int z, int expectedHashCode)
        {
            //
            // Arrange.
            //
            PointComparer comparer = new PointComparer();
            Point p = new Point() { X = x, Y = y, Z = z };


            //
            // Act.
            //
            int result = comparer.GetHashCode(p);

            //
            // Assert.
            //
            Assert.That(result, Is.EqualTo(expectedHashCode));
        }
        private IEnumerable SamePointObjectReferenceSource()
        {
            yield return new TestCaseData(null, null);
            Point p1 = new Point()
            {
                X = 4,
                Y = 8,
                Z = 10
            };

            Point p2 = p1;
            p2.Y = 100;
            yield return new TestCaseData(p1, p2);
            yield return new TestCaseData(p1, p1);
            yield return new TestCaseData(p2, p2);


        }

        private IEnumerable NullPointObjectsSource()
        {
            yield return new TestCaseData(new Point(), null);
            yield return new TestCaseData(null, new Point());
        }

    }
}