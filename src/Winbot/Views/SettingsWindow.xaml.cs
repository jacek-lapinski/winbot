using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Winbot.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void UpdateShortcutOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var key = (e.Key == Key.System ? e.SystemKey : e.Key);

            if (key == Key.LeftShift || key == Key.RightShift
                || key == Key.LeftCtrl || key == Key.RightCtrl
                || key == Key.LeftAlt || key == Key.RightAlt
                || key == Key.LWin || key == Key.RWin)
            {
                return;
            }

            var modifierKey = GetModifierPart();
            var shortcut = $"{modifierKey}{key}";

            var textbox = sender as TextBox;
            if (textbox != null)
            {
                textbox.Text = shortcut;
            }
        }

        private static string GetModifierPart()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                return "Ctrl+";
            }
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                return "Shift+";
            }
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                return "Alt+";
            }

            return string.Empty;
        }
    }
}
