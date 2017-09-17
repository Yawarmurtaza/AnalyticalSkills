using System.Collections.Generic;

namespace AnalyticalSkills.SafestPlace.DomainModel
{
    /// <summary> Represents the data that needs to be tested. It holds the bomb locations and the test number from the source. </summary>
    public class TestData
    {
        /// <summary> Gets or sets the test number. </summary>
        public int TestNumber { get; set; }

        /// <summary> Gets or sets the Bomb locations. </summary>
        public List<Point> BombLocations { get; set; }
    }
}