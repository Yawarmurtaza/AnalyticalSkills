using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Provides the distance calculation. </summary>
    public interface IDistanceCalculator
    {
        /// <summary> Calcualtes the squared distance between 2 points in a 3 dimensional cartesian coordinates.  </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns>Distance between 2 points.</returns>
        int CalculateSquaredDistance(Point p1, Point p2);

        /// <summary>
        ///     Find the distance from the mine for 8 corners of the cube and the centre. We are interested in the point that has
        ///     furtherest bomb with shortest radius.
        /// </summary>
        /// <param name="bombLocations">List the locations of all bombs in the cube.</param>
        /// <param name="baseCoords">Coordinates to calculate the distance from.</param>
        /// <returns>A reference point with </returns>
        ReferencePointBombLocationDistance GetNearestBombDistanceFromFurtherest(IEnumerable<Point> bombLocations,
            IEnumerable<Point> baseCoords);
    }
}