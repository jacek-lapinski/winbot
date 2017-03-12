using GalaSoft.MvvmLight;
using Winbot.Settings;

namespace Winbot.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        public AppSettings Settings { get; private set; }

        public SettingsViewModel(AppSettings settings)
        {
            Settings = settings;
        }
    }
}
