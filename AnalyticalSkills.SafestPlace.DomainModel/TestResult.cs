namespace AnalyticalSkills.SafestPlace.DomainModel
{
    /// <summary> Represents the test results. </summary>
    public class TestResult : ReferencePointBombLocationDistance
    {
        /// <summary> Gets or sets the test number. </summary>
        public int TestNumber { get; set; }
    }
}