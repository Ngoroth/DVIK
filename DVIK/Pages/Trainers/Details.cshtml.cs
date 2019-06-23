using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Core.Abstractions;

namespace Dvik.Pages.Trainers
{
    public class DetailsModel : PageModel
    {
        private readonly IData<Trainer> trainerData;

        public DetailsModel(IData<Trainer> trainerData)
        {
            this.trainerData = trainerData;
        }

        public Trainer Trainer { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int trainerId)
        {
            this.Trainer = await this.trainerData.SearchByIdAsync(trainerId);
            return null == this.Trainer
                ? this.RedirectToPage("./NotFound")
                : (IActionResult)this.Page();
        }
    }
}
