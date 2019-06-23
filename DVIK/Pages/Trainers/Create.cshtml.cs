using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;

namespace Dvik.Pages.Trainers
{
    public class CreateModel : PageModel
    {
        private readonly Data.DvikDbContext _context;

        public CreateModel(Data.DvikDbContext context)
        {
            this._context = context;
        }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._context.Trainers.Add(this.Trainer);
            await this._context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}