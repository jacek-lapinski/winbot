using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using Winbot.Entities;
using Winbot.Repositories;

namespace Winbot.ViewModels
{

    internal class ScenariosViewModel : ViewModelBase
    {
        private readonly InMemoryRepository _repository;

        public IEnumerable<Scenario> Scenarios => _repository.GetAll();

        private Scenario _selectedScenario;

        public Scenario SelectedScenario
        {
            get { return _selectedScenario;}
            set
            {
                _selectedScenario = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Actions));
            }
        }

        public IEnumerable<UserAction> Actions => _selectedScenario != null ? _selectedScenario.Actions : Enumerable.Empty<UserAction>();

        public ScenariosViewModel(InMemoryRepository repository)
        {
            _repository = repository;
        }
    }
}
