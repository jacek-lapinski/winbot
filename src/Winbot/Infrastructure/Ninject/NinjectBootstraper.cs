using Ninject;

namespace Winbot.Infrastructure.Ninject
{
    internal class NinjectBootstraper
    {
        private readonly IKernel _kernel;

        public NinjectBootstraper()
        {
            _kernel = new StandardKernel();
        }

        public void Initialize()
        {
            _kernel.Load<DesktopAppModule>();
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
