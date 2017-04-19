using System;
using Winbot.Entities;

namespace Winbot.Utils
{
    internal interface IScenarioFileManager
    {
        void Save(Scenario scenario);
        void Load(string filePath, Action<Scenario> onLoad);
    }
}
