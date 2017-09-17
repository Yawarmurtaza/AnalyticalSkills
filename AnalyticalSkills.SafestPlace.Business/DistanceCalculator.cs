using System;
using System.Collections.Generic;
using System.Linq;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Provides the distance calculation. </summary>
    public class DistanceCalculator : IDistanceCalculator
    {
        /// <summary> Calcualtes the squared distance between 2 points in a 3 dimensional cartesian coordinates.  </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns>Distance between 2 points.</returns>
        public int CalculateSquaredDistance(Point p1, Point p2)
        {
            return (int)(Math.Pow(p2.X - p1.X, 2) +
                          Math.Pow(p2.Y - p1.Y, 2) +
                          Math.Pow(p2.Z - p1.Z, 2));
        }

        /// <summary>
        ///     Find the distance from the mine for 8 corners of the cube and the centre. We are interested in the point that has
        ///     furtherest bomb with shortest radius.
        /// </summary>
        /// <param name="bombLocations">List the locations of all bombs in the cube.</param>
        /// <param name="baseCoords">Coordinates to calculate the distance from.</param>
        /// <returns>A reference point with </returns>
        public ReferencePointBombLocationDistance GetNearestBombDistanceFromFurtherest(IEnumerable<Point> bombLocations,
            IEnumerable<Point> baseCoords)
        {
            IList<PointDistance> pointDistnaces = new List<PointDistance>();
            // loop through each base coordinate / reference point for all bombs
            // to find out the distances that will be used further down.
            foreach (Point nextCorner in baseCoords)
            {
                var pd = new PointDistance
                {
                    BombDistanceList = new List<BombDistance>(),
                    ReferencePoint = nextCorner
                };

                foreach (Point bombLocation in bombLocations)
                {
                    // get the distance between the base coordinate and the bomb location.
                    int dist = CalculateSquaredDistance(nextCorner, bombLocation);
                    var md = new BombDistance
                    {
                        BombLocation = bombLocation,
                        Distance = dist
                    };

                    // add all of them into a list.
                    pd.BombDistanceList.Add(md);
                }

                pointDistnaces.Add(pd);
            }

            // find the base coordinate with furtherest bomb location
            int maxDist = pointDistnaces.SelectMany(pd => pd.BombDistanceList).Max(md => md.Distance);

            // we need the base coordinate or reference point that has furtherest bomb distance.
            PointDistance targetRefPoint = (from pd in pointDistnaces
                                            from md in pd.BombDistanceList
                                            where md.Distance == maxDist
                                            select pd).First();

            // now construct an object that contains selected base coordinate / reference point
            // and the nearest bomb distance, this nearest distance would be the radius
            // for all the sphares we will create on all bombs.
            ReferencePointBombLocationDistance rpbd = (from pd in pointDistnaces
                                                       from md in pd.BombDistanceList
                                                       where md.Distance == targetRefPoint.BombDistanceList.Min(m => m.Distance)
                                                             && pd.ReferencePoint == targetRefPoint.ReferencePoint
                                                       select new ReferencePointBombLocationDistance
                                                       {
                                                           ReferencePoint = targetRefPoint.ReferencePoint,
                                                           BombLocation = md.BombLocation,
                                                           Distance = md.Distance
                                                       }).First();

            return rpbd;
        }

        /// <summary> Defines the safe / reference point and the list of bombs. </summary>
        private class PointDistance
        {
            /// <summary> Gets or sets the reference point in the cube. </summary>
            public Point ReferencePoint { get; set; }

            /// <summary> Gets or sets the list of bomb distance. </summary>
            public List<BombDistance> BombDistanceList { get; set; }
        }

        /// <summary>
        ///     Represents the point and the distance from the nearest bomb.
        /// </summary>
        private class BombDistance
        {
            /// <summary> Gets or sets the distance from the bomb. </summary>
            public int Distance { get; set; }

            /// <summary> Gets or sets the location of the bomb. </summary>
            public Point BombLocation { get; set; }
        }
    }
}