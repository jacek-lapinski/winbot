using Winbot.Entities;

namespace Winbot.Listeners
{
    internal interface IUserActionListener
    {
        void Update(UserAction userAction);
    }
}
