using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class MouseWheelExecutor : IUserActionExecutor<MouseWheel>
    {
        public void Execute(MouseWheel action)
        {
            MouseActionSimulator.Wheel(action.Delta);
        }
    }
}
