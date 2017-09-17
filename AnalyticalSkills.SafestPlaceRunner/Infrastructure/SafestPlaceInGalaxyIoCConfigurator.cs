using System.Reflection;
using AnalyticalSkills.SafestPlace.Business;
using AnalyticalSkills.SafestPlace.DataAccess;
using log4net;
using Microsoft.Practices.Unity;

namespace AnalyticalSkills.SafestPlaceRunner.Infrastructure
{
    /// <summary> The IoC configurator for Safest place in the galaxy challenge. </summary>
    public class SafestPlaceInGalaxyIoCConfigurator
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary> Registers the types with their respective interfaces. </summary>
        public static void ConfigureIoC()
        {
            IoCWrapper.Instance().RegisterType<IFileReadonlyAccess, FileReadOnlyAccess>();
            IoCWrapper.Instance().RegisterType<ICoordinatesManager, CoordinatesManager>();
            IoCWrapper.Instance().RegisterType<IDataProcessor, DataProcessor>();
            IoCWrapper.Instance().RegisterType<IDistanceCalculator, DistanceCalculator>();
            IoCWrapper.Instance().RegisterType<IDataProvider, TextFileDataProvider>(".txt");
            IoCWrapper.Instance().RegisterType<IFileReadonlyAccess, FileReadOnlyAccess>();


            Logger.InfoFormat("Dependencies registered successfully.");
        }
    }
}