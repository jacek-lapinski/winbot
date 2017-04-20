using System;
using System.Windows.Forms;

namespace Winbot.Entities
{
    [Serializable]
    public class KeyDown : UserAction
    {
        public Keys KeyCode { get; set; }
        public override string Description => $"Key {KeyCode} down";
    }
}
