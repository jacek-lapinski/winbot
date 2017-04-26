using System;

namespace Winbot.Entities
{
    [Serializable]
    public class MouseMove : UserAction
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string Description => $"Mouse move, X: {X}, Y:{Y}";
    }
}
