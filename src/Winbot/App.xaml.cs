using System.Reflection;
using System.Windows;
using AppUpdate;
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
                #if !DEBUG
                CheckUpdates();
                #endif

                return;
            }

            ExecuteScenarioFromFile(e.Args[0]);
            Current.Shutdown();
        }

        private async void CheckUpdates()
        {
            var gitHubMsiProvider = new GitHubReleaseMsiProvider("jacek-lapinski", "winbot");
            var msiUpdater = new MsiUpdater(gitHubMsiProvider);
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            await msiUpdater.CheckUpdates(currentVersion, NewVersionIsAvailableDialog);
        }

        private bool NewVersionIsAvailableDialog()
        {
            var result = MessageBox.Show(MainWindow, "New version is available. Do you want to download it? New version will be installed after application shut down.",
                "New version available", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
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
