using System;
using System.Windows;

namespace Winbot.Views
{
    /// <summary>
    /// Interaction logic for RecordingWindow.xaml
    /// </summary>
    public partial class RecordingWindow : Window
    {
        public RecordingWindow()
        {
            InitializeComponent();
            Deactivated += RecordingWindow_Deactivated;
        }

        private void RecordingWindow_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
