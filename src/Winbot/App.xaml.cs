using System;
using System.Reflection;
using System.Windows;
using AppUpdate;
using log4net;
using Winbot.Entities;
using Winbot.Executors;
using Winbot.Infrastructure.Ninject;
using Winbot.Utils;

namespace Winbot
{
    public partial class App : Application
    {
        private readonly ILog _log;

        public App()
        {
            NinjectBootstrapper.Initialize();
            _log = NinjectBootstrapper.Get<ILog>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);        

            if (e.Args.Length != 1)
            {
#if !DEBUG
                CheckUpdates();
#endif

                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                return;
            }

            //execute only when application started from associated file
            ExecuteScenarioFromFile(e.Args[0]);
            Current.Shutdown();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                HandleUnhandledException(exception);
            }
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
            e.Handled = true;
        }

        private void HandleUnhandledException(Exception exception)
        {
            _log.Error("Unhandled exception occured", exception);
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
