using System;
using System.Windows;

namespace Winbot.Views
{
    public partial class DialogEditorWindow : Window
    {
        private readonly Action<ICloneable> _onSave;
        public ICloneable Item { get; set; }

        public DialogEditorWindow(ICloneable item, Action<ICloneable> onSave)
        {
            InitializeComponent();
            DataContext = this;
            _onSave = onSave;

            Item = (ICloneable)item.Clone();
        }

        private void SaveOnClick(object sender, RoutedEventArgs e)
        {
            _onSave(Item);
            Close();
        }

        private void CancelOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
