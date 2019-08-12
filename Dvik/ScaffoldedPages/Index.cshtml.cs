using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dvik.Core;
using Dvik.Data;

namespace Dvik.ScaffoldedPages
{
    public class IndexModel : PageModel
    {
        private readonly Dvik.Data.DvikDbContext _context;

        public IndexModel(Dvik.Data.DvikDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Courses.ToListAsync();
        }
    }
}
