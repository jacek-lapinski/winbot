using System;

namespace Winbot.Entities
{
    internal class Scenario
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public UserAction[] Actions { get; set; }
    }
}
