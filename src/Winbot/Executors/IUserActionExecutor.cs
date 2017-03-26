using Winbot.Entities;

namespace Winbot.Executors
{
    internal interface IUserActionExecutor
    {
    }

    internal interface IUserActionExecutor<in TUserAction> : IUserActionExecutor where TUserAction : UserAction
    {
        void Execute(TUserAction action);
    }
}
