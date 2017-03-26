using System.Windows.Forms;

namespace Winbot.Entities
{
    internal class KeyDown : UserAction
    {
        public Keys KeyCode { get; set; }
        public override string Description => $"Key {KeyCode} down";
    }
}
