using System;

namespace Winbot.Entities
{
    [Serializable]
    public class MouseWheel : UserAction
    {
        public int Delta { get; set; }
        public override string Description => $"Mouse wheel, delta: {Delta}";
    }
}
