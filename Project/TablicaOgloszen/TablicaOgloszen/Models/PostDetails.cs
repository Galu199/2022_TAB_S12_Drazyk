using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class PostDetails : Post
    {
        public PostDetails(Post _post)
        {
            Id = _post.Id;
            Title = _post.Title;
            Text = _post.Text;
            Rating = _post.Rating;
            Date = _post.Date;
            Pinned = _post.Pinned;
            Users_Id = _post.Users_Id;
            ModedBy = _post.ModedBy;
        }
        public List<Comment> Comments = new List<Comment>();
        public List<Tag> Tags = new List<Tag>();
        public User Owner = new User();
        public int CommentsCount = 0;
    }
}
