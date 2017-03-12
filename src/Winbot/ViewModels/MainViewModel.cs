using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Views;

namespace Winbot.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public RelayCommand ShowSettingsCommand { get; private set; }
        public MainViewModel()
        {
            ShowSettingsCommand = new RelayCommand(ShowSettings);
        }

        private static void ShowSettings()
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}