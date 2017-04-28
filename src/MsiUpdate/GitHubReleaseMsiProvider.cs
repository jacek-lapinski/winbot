using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppUpdate
{
    public class GitHubReleaseMsiProvider : IMsiProvider
    {
        private const string ApiUrl = "https://api.github.com";

        private readonly string _owner;
        private readonly string _repository;

        private string LatestReleaseUrl => $"{ApiUrl}/repos/{_owner}/{_repository}/releases/latest";

        public GitHubReleaseMsiProvider(string owner, string repository)
        {
            _owner = owner;
            _repository = repository;
        }

        public async Task<bool> CheckIfNewVersionIsAvailable(Version currentVersion)
        {
            try
            {
                var release = await DownloadLatestRelease();
                return release.Version > currentVersion;
            }
            catch (Exception ex)
            {
                //todo log
                return false;
            }
        }

        public async Task DownloadLatestVersion(string destinationFile)
        {
            try
            {
                var release = await DownloadLatestRelease();
                await DownloadMsiFromUrl(release.Assets[0].DownloadUrl, destinationFile);

            }
            catch (Exception ex)
            {
                //todo log
            }
        }

        private async Task<GitHubRelease> DownloadLatestRelease()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "AppUpdate");
                var response = await client.GetAsync(LatestReleaseUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var release = JsonConvert.DeserializeObject<GitHubRelease>(content);
                return release;
            }
        }

        private static async Task DownloadMsiFromUrl(string downloadUrl, string destinationFile)
        {
            using (var client = new WebClient())
            {
                var uri = new Uri(downloadUrl);
                await client.DownloadFileTaskAsync(uri, destinationFile);
            }
        }

        private class GitHubRelease
        {
            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("assets")]
            public GitHubAsset[] Assets { get; set; }

            [JsonIgnore]
            public Version Version
            {
                get
                {
                    var version = TagName.StartsWith("v") ? TagName.Substring(1) : TagName;
                    var result = new Version(version);
                    return result;
                }
            }
        }

        private class GitHubAsset
        {
            [JsonProperty("browser_download_url")]
            public string DownloadUrl { get; set; }
        }
    }
}
