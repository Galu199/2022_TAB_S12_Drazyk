using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class UserIndex
    {
        public User user { get; set; } = new User();
        public User modedByUser { get; set; } = new User();
    }
}
