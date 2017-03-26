using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseDoubleClickExecutor : IUserActionExecutor<MouseDoubleClick>
    {
        public void Execute(MouseDoubleClick action)
        {
            MouseActionSimulator.MouseClick(action.Button, action.X, action.Y);
            MouseActionSimulator.MouseClick(action.Button, action.X, action.Y);
        }
    }
}
