using Ninject.Modules;
using Winbot.Notifiers;

namespace Winbot.Infrastructure.Ninject
{
    internal class DesktopAppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<UserActionNotifier>().To<MouseClickNotifier>().InSingletonScope();
            Bind<UserActionNotifier>().To<MouseDoubleClickNotifier>().InSingletonScope();

            Bind<Settings.AppSettings>().ToSelf().InSingletonScope();
        }
    }
}
