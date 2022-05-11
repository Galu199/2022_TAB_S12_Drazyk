using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class PostIndex : Post
    {
        public PostIndex(Post _post)
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
        public List<Comment> Comments;
        public User Owner;
    }
}
