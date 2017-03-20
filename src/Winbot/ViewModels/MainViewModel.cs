using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Repositories;
using Winbot.Settings;
using Winbot.Utils;
using Winbot.Views;

namespace Winbot.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly AppSettings _appSettings;
        private readonly InMemoryRepository _inMemoryRepository;
        private readonly IScenarioBuilder _scenarioBuilder;

        public RelayCommand ShowSettingsCommand { get; private set; }
        public RelayCommand ShowScenariosCommand { get; private set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public string StartShortcut => _appSettings.StartShortcut;
        public string StopShortcut => _appSettings.StopShortcut;
        public MainViewModel(AppSettings appSettings, InMemoryRepository inMemoryRepository, IScenarioBuilder scenarioBuilder)
        {
            _appSettings = appSettings;
            _scenarioBuilder = scenarioBuilder;
            _inMemoryRepository = inMemoryRepository;
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            ShowScenariosCommand= new RelayCommand(ShowScenarios);
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
        }

        private void Stop()
        {
            var scenario = _scenarioBuilder.Build();
            _inMemoryRepository.Add(scenario);
        }

        private void Start()
        {
            _scenarioBuilder.Init();
        }

        private static void ShowSettings()
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private static void ShowScenarios()
        {
            var scenariosWindow = new ScenariosWindow();
            scenariosWindow.ShowDialog();
        }
    }
}