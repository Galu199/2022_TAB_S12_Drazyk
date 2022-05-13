
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TablicaOgloszen.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tag")]
        public string Text { get; set; }
        public int Posts_Id { get; set; }
    }
}
