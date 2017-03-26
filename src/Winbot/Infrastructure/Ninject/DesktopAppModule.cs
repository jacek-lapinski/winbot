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
            Bind<UserActionNotifier>().To<MouseClickNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseDoubleClickNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<KeyDownNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<KeyUpNotifier>().InSingletonScope();

            Bind<IUserActionExecutor>().To<MouseClickExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<MouseDoubleClickExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<KeyDownExecutor>().InSingletonScope();
            Bind<IUserActionExecutor>().To<KeyUpExecutor>().InSingletonScope();

            Bind<IScenarioExecutor>().To<ScenarioExecutor>().InSingletonScope();

            Bind<Settings.AppSettings>().ToSelf().InSingletonScope();
            Bind(typeof(IRepository<>)).To(typeof(LocalRepository<>)).InSingletonScope();
            Bind<IScenarioBuilder>().To<ScenarioBuilder>().InSingletonScope();
        }
    }
}
