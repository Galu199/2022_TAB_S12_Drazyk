using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class CommentIndex
    {
        public Post post { get; set; }
        public List<Tuple<Comment,User>> comments { get; set; } = new List<Tuple<Comment, User>>();
        
    }
}
