using System;
using System.Collections.Generic;

namespace Winbot.Entities.ComplexScenarios
{
    [Serializable]
    public class ScenarioRepeatFor : Scenario
    {
        public Scenario Scenario { get; set; }
        public TimeSpan Duration { get; set; }

        public override IEnumerable<UserAction> GetExecutingActions()
        {
            var start = DateTime.Now;
            TimeSpan delta;

            do
            {
                foreach (var action in Scenario.GetExecutingActions())
                {
                    yield return action;
                }

                var now = DateTime.Now;
                delta = now - start;

            } while (delta < Duration);
        }
    }
}
