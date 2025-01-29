using System;
using System.Linq;
using LibGit2Sharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1
{
    public class RepoModel
    {
        public Branch gitCheckoutOwn(string branchName)
        {
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                var branch = repo.Branches[branchName];

                if (branch == null)
                {
                    Console.WriteLine("something went wrong");
                }

                return Commands.Checkout(repo, branch);
            }
        }

        public List<Commit> getCommit()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                return repo.Commits.Take(10).ToList();
            }
        }

        public List<string> getMessages()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                return repo.Commits.Select(x => x.Message).ToList();
            }
        }
        public List<string> GetFiles(String branch)
        {
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                List<string> files = new List<string>();
                Branch currentBranch = Commands.Checkout(repo, branch);
                foreach (IndexEntry e in repo.Index)
                {
                    files.Add(e.Path);
                }
                return files;
            }

        }

        public string showFile(string fileName)
        {
            string[] paths = { "C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project\\", fileName };
            string fullPath = Path.Combine(paths);
            return File.ReadAllText(fullPath);
        }
        public List<Branch> GetBranches()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                return repo.Branches.Take(10).ToList();
            }
        }

        public string getDifference(string fileName)
        {
            string result;
            using (var repo = new Repository("C:\\Users\\kamil\\source\\repos\\KamilKlepacz\\test_project"))
            {
                //List<Commit> CommitList = new List<Commit>();
                //foreach (LogEntry entry in repo.Commits.QueryBy(fileName).ToList())
                //    CommitList.Add(entry.Commit);

                //CommitList.Add(null); // Added to show correct initial add
               List<Commit> CommitList = repo.Commits.Take(10).ToList();

                {
                    int ChangeDesired = 0; // Change difference desired
                    //var repoDifferences = repo.Diff.Compare<Patch>((Equals(CommitList[ChangeDesired + 1], null)) ? null : CommitList[ChangeDesired + 1].Tree, (Equals(CommitList[ChangeDesired], null)) ? null : CommitList[ChangeDesired].Tree);
                    var repoDifferences = repo.Diff.Compare<Patch>(CommitList[3].Tree, CommitList[0].Tree);


                    String resultingPatch = "";
                    foreach (var patch in repoDifferences)
                    {
                        resultingPatch += patch.Patch;
                    }

                    return resultingPatch;

                    PatchEntryChanges file = null;

                    try { file = repoDifferences.First(e => e.Path == fileName); }
                    catch { } // If the file has been renamed in the past- this search will fail
                    if (!Equals(file, null))
                    {
                        result = file.Patch;
                        return result;
                    }
                    else
                    {
                        result = "";
                        return result;
                    }
                }

            }
        }
    }
}