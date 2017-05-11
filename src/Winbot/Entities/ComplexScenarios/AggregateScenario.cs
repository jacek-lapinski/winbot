using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Winbot.Entities.ComplexScenarios
{
    [Serializable]
    public class AggregateScenario : Scenario
    {
        [XmlArrayItem(typeof(Scenario), ElementName = nameof(Scenario))]
        [XmlArrayItem(typeof(AggregateScenario), ElementName = nameof(AggregateScenario))]
        [XmlArrayItem(typeof(ScenarioRepeat), ElementName = nameof(ScenarioRepeat))]
        [XmlArrayItem(typeof(ScenarioRepeatFor), ElementName = nameof(ScenarioRepeatFor))]
        public List<Scenario> Scenarios { get; set; }

        public AggregateScenario() : this(Enumerable.Empty<Scenario>())
        {
            
        }

        public AggregateScenario(IEnumerable<Scenario> scenarios)
        {
            Scenarios = new List<Scenario>(scenarios);
        }

        public override IEnumerable<UserAction> GetExecutingActions()
        {
            foreach (var scenario in Scenarios)
            {
                foreach (var action in scenario.GetExecutingActions())
                {
                    yield return action;
                }
            }
        }
    }
}
