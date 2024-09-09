using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    public class Program
    {
        public List<Commit> getCommit()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                return repo.Commits.Take(10).ToList();
            }
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                
                var branch = repo.Branches["master"];
                
                if (branch == null)
                {
                    // repository return null object when branch not exists
                    
                }
                
                Branch currentBranch = Commands.Checkout(repo, branch);

                
                List<Commit> commitList = repo.Commits.Take(10).ToList();
                List<Branch> branchList = repo.Branches.ToList();
                Console.WriteLine(commitList.ToString());
                Console.WriteLine(branchList.ToString());
                // foreach (Commit c in repo.Commits.Take(15))
                // {
                //     Console.WriteLine(string.Format("commit {0}", c.Id));
                //
                //     if (c.Parents.Count() > 1)
                //     {
                //         Console.WriteLine("Merge: {0}",
                //             string.Join(" ", c.Parents.Select(p => p.Id.Sha.Substring(0, 7)).ToArray()));
                //     }
                //
                //     Console.WriteLine(string.Format("Author: {0} <{1}>", c.Author.Name, c.Author.Email));
                //     Console.WriteLine();
                //     Console.WriteLine(c.Message);
                //     Console.WriteLine();
                // }


            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
