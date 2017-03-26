using Winbot.Entities;
using Winbot.Utils;

namespace Winbot.Executors
{
    internal class KeyUpExecutor : IUserActionExecutor<KeyUp>
    {
        public void Execute(KeyUp action)
        {
            KeyboardActionSimulator.KeyUp(action.KeyCode);
        }
    }
}
