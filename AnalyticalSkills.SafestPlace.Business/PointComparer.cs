using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Compares the two points. </summary>
    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point p1, Point p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
                return false;

            return p1.Z.Equals(p2.Z) && p1.Y.Equals(p2.Y) && p1.X.Equals(p2.X);
        }

        public int GetHashCode(Point point)
        {
            int hashCode = point.Y.GetHashCode() + point.X.GetHashCode() + point.Z.GetHashCode();
            return hashCode;
        }
    }
}