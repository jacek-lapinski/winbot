using Ninject.Modules;
using Winbot.Executors;
using Winbot.Notifiers;
using Winbot.Repositories;
using Winbot.Utils;

namespace Winbot.Infrastructure.Ninject
{
    internal class DesktopAppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UserActionNotifier>().To<MouseMoveNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseClickNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseDoubleClickNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseDownNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseUpNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseWheelNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<KeyDownNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<KeyUpNotifier>().InSingletonScope();

            Bind<IUserActionExecutor>().To<MouseMoveExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseClickExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseDoubleClickExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseDownExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseUpExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseWheelExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<KeyDownExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<KeyUpExecutor>().InSingletonScope();

            Bind<IScenarioExecutor>().To<ScenarioExecutor>().InSingletonScope();

            Bind<Settings.DatabaseSettings>().ToSelf().InSingletonScope();
            Bind<Settings.AppSettings>().ToSelf().InSingletonScope();
            Bind(typeof(IRepository<>)).To(typeof(LocalRepository<>)).InSingletonScope();
            Bind<IScenarioBuilder>().To<ScenarioBuilder>().InSingletonScope();
            Bind<IScenarioFileManager>().To<ScenarioFileManager>().InSingletonScope();
        }
    }
}
