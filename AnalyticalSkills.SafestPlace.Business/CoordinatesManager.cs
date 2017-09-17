using System;
using System.Collections.Generic;
using System.Linq;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Allows the creation and calculation of coordinates. </summary>
    public class CoordinatesManager : ICoordinatesManager
    {
        /// <summary>
        ///     Generates all coordinates starting from 0,0,0 till given limit.
        /// </summary>
        /// <param name="maxLength">Limit of the dimensions.</param>
        /// <returns>All coordinates.</returns>
        public IEnumerable<Point> Generate3DCoords(int maxLength = 1000)
        {
            IList<Point> coords = new List<Point>();

            for (int z = 0; z < maxLength + 1; z++)
            for (int y = 0; y < maxLength + 1; y++)
            for (int x = 0; x < maxLength + 1; x++)
            {
                var p = new Point {X = x, Y = y, Z = z};
                coords.Add(p);
            }

            return coords;
        }

        /// <summary>
        ///     We are not interested in thos points that are within the radius of shorted distance. This is because we already
        ///     know a possible safest point.
        /// </summary>
        /// <param name="targetPoint">Point from which we have shorted radius.</param>
        /// <param name="bombLocations">List the locations of all bombs in the cube.</param>
        /// <param name="endCoordinateValue">End limit of the coordinates.</param>
        /// <returns>Discounted points.</returns>
        public IEnumerable<Point> GetAllDiscountedPoints(ReferencePointBombLocationDistance targetPoint, IEnumerable<Point> bombLocations, int endCoordinateValue = 1000)
        {
            int radious = (int) Math.Sqrt(targetPoint.Distance);
            var discountedCoords = new List<Point>();
            foreach (Point nextMine in bombLocations)
            {
                int startX = nextMine.X - radious;
                int endX = Math.Min(endCoordinateValue, nextMine.X + radious);
                int endY = Math.Min(endCoordinateValue, nextMine.Y + radious);
                int lowerBound = 0;
                int upperBoud = 0;
                int dir = 1;
                int uDir = -1;

                // we are creating a dimond shape that will contain all the coordinates that we  do not need to check for the safest point.
                // we are achieving this by increamenting the x axis while the y axis will be increasing its range of lower and upper bound
                // up until it reaches to the middle, then decreasing the bounds.
                for (int myX = startX; myX < endX + 1; myX++)
                {
                    // I am not sure if this is the correct way to evualate the z axis, this is where i am stuck.
                    for (int y = lowerBound + nextMine.Y, z = lowerBound + nextMine.Z;
                        y <= upperBoud + nextMine.Y && z <= upperBoud + nextMine.Z;
                        y++, z++)
                    {
                        var p = new Point {X = myX, Y = y, Z = z};
                        if (p.X > -1 && p.Y > -1 && p.Y <= endY)
                            if (!discountedCoords.Contains(p, new PointComparer()))
                                discountedCoords.Add(p);
                    }


                    // keep increasing until we reach to the middle of the diamond.
                    upperBoud += dir;
                    if (upperBoud == radious)
                        dir = -1;

                    lowerBound += uDir;
                    if (lowerBound == -radious)
                        uDir = 1;
                }
            }

            return discountedCoords;
        }
    }
}