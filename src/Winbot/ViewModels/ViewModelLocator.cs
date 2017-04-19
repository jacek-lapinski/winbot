using Winbot.Infrastructure.Ninject;

namespace Winbot.ViewModels
{
    internal class ViewModelLocator
    {
        public ViewModelLocator()
        {
            NinjectBootstrapper.Initialize();
        }

        public MainViewModel Main => NinjectBootstrapper.Get<MainViewModel>();
    }
}