using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseDownExecutor : IUserActionExecutor<MouseDown>
    {
        public void Execute(MouseDown action)
        {
            MouseActionSimulator.MouseDown(action.Button, action.X, action.Y);
        }
    }
}
