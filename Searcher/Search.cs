using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Searcher
{
    public class Search
    {
        IRepo _repository;
        public Search(IRepo repository)
        {
            _repository = repository;
        }
        public async Task<List<RepoFile>> FindFiles(RepositorySlug repositorySlug, string filePattern = null)
        {
            Regex regex = new Regex(filePattern);
            var files = await _repository.GetRepoFiles(repositorySlug);
            return files.Where(f => regex.IsMatch(f.RelativePath)).Select(f=>f)?.ToList();
        }

        public IReadOnlyDictionary<String, List<RepoFile>> GroupByPattern(List<RepoFile> repoFiles, string groupingPattern)
        {
            Regex regex = new Regex(groupingPattern);
            ConcurrentDictionary<string, List<RepoFile>> result = new ConcurrentDictionary<string, List<RepoFile>>(); 
            Parallel.ForEach(repoFiles, f =>
            {
                var match = regex.Match(groupingPattern);
                if (match.Success)
                {
                    var val = match.Captures.First().Value;
                    result.GetOrAdd(val, new List<RepoFile>()).Add(f);
                }
            });
            return result;
        }
    }
}
