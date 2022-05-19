using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class Rating
    {
        [Key][Required]
        public string Users_Id { get; set; }
        [Key][Required]
        public int Posts_Id { get; set; }
        [Required][DisplayName("Ocena")]
        public int Value { get; set; }
    }
}
