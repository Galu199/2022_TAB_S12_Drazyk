using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Models
{
    public class Permissions
    {
        public string Id { get; set; }
        public PermissionsRole Level { get; set; }
        public Dictionary<PermissionsRole, string> PermissionsRolesDictionary;

        public Permissions()
        {
            PermissionsRolesDictionary = new Dictionary<PermissionsRole, string>();
            PermissionsRolesDictionary.Add(PermissionsRole.Ban, "Ban");
            PermissionsRolesDictionary.Add(PermissionsRole.User, "User");
            PermissionsRolesDictionary.Add(PermissionsRole.Moderator, "Moderator");
            PermissionsRolesDictionary.Add(PermissionsRole.Administrator, "Administrator");
        }
    }

    public enum PermissionsRole
    {
        Ban,
        User,
        Moderator,
        Administrator
    }
}
