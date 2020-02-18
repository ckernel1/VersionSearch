using System.Collections.Generic;
using System.Threading.Tasks;

namespace Searcher
{
    public interface IRepo
    {
        Task<List<Project>> GetProjects();
        Task<List<RepositorySlug>> GetRepos(Project project);
        Task<List<RepoFile>> GetRepoFiles(RepositorySlug repositorySlug);
        Task<string> GetFileContent(RepositorySlug repositorySlug, string path);
    }
}