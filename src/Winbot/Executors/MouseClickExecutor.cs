using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseClickExecutor : IUserActionExecutor<MouseClick>
    {
        public void Execute(MouseClick action)
        {
            MouseActionSimulator.MouseClick(action.Button, action.X, action.Y);
        }
    }
}
