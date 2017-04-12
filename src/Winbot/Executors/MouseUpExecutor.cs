using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseUpExecutor : IUserActionExecutor<MouseUp>
    {
        public void Execute(MouseUp action)
        {
            MouseActionSimulator.MouseUp(action.Button, action.X, action.Y);
        }
    }
}
