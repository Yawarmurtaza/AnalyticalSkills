using System;
using System.Configuration;

namespace AnalyticalSkills.SafestPlace.DomainModel
{
    /// <summary> Represents the app settings for safest place application. </summary>
    public static class StaticConfiguration
    {
        /// <summary> Gets the full file path for the test data file that contains test numbers and bomb locations. </summary>
        public static string TestDataFilePath
        {
            get
            {
                string filePath = ConfigurationManager.AppSettings["TestDataFilePath"];
                if (!string.IsNullOrEmpty(filePath) || !string.IsNullOrWhiteSpace(filePath))
                {
                    return filePath;
                }

                throw new ArgumentException("TestDataFilePath not found or invalid in the appSettings.config file.");
            }
        }
    }
}