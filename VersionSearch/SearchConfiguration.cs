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
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configurationRoot = builder.Build();
        }
        public SearchConfiguration(IConfigurationRoot configurationRoot) => _configurationRoot = configurationRoot;

        public string BitBucketToken => _configurationRoot["BitBucketToken"];

        public string BaseBitBucketUrl => _configurationRoot["BaseBitBucketUrl"];

        public string FileSearchPattern => _configurationRoot["FileSearchPattern"];

        public string ContentMatchPattern => _configurationRoot["ContentMatchPattern"];

        
    }
}