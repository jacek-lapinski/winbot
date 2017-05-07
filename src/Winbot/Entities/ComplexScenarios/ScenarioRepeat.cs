using System;
using System.Collections.Generic;

namespace Winbot.Entities.ComplexScenarios
{
    [Serializable]
    public class ScenarioRepeat : Scenario
    {
        public Scenario Scenario { get; set; }
        public int Times { get; set; }

        public override IEnumerable<UserAction> GetExecutingActions()
        {
            for (var n = 0; n < Times; n++)
            {
                foreach (var action in Scenario.GetExecutingActions())
                {
                    yield return action;
                }
            }
        }
    }
}
