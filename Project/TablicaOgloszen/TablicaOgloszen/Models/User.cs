using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TablicaOgloszen.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Imię i nazwisko")]
        public string UserName { get; set; }
        [Required]
        [Phone]
        [DisplayName("Numer telefonu")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Zbanowany")]
        public bool Ban { get; set; }
        [DisplayName("Zablokowany przez")]
        public string ModedBy { get; set; }
    }
}
