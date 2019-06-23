using Dvik.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Dvik.Core
{
    public class Trainer : IHaveName
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Bio { get; set; }
        public Photo Photo { get; set; }
    }
}
