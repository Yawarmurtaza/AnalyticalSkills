using Microsoft.Practices.Unity;

namespace AnalyticalSkills.SafestPlaceRunner.Infrastructure
{
    public static class IoCWrapper
    {
        private static IUnityContainer container;

        public static IUnityContainer Instance() => container ?? (container = new UnityContainer());
    }
}