using Ninject.Modules;
using Winbot.ViewModels;

namespace Winbot.Infrastructure.Ninject
{
    internal class ViewModelsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf().InSingletonScope();
        }
    }
}
