using System;
using System.Windows.Forms;

namespace Winbot.Entities
{
    [Serializable]
    public class MouseUp : UserAction
    {
        public MouseButtons Button { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string Description => $"Mouse {Button.ToString().ToLower()} button up, X: {X}, Y:{Y}";
    }
}
