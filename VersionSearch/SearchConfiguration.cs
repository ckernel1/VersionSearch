using Searcher;

namespace VersionSearch
{
    internal class SearchConfiguration : IConfiguration
    {
        public string BitBucketToken => "NjgyNTUzMTIwNjUzOv4WxW1sKT+CY0w23LkfTD9oYP3H";

        public string BaseBitBucketUrl => "http://localhost:7990/bitbucket";

        public string FileSearchPattern => ".*.csproj";

        public string ContentMatchPattern => "TargetFramework (.*) TargetFramework";

        
    }
}