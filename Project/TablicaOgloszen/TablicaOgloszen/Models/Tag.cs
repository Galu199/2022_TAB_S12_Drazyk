
using System.ComponentModel.DataAnnotations;

namespace TablicaOgloszen.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
