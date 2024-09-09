using System;
using System.Linq;
using LibGit2Sharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;


namespace WebApplication1
{
    public class RepoModel
    {
        public List<Commit> getCommit()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                return repo.Commits.Take(10).ToList();
            }
        }

        public List<string> getMessages()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                return repo.Commits.Select(x => x.Message).ToList();
            }
        }
        public List<string> GetFiles(String branch)
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                List<string> files = new List<string>();
                Branch currentBranch = Commands.Checkout(repo, branch);
                foreach(IndexEntry e in repo.Index)
                {
                   files.Add(e.Path);
                }
                return files;
            }
                
        }
        public List<Branch> GetBranches()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                return repo.Branches.Take(10).ToList();
            }
        }
    }
}