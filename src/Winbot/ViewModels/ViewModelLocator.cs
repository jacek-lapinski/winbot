using Winbot.Infrastructure.Ninject;

namespace Winbot.ViewModels
{
    internal class ViewModelLocator
    {
        public MainViewModel Main => NinjectBootstrapper.Get<MainViewModel>();
    }
}