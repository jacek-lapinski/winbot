using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class KeyDownExecutor : IUserActionExecutor<KeyDown>
    {
        public void Execute(KeyDown action)
        {
            KeyboardActionSimulator.KeyDown(action.KeyCode);
        }
    }
}
