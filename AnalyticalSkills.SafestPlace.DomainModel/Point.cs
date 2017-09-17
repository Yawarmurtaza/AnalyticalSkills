namespace AnalyticalSkills.SafestPlace.DomainModel
{
    /// <summary> Represents a location in a 3 dimensional cartesian coordinates. </summary>
    public class Point
    {
        /// <summary> Gets or sets a value along X axis. </summary>
        public int X { get; set; }

        /// <summary> Gets or sets a value along Y axis. </summary>
        public int Y { get; set; }

        /// <summary> Gets or sets a value along Z axis. </summary>
        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}