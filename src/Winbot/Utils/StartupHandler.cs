using System;
using System.Windows;
using Winbot.Entities;
using Winbot.Executors;
using Winbot.Infrastructure.Ninject;

namespace Winbot.Utils
{
    internal class StartupHandler
    {
        public StartupHandler()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length != 0)
            {
                return;
            }

            ExecuteScenarioFromFile(args[0]);
            Application.Current.Shutdown();
        }

        private static void ExecuteScenarioFromFile(string filePath)
        {
            var fileManager = NinjectBootstrapper.Get<IScenarioFileManager>();
            fileManager.Load(filePath, ExecuteScenario);
        }

        private static void ExecuteScenario(Scenario scenario)
        {
            var executor = NinjectBootstrapper.Get<IScenarioExecutor>();
            executor.Execute(scenario);
        }
    }
}
