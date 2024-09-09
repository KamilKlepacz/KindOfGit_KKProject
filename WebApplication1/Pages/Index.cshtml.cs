using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LibGit2Sharp;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Commit> GetCommit()
        {
            using (var repo = new Repository("C:\\Users\\kamil\\RiderProjects\\test_project"))
            {
                return repo.Commits.Take(10).ToList();
            }
        }


        public void OnGet()
        {

        }
    }
}