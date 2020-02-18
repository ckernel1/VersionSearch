using Microsoft.Extensions.Configuration;
using Searcher;
using System;
using System.Collections.Generic;
using System.IO;

namespace VersionSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            

            var repo = new BitBucket(new SearchConfiguration());
            var search = new Search(repo);

            var projects = repo.GetProjects().Result;
            int i=0;

            while (true)
            {
                projects.ForEach(p => Console.WriteLine($"{i++}. {p.Name}"));
                Console.Write("Selection : ");
                int projectNumber = Convert.ToInt32(Console.ReadLine());

                var repos =  repo.GetRepos(projects[projectNumber]).Result;
                i = 0;
                repos.ForEach(r => Console.WriteLine($"{i++}. {r.Name}"));
                Console.Write("Selection : ");
                int repoNumber = Convert.ToInt32(Console.ReadLine());

                var files =  search.FindFiles(repos[repoNumber], @".*.txt").Result; //.*.csproj
                var grouping = search.GroupByPattern(files, "(file)"); //TargetFramework (.*) TargetFramework
                Console.Clear();
                RunReport(grouping);
                Console.WriteLine(); Console.WriteLine();
            }

        }

        private static void RunReport(IReadOnlyDictionary<string, List<RepoFile>> grouping)
        {
            foreach (var x in grouping)
            {
                
                Console.WriteLine($"{x.Key} => {x.Value.Count}");
            }
            Console.WriteLine("________________________________________________________________");
        }
    }
}
