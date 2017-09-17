using System;
using System.Collections.Generic;
using System.Reflection;
using AnalyticalSkills.SafestPlace.Business;
using AnalyticalSkills.SafestPlace.DomainModel;
using AnalyticalSkills.SafestPlaceRunner.Infrastructure;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;

namespace AnalyticalSkills.SafestPlaceRunner
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            Logger.InfoFormat("******************[ Safest place in galaxy has started ]******************");

            try
            {

                SafestPlaceInGalaxyIoCConfigurator.ConfigureIoC();
                var processor = IoCWrapper.Instance().Resolve<IDataProcessor>();

                IEnumerable<TestResult> results = processor.ProcessData(StaticConfiguration.TestDataFilePath);

                foreach (TestResult nextResult in results)
                {
                    Console.WriteLine(
                        $"Test Number = {nextResult.TestNumber}, Square of the distance = {nextResult.Distance}, Bomb Location = {nextResult.BombLocation}, Safest Point = {nextResult.ReferencePoint}");
                }


                Logger.InfoFormat("******************[ Safest place in galaxy has finished ]******************");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("A fatel error occured during the execution of application. Details: {0}", ex);
            }
        }
    }
}