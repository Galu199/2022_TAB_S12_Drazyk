using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TablicaOgloszen.Models;

namespace TablicaOgloszen.Services
{
    public class MyPermissionsManagerService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public MyPermissionsManagerService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Permissions permissions;

        public async Task getPermissions(ClaimsPrincipal user)
        {
            permissions = new Permissions();
            if (user == null) return;
            if (user.Identity.Name == null) return;
            permissions.Id = _userManager.GetUserId(user);
            var list = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(permissions.Id));
            //CODE FOR CHECKIG ROLES
            permissions.Level = PermissionsRole.Ban;
            foreach (var item in permissions.PermissionsRolesDictionary) 
            {
                foreach (var role in list)
                {
                    if (role.ToUpper() == item.Value.ToUpper()) permissions.Level = item.Key;
                }
            }
            //END
            return;
        }
    }
}
