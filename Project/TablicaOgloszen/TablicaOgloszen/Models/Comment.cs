using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TablicaOgloszen.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Treść")]
        public string Text { get; set; }
        [Required]
        [DisplayName("Data utworzenia")]
        public DateTime Date { get; set; }
        [DisplayName("Czy usunięty")]
        public bool Deleted { get; set; }
        [DisplayName("Twórca")]
        public string Users_Id { get; set; }
        [DisplayName("Jego ogłoszenie")]
        public int Posts_Id { get; set; }
        [DisplayName("Kto usunął")]
        public string ModedBy { get; set; }
    }
}
