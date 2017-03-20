using Winbot.Entities;

namespace Winbot.Utils
{
    internal interface IScenarioBuilder
    {
        void Init();
        Scenario Build();
    }
}
