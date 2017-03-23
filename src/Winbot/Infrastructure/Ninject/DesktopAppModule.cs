using Ninject.Modules;
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

            Bind<Settings.AppSettings>().ToSelf().InSingletonScope();
            Bind(typeof(IRepository<>)).To(typeof(LocalRepository<>)).InSingletonScope();
            Bind<IScenarioBuilder>().To<ScenarioBuilder>().InSingletonScope();
        }
    }
}
