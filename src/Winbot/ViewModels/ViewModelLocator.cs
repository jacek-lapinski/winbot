using Winbot.Infrastructure.Ninject;

namespace Winbot.ViewModels
{
    internal class ViewModelLocator
    {
        private readonly NinjectBootstraper _bootstraper;
        public ViewModelLocator()
        {
            _bootstraper = new NinjectBootstraper();
            _bootstraper.Initialize();
        }

        public MainViewModel Main => _bootstraper.Get<MainViewModel>();
        public SettingsViewModel Settings => _bootstraper.Get<SettingsViewModel>();
    }
}