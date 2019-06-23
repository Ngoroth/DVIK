using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Core.Abstractions;

namespace Dvik.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly IData<Trainer> trainerData;

        public DeleteModel(IData<Trainer> trainerData)
        {
            this.trainerData = trainerData;
        }

        [BindProperty]
        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnGetAsync(int trainerId)
        {
            this.Trainer = await this.trainerData.SearchByIdAsync(trainerId);
            return this.Trainer == null
                ? this.RedirectToPage("./NotFound")
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync(int trainerId)
        {
            var course = await this.trainerData.DeleteAsync(trainerId);

            if (course == null)
            {
                return this.RedirectToPage("./List");
            }
            await this.trainerData.CommitAsync();
            this.TempData["Message"] = $"Тренер {course.Name} удалён";
            return this.RedirectToPage("./List");
        }
    }
}
