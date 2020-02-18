using Microsoft.Extensions.Configuration;
using Searcher;
using System.IO;

namespace VersionSearch
{
    internal class SearchConfiguration : Searcher.IConfiguration
    {
        IConfigurationRoot _configurationRoot;
        public SearchConfiguration()
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
        }
        public SearchConfiguration(IConfigurationRoot configurationRoot) => _configurationRoot = configurationRoot;

        public string BitBucketToken => "NjgyNTUzMTIwNjUzOv4WxW1sKT+CY0w23LkfTD9oYP3H";

        public string BaseBitBucketUrl => "http://localhost:7990/bitbucket";

        public string FileSearchPattern => ".*.csproj";

        public string ContentMatchPattern => "TargetFramework (.*) TargetFramework";

        
    }
}