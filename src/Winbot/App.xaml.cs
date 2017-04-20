using System.Windows;
using Winbot.Entities;
using Winbot.Executors;
using Winbot.Infrastructure.Ninject;
using Winbot.Utils;

namespace Winbot
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NinjectBootstrapper.Initialize();

            if (e.Args.Length != 1)
            {
                return;
            }

            ExecuteScenarioFromFile(e.Args[0]);
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
