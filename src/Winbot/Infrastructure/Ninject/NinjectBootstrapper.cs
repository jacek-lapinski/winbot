using Ninject;

namespace Winbot.Infrastructure.Ninject
{
    internal static class NinjectBootstrapper
    {
        private static readonly IKernel Kernel;

        static NinjectBootstrapper()
        {
            Kernel = new StandardKernel();
        }

        public static void Initialize()
        {
            Kernel.Load<DesktopAppModule>();
            Kernel.Load<ViewModelsModule>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
