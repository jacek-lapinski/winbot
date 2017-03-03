using System;
using Winbot.Entities;

namespace Winbot.Listeners
{
    internal class ConsoleListener : IUserActionListener
    {
        public void Update(UserAction userAction)
        {
            var message = userAction.ToString();
            Console.WriteLine(message);
        }
    }
}
