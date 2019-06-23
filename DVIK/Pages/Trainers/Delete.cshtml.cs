using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dvik.Core;

namespace Dvik.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly Data.DvikDbContext _context;

        public DeleteModel(Data.DvikDbContext context)
        {
            this._context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Trainer = await this._context.Trainers.FindAsync(id);

            if (this.Trainer != null)
            {
                this._context.Trainers.Remove(this.Trainer);
                await this._context.SaveChangesAsync();
            }

            return this.RedirectToPage("./Index");
        }
    }
}
