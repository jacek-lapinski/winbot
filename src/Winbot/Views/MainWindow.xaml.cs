using System.Windows;

namespace Winbot.Views
{
    public partial class MainWindow : Window
    {
        private readonly Window _recordingWindow;

        public MainWindow()
        {
            InitializeComponent();
            
            _recordingWindow = new RecordingWindow();
            _recordingWindow.Show();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            _recordingWindow.Close();
        }
    }
}
