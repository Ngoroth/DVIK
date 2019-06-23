using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dvik.Core;

namespace Dvik.Pages.Trainers
{
    public class EditModel : PageModel
    {
        private readonly Data.DvikDbContext _context;

        public EditModel(Data.DvikDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._context.Attach(this.Trainer).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.TrainerExists(this.Trainer.Id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.RedirectToPage("./Index");
        }

        private bool TrainerExists(int id)
        {
            return this._context.Trainers.Any(e => e.Id == id);
        }
    }
}
