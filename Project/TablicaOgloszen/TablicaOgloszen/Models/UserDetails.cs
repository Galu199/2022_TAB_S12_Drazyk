using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class UserDetails : User
    {
        public UserDetails(User _user)
        {
            Id = _user.Id;
            Email = _user.Email;
            UserName = _user.UserName;
            PhoneNumber = _user.PhoneNumber;
            Ban = _user.Ban;
            ModedBy = _user.ModedBy;
        }
        public List<Post> Posts = new List<Post>();
        public List<Comment> Comments = new List<Comment>();
    }
}
