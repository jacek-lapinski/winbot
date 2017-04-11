using System;
using System.Windows.Forms;

namespace Winbot.Entities
{
    [Serializable]
    internal class KeyUp : UserAction
    {
        public Keys KeyCode { get; set; }
        public override string Description => $"Key {KeyCode} up";
    }
}
