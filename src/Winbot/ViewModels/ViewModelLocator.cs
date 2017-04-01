using Winbot.Infrastructure.Ninject;

namespace Winbot.ViewModels
{
    internal class ViewModelLocator
    {
        private readonly NinjectBootstrapper _bootstraper;
        public ViewModelLocator()
        {
            _bootstraper = new NinjectBootstrapper();
            _bootstraper.Initialize();
        }

        public MainViewModel Main => _bootstraper.Get<MainViewModel>();
    }
}