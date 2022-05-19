using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class PostIndex 
    {
        public Post post = new Post();
        public List<Comment> Comments = new List<Comment>();
        public User Owner = new User();
    }
}
