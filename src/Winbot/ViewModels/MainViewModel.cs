using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Entities;
using Winbot.Entities.ComplexScenarios;
using Winbot.Executors;
using Winbot.Repositories;
using Winbot.Settings;
using Winbot.Utils;
using Winbot.Views;

namespace Winbot.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly IScenarioExecutor _scenarioExecutor;
        private readonly IScenarioBuilder _scenarioBuilder;
        private readonly IRepository<Scenario> _repository;
        private readonly IScenarioFileManager _scenarioFileManager;


        public RelayCommand StartCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand<Scenario> ExecuteScenarioCommand { get; private set; }
        public RelayCommand<Scenario> DeleteScenarioCommand { get; private set; }
        public RelayCommand<Scenario> EditScenarioCommand { get; private set; }
        public RelayCommand<Scenario> ExportScenarioCommand { get; private set; }
        public RelayCommand CreateAggregateScenarioCommand { get; private set; }
        public RelayCommand AddAggregateScenarioCommand { get; private set; }

        public string AppName => $"Winbot {Assembly.GetExecutingAssembly().GetName().Version}";

        public AppSettings Settings { get; private set; }
        public DatabaseSettings DbSettings { get; private set; }

        private ObservableCollection<Scenario> _scenarios;
        public ObservableCollection<Scenario> Scenarios
        {
            get { return _scenarios;}
            set
            {
                _scenarios = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Scenario> _aggregateScenarios;
        public ObservableCollection<Scenario> AggregateScenarios
        {
            get { return _aggregateScenarios; }
            set
            {
                _aggregateScenarios = value;
                RaisePropertyChanged();
                CreateAggregateScenarioCommand.RaiseCanExecuteChanged();
            }
        }

        private Scenario _aggregateScenario;
        public Scenario AggregateScenario
        {
            get { return _aggregateScenario;}
            set
            {
                _aggregateScenario = value;
                RaisePropertyChanged();
                AddAggregateScenarioCommand.RaiseCanExecuteChanged();
            }
        }

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

        public MainViewModel(AppSettings appSettings, DatabaseSettings dbSettings, IScenarioBuilder scenarioBuilder, IRepository<Scenario> repository, IScenarioExecutor scenarioExecutor, IScenarioFileManager scenarioFileManager)
        {
            Settings = appSettings;
            DbSettings = dbSettings;
            _scenarioBuilder = scenarioBuilder;
            _repository = repository;
            _scenarioExecutor = scenarioExecutor;
            _scenarioFileManager = scenarioFileManager;

            _aggregateScenarios = new ObservableCollection<Scenario>();
            _aggregateScenarios.CollectionChanged +=
                (sender, args) => CreateAggregateScenarioCommand.RaiseCanExecuteChanged();

            ExecuteScenarioCommand = new RelayCommand<Scenario>(ExecuteScenario);
            DeleteScenarioCommand = new RelayCommand<Scenario>(DeleteScenario);
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            EditScenarioCommand = new RelayCommand<Scenario>(EditScenario);
            ExportScenarioCommand = new RelayCommand<Scenario>(ExportScenario);
            CreateAggregateScenarioCommand = new RelayCommand(CreateAggregateScenario, AnyAggregateScenarios);
            AddAggregateScenarioCommand = new RelayCommand(AddAggregateScenario, IsAggregateScenario);  

            DbSettings.DbFilePathChanged += DbSettings_DbFilePathChanged;
            LoadScenarios();
            IsRecording = false;
        }

        private void AddAggregateScenario()
        {
            AggregateScenarios.Add(AggregateScenario);
        }

        private bool IsAggregateScenario()
        {
            return AggregateScenario != null;
        }

        private void CreateAggregateScenario()
        {
            var aggregateScenario = new AggregateScenario(AggregateScenarios)
            {
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
            };
            _repository.Add(aggregateScenario);
        }

        private bool AnyAggregateScenarios()
        {
            return AggregateScenarios != null && AggregateScenarios.Any();
        }

        private void LoadScenarios()
        {
            Scenarios = new ObservableCollection<Scenario>(_repository.GetAll().OrderByDescending(s => s.CreateTime));
        }

        private void DbSettings_DbFilePathChanged(object sender, EventArgs e)
        {
            LoadScenarios();
        }

        private void ExportScenario(Scenario scenario)
        {
            _scenarioFileManager.Save(scenario);
        }

        private void EditScenario(Scenario scenario)
        {
            var onSave = new Action<ICloneable>((s) =>
            {
                var updatedScenario = (Scenario) s;
                updatedScenario.UpdateTime = DateTime.Now;
                _repository.Update(updatedScenario);
                var index = Scenarios.IndexOf(scenario);
                Scenarios.Remove(scenario);
                Scenarios.Insert(index, updatedScenario);
                SelectedScenario = updatedScenario;
            });
            var dialog = new DialogEditorWindow(scenario, onSave);
            dialog.ShowDialog();
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