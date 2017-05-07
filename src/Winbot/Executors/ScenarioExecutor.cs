using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Winbot.Entities;

namespace Winbot.Executors
{
    internal class ScenarioExecutor : IScenarioExecutor
    {
        private readonly Dictionary<Type, IUserActionExecutor> _executors;

        public ScenarioExecutor(IEnumerable<IUserActionExecutor> executors)
        {
            _executors = executors.ToDictionary(
                e => e.GetType().GetInterfaces().Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IUserActionExecutor<>)).GenericTypeArguments[0],
                e => e);
        }

        public void Execute(Scenario scenario)
        {
            var currentTime = new TimeSpan(0);
            foreach (var action in scenario.GetExecutingActions())
            {
                var sleepTime = action.Time - currentTime;
                Thread.Sleep(sleepTime);
                currentTime = action.Time;

                ExecuteAction(action);
            }
        }

        private void ExecuteAction(UserAction action)
        {
            var actionType = action.GetType();
            var executor = _executors[actionType];

            var executeMethod = typeof(IUserActionExecutor<>).MakeGenericType(actionType).GetMethod("Execute");
            executeMethod.Invoke(executor, new object[] {action});
        }

    }
}
