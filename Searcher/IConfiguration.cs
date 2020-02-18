namespace Searcher
{
    public interface IConfiguration
    {
        string BitBucketToken { get; }
        string BaseBitBucketUrl { get; }
        string FileSearchPattern { get; }
        string ContentMatchPattern { get; }
    }
}