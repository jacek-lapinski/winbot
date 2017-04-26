using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseMoveExecutor : IUserActionExecutor<MouseMove>
    {
        public void Execute(MouseMove action)
        {
            MouseActionSimulator.MoveTo(action.X, action.Y);
        }
    }
}
