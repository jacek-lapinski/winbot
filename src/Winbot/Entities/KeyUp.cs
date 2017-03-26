using System.Windows.Forms;

namespace Winbot.Entities
{
    internal class KeyUp : UserAction
    {
        public Keys KeyCode { get; set; }
        public override string Description => $"Key {KeyCode} up";
    }
}
