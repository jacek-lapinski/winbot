using System.Collections.Generic;
using Ninject.Infrastructure.Language;
using Winbot.Entities;

namespace Winbot.Repositories
{
    internal class InMemoryRepository
    {
        private readonly IList<Scenario> _scenarios;

        public InMemoryRepository()
        {
            _scenarios = new List<Scenario>();
        }

        public void Add(Scenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public IEnumerable<Scenario> GetAll()
        {
            return _scenarios.ToEnumerable();
        }
    }
}
