using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Entities;
using Winbot.Repositories;
using Winbot.Settings;
using Winbot.Utils;
using Winbot.Views;

namespace Winbot.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly AppSettings _appSettings;
        private readonly IScenarioBuilder _scenarioBuilder;
        private readonly IRepository<Scenario> _repository;

        public RelayCommand ShowSettingsCommand { get; private set; }
        public RelayCommand ShowScenariosCommand { get; private set; }
        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public string StartShortcut => _appSettings.StartShortcut;
        public string StopShortcut => _appSettings.StopShortcut;
        public MainViewModel(AppSettings appSettings, IScenarioBuilder scenarioBuilder, IRepository<Scenario> repository)
        {
            _appSettings = appSettings;
            _scenarioBuilder = scenarioBuilder;
            _repository = repository;
            ShowSettingsCommand = new RelayCommand(ShowSettings);
            ShowScenariosCommand= new RelayCommand(ShowScenarios);
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
        }

        private void Stop()
        {
            var scenario = _scenarioBuilder.Build();
            _repository.Add(scenario);
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