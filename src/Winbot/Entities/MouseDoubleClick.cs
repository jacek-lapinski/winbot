using System;
using System.Windows.Forms;

namespace Winbot.Entities
{
    [Serializable]
    internal class MouseDoubleClick : UserAction
    {
        public MouseButtons Button { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string Description => $"Mouse {Button.ToString().ToLower()} button double click, X: {X}, Y:{Y}";
    }
}
