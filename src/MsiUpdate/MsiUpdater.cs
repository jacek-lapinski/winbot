using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AppUpdate
{
    public class MsiUpdater
    {
        private readonly IMsiProvider _msiProvider;

        public MsiUpdater(IMsiProvider msiProvider)
        {
            _msiProvider = msiProvider;
        }

        public async Task CheckUpdates(Version currentVersion, Func<bool> shouldBeUpdated)
        {
            var destinationFile = GetTempMsiFilePath();
            var isNewVersionAvailable = await _msiProvider.CheckIfNewVersionIsAvailable(currentVersion);

            if (isNewVersionAvailable && shouldBeUpdated())
            {
                await _msiProvider.DownloadLatestVersion(destinationFile);
                Update(destinationFile);
            }
        }

        private static string GetTempMsiFilePath()
        {
            var tempPath = Path.GetTempPath();
            var fileName = $"{Guid.NewGuid()}.msi";
            return Path.Combine(tempPath, fileName);
        }

        private static void Update(string destinationFile)
        {
            var currentProcessId = Process.GetCurrentProcess().Id;
            var updateProcessStartInfo = new ProcessStartInfo("AppUpdate.exe", $"{currentProcessId} {destinationFile}")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(updateProcessStartInfo);
        }
    }
}
