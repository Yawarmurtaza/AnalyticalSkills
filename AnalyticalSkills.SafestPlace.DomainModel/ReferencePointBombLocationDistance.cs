namespace AnalyticalSkills.SafestPlace.DomainModel
{
    /// <summary> Represents the bomb, reference point and their distance in the cube. </summary>
    public class ReferencePointBombLocationDistance
    {
        /// <summary>
        ///     Gets or sets the reference point in the cube.
        /// </summary>
        public Point ReferencePoint { get; set; }

        /// <summary> Gets or sets the location of the nearest bomb. </summary>
        public Point BombLocation { get; set; }

        /// <summary>
        ///     Gets or sets the distance between the bomb and the reference point.
        /// </summary>
        public double Distance { get; set; }
    }
}