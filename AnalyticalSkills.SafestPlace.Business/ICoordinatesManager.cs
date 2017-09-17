using System.Collections.Generic;
using AnalyticalSkills.SafestPlace.DomainModel;

namespace AnalyticalSkills.SafestPlace.Business
{
    /// <summary> Allows the creation and calculation of coordinates. </summary>
    public interface ICoordinatesManager
    {
        /// <summary>
        ///     Generates all coordinates starting from 0,0,0 till given limit.
        /// </summary>
        /// <param name="maxLength">Limit of the dimensions.</param>
        /// <returns>All coordinates.</returns>
        IEnumerable<Point> Generate3DCoords(int maxLength = 1000);

        /// <summary>
        ///     We are not interested in thos points that are within the radius of shorted distance. This is because we already
        ///     know a possible safest point.
        /// </summary>
        /// <param name="targetPoint">Point from which we have shorted radius.</param>
        /// <param name="bombLocations">List the locations of all bombs in the cube.</param>
        /// <param name="endCoordinateValue">End limit of the coordinates.</param>
        /// <returns>Discounted points.</returns>
        IEnumerable<Point> GetAllDiscardedPoints(ReferencePointBombLocationDistance targetPoint, IEnumerable<Point> bombLocations, int endCoordinateValue = 1000);
    }
}