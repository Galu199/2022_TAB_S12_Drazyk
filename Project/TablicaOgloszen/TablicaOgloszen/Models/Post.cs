using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TablicaOgloszen.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [DisplayName("Tekst")]
        public string Text { get; set; }
        [Required]
        [DisplayName("Łapki w górę")]
        public int Rating { get; set; }
        [Required]
        [DisplayName("Data utworzenia")]
        public DateTime Date { get; set; }
        [DisplayName("Przypięte")]
        public bool Pinned { get; set; }
        [DisplayName("Usunięte")]
        public bool Deleted { get; set; }
        [DisplayName("Twórca")]
        public string Users_Id { get; set; }
        [DisplayName("Usunięte przez")]
        public string ModedBy { get; set; }
        public List<Comment> Comments;
        public User Owner;
    }
}