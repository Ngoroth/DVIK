using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Dvik.Data;
using Microsoft.EntityFrameworkCore;

namespace Dvik.Pages.Trainers
{
    public class ListModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public ListModel(DvikDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Trainer> Trainers { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public async Task OnGetAsync()
        {
            this.Trainers = await this.dbContext.Trainers.Where(t => string.IsNullOrEmpty(this.SearchTerm)
            || t.Name == this.SearchTerm).ToArrayAsync();
        }
    }
}
