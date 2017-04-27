using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using Winbot.Annotations;

namespace Winbot.Settings
{
    internal class DatabaseSettings : INotifyPropertyChanged
    {
        public event EventHandler DbFilePathChanged;

        private const string DefaultDbFilePath = @"C:\db\winbot.db";

        private RegistryKey AppRegistryKey => Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Winbot", true);

        private string _dbFilePath;
        public string DbFilePath
        {
            get { return _dbFilePath; }
            set
            {
                _dbFilePath = value;
                AppRegistryKey.SetValue(nameof(DbFilePath), value);
                var fileInfo = new FileInfo(value);
                Directory.CreateDirectory(fileInfo.Directory.FullName);
                OnPropertyChanged();
                OnDbFilePathChanged();
            }
        }

        public DatabaseSettings()
        {
            InitDbFilePathIfNeeded();
        }

        private void InitDbFilePathIfNeeded()
        {
            var dbFilePath = AppRegistryKey.GetValue(nameof(DbFilePath));
            if (dbFilePath == null)
            {
                DbFilePath = DefaultDbFilePath;
            }
            else
            {
                _dbFilePath = dbFilePath.ToString();
            }
        }

        private void OnDbFilePathChanged()
        {
            DbFilePathChanged?.Invoke(this, EventArgs.Empty);
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
