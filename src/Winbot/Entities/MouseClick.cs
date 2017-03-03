using System.Windows.Forms;

namespace Winbot.Entities
{
    internal class MouseClick : UserAction
    {
        public MouseButtons Button { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public override string Description => $"Mouse {Button.ToString().ToLower()} button click, X: {X}, Y:{Y}";
    }
}
