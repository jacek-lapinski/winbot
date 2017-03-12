using Ninject;

namespace Winbot.Infrastructure.Ninject
{
    internal class NinjectBootstrapper
    {
        private readonly IKernel _kernel;

        public NinjectBootstrapper()
        {
            _kernel = new StandardKernel();
        }

        public void Initialize()
        {
            _kernel.Load<DesktopAppModule>();
            _kernel.Load<ViewModelsModule>();
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
