using System;
using System.Windows;
using Winbot.Listeners;
using Winbot.Notifiers;

namespace Winbot.Views
{
    public partial class MainWindow : Window
    {
        private IUserActionListener _consoleListener;
        private UserActionNotifier _mouseClickNotifier;
        private UserActionNotifier _mouseDoubleClickNotifier;

        public MainWindow()
        {
            InitializeComponent();

            //todo: delete it later, it is just for test

            _consoleListener = new ConsoleListener();
            _mouseClickNotifier = new MouseClickNotifier();
            _mouseDoubleClickNotifier = new MouseDoubleClickNotifier();

            _mouseClickNotifier.Subscribe(_consoleListener);
            _mouseDoubleClickNotifier.Subscribe(_consoleListener);

            var now = DateTime.Now;
            _mouseClickNotifier.Start(now);
            _mouseDoubleClickNotifier.Start(now);
        }
    }
}
