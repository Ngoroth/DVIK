using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dvik.Core;

namespace Dvik.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly Data.DvikDbContext _context;

        public DetailsModel(Data.DvikDbContext context)
        {
            this._context = context;
        }

        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Trainer = await this._context.Trainers.FirstOrDefaultAsync(m => m.Id == id);

            return this.Trainer == null ? this.NotFound() : (IActionResult)this.Page();
        }
    }
}
