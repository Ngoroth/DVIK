using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Data;

namespace Dvik.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public DeleteModel(DvikDbContext trainerData)
        {
            this.dbContext = trainerData;
        }

        [BindProperty]
        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnGetAsync(int trainerId)
        {
            this.Trainer = await this.dbContext.Trainers.FindAsync(trainerId);
            return this.Trainer == null
                ? this.RedirectToPage("./NotFound")
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync(int trainerId)
        {
            var trainer = await this.dbContext.Trainers.FindAsync(trainerId);

            if (trainer == null)
            {
                return this.RedirectToPage("./List");
            }

            this.dbContext.Remove(trainer);
            await this.dbContext.SaveChangesAsync();

            this.TempData["Message"] = $"Тренер {trainer.Name} удалён";
            return this.RedirectToPage("./List");
        }
    }
}
