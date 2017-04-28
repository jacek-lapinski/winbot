using System;
using System.Threading.Tasks;

namespace AppUpdate
{
    public interface IMsiProvider
    {
        Task<bool> CheckIfNewVersionIsAvailable(Version currentVersion);
        Task DownloadLatestVersion(string destinationFile);
    }
}
