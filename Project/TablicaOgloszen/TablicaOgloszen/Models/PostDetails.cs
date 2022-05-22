using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class PostDetails 
    {
        public Post Post = new Post();
        public List<Comment> Comments = new List<Comment>();
        public List<Tag> Tags = new List<Tag>();
        public User Owner = new User();
        public Rating myRating = new Rating();
        public float AvgRating = 0;
        public int CommentsCount = 0;
    }
}
