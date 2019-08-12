using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Data;

namespace Dvik.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly DvikDbContext dbContext;

        public DetailsModel(DvikDbContext trainerData)
        {
            this.dbContext = trainerData;
        }

        public Trainer Trainer { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int trainerId)
        {
            this.Trainer = await this.dbContext.Trainers.FindAsync(trainerId);
            return null == this.Trainer
                ? this.RedirectToPage("./NotFound")
                : (IActionResult)this.Page();
        }
    }
}
