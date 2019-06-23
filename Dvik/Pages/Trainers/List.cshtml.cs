using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dvik.Core;
using Dvik.Core.Abstractions;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Dvik.Pages.Trainers
{
    public class ListModel : PageModel
    {
        private readonly IData<Trainer> trainerData;

        public ListModel(IData<Trainer> trainerData)
        {
            this.trainerData = trainerData;
        }

        public List<Trainer> Trainers { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public async Task OnGetAsync()
        {
            this.Trainers = (await this.trainerData.SearchByNameAsync(this.SearchTerm)).ToList();
        }
    }
}
