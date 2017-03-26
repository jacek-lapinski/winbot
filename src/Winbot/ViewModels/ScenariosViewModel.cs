using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Winbot.Entities;
using Winbot.Executors;
using Winbot.Repositories;

namespace Winbot.ViewModels
{

    internal class ScenariosViewModel : ViewModelBase
    {
        private readonly IRepository<Scenario> _repository;
        private readonly IScenarioExecutor _scenarioExecutor;

        public IEnumerable<Scenario> Scenarios => _repository.GetAll();

        private Scenario _selectedScenario;

        public RelayCommand ExecuteScenarioCommand { get; private set; }

        public Scenario SelectedScenario
        {
            get { return _selectedScenario;}
            set
            {
                _selectedScenario = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Actions));
                ExecuteScenarioCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<UserAction> Actions => _selectedScenario != null ? _selectedScenario.Actions : Enumerable.Empty<UserAction>();

        public ScenariosViewModel(IRepository<Scenario> repository, IScenarioExecutor scenarioExecutor)
        {
            _repository = repository;
            _scenarioExecutor = scenarioExecutor;
            ExecuteScenarioCommand = new RelayCommand(ExecuteScenario, AnyScenarioSelected);
        }

        private bool AnyScenarioSelected()
        {
            return SelectedScenario != null;
        }

        private void ExecuteScenario()
        {
            _scenarioExecutor.Execute(SelectedScenario);
        }
    }
}
