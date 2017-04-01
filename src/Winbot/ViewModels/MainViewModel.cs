using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Entities;
using Winbot.Executors;
using Winbot.Repositories;
using Winbot.Settings;
using Winbot.Utils;

namespace Winbot.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly IScenarioExecutor _scenarioExecutor;
        private readonly IScenarioBuilder _scenarioBuilder;
        private readonly IRepository<Scenario> _repository;


        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand<Scenario> ExecuteScenarioCommand { get; private set; }
        public RelayCommand<Scenario> DeleteScenarioCommand { get; private set; }


        public AppSettings Settings { get; private set; }

        public ObservableCollection<Scenario> Scenarios { get; private set; }

        private Scenario _selectedScenario;
        public Scenario SelectedScenario
        {
            get { return _selectedScenario; }
            set
            {
                _selectedScenario = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Actions));
                ExecuteScenarioCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<UserAction> Actions => _selectedScenario != null ? _selectedScenario.Actions : Enumerable.Empty<UserAction>();

        private bool _isRecording;
        public bool IsRecording
        {
            get { return _isRecording;}
            set
            {
                _isRecording = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsIdle));
            }
        }

        private bool _isExecuting;
        public bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsIdle));
            }
        }

        public bool IsIdle => !_isExecuting && !_isRecording;

        public MainViewModel(AppSettings appSettings, IScenarioBuilder scenarioBuilder, IRepository<Scenario> repository, IScenarioExecutor scenarioExecutor)
        {
            Settings = appSettings;
            _scenarioBuilder = scenarioBuilder;
            _repository = repository;
            _scenarioExecutor = scenarioExecutor;

            ExecuteScenarioCommand = new RelayCommand<Scenario>(ExecuteScenario);
            DeleteScenarioCommand = new RelayCommand<Scenario>(DeleteScenario);
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);

            Scenarios = new ObservableCollection<Scenario>(_repository.GetAll().OrderByDescending(s => s.CreateTime));
            IsRecording = false;
        }

        private void Stop()
        {
            var scenario = _scenarioBuilder.Build();
            _repository.Add(scenario);
            Scenarios.Insert(0, scenario);
            IsRecording = false;
        }

        private void Start()
        {
            IsRecording = true;
            _scenarioBuilder.Init();
        }

        private async void ExecuteScenario(Scenario scenario)
        {
            Dispatcher.CurrentDispatcher.Invoke(() => IsExecuting = true, DispatcherPriority.Send);
            await Task.Run(() => _scenarioExecutor.Execute(scenario));
            Dispatcher.CurrentDispatcher.Invoke(() => IsExecuting = false, DispatcherPriority.Send);
        }

        private void DeleteScenario(Scenario scenario)
        {
            _repository.Delete(scenario);
            Scenarios.Remove(scenario);
        }
    }
}