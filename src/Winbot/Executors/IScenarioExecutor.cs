using Winbot.Entities;

namespace Winbot.Executors
{
    internal interface IScenarioExecutor
    {
        void Execute(Scenario scenario);
    }
}