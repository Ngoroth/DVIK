using Dvik.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Dvik.Core
{
    public class Course : IHaveName
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо ввести название курса!")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести описание курса!")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public Trainer Trainer { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
