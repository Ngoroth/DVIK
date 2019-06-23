using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Dvik.Pages.Trainers
{
    public class EditModel : PageModel
    {
        private readonly IData<Trainer> trainerData;

        public EditModel(IData<Trainer> trainerData)
        {
            this.trainerData = trainerData;
        }

        [BindProperty]
        public Trainer Trainer { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? trainerId)
        {
            this.Trainer = trainerId.HasValue 
                ? await this.trainerData.SearchByIdAsync(trainerId.Value) 
                : new Trainer();

            return null == this.Trainer
                ? this.RedirectToPage("./List")
                : (IActionResult)this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.addPhoto();

            this.Trainer = this.Trainer.Id > 0
                ? this.trainerData.Update(this.Trainer)
                : await this.trainerData.AddAsync(this.Trainer);

            await this.trainerData.CommitAsync();
            this.TempData["Message"] = "Тренер сохранен.";
            return this.RedirectToPage("./Details", new { trainerId = this.Trainer.Id });
        }

        private void addPhoto()
        {
            if (this.Photo == null)
            {
                return;
            }

            this.Trainer.Photo = new Photo();
            using (var binaryReader = new BinaryReader(this.Photo.OpenReadStream()))
            {
                this.Trainer.Photo.Data = binaryReader.ReadBytes((int)this.Photo.Length);
            }
        }
    }
}
