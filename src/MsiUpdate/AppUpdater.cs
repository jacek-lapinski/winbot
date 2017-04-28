using System.Diagnostics;
using System.IO;

namespace AppUpdate
{
    public static class AppUpdater
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
                return;

            var appProcessId = int.Parse(args[0]);
            var msiFilePath = args[1];

            Process.GetProcessById(appProcessId).WaitForExit();

            var installationProcess = Process.Start(msiFilePath);
            if (installationProcess == null)
                return;

            installationProcess.WaitForExit();
            File.Delete(msiFilePath);
        }
    }
}
