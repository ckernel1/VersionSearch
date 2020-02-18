using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    public class BitBucket : IRepo
    {
        HttpClient _httpClient;
        IConfiguration _configuration;

        public BitBucket(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration.BitBucketToken);
        }
        public async Task<string> GetFileContent(RepositorySlug repositorySlug, string path)
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseBitBucketUrl}/rest/api/1.0/projects/{repositorySlug.ContainingProject.Name}/repos/{repositorySlug.Name}/raw/{path}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            return string.Empty;
        }

        public async Task<List<Project>> GetProjects()
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseBitBucketUrl}/rest/api/1.0/projects/");
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Call to get projects failed: {response.StatusCode}");
            var content = response.Content.ReadAsStringAsync();
            var result = new List<Project>();
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(await content);
            foreach(dynamic value in json.values)
            {
                result.Add(new Project { Name = value.key });
            }
            return result;
                
        }

        public async Task<List<RepoFile>> GetRepoFiles(RepositorySlug repositorySlug)
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseBitBucketUrl}/rest/api/1.0/projects/{repositorySlug.ContainingProject.Name}/repos/{repositorySlug.Name}/files/");
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Call to get files failed: {response.StatusCode}");
            var content = response.Content.ReadAsStringAsync();
            var result = new List<RepoFile>();
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(await content);
            foreach (dynamic path in json.values)
            {
                result.Add(new RepoFile { RelativePath = path });
            }
            return result;
        }

        public async Task<List<RepositorySlug>> GetRepos(Project project)
        {
            var response = await _httpClient.GetAsync($"{_configuration.BaseBitBucketUrl}/rest/api/1.0/projects/{project.Name}/repos");
            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Call to get repositories failed: {response.StatusCode}");
            var content = response.Content.ReadAsStringAsync();
            var result = new List<RepositorySlug>();
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(await content);
            foreach (dynamic value in json.values)
            {
                result.Add(new RepositorySlug { Name = value.slug, ContainingProject=project });
            }
            return result;
        }
    }
}
