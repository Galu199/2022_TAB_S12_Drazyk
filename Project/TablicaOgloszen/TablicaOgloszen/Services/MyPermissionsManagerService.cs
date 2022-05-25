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

        public Permissions permissions = new Permissions();

        //
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

        //
        public async Task<PermissionsRole> findPermissions(User user)
        {
            var Level = PermissionsRole.Ban;
            try
            {
                if (user == null) return Level;
                if (user.Id == null) return Level;
                var list = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
                //CODE FOR CHECKIG ROLES
                foreach (var item in permissions.PermissionsRolesDictionary)
                {
                    foreach (var role in list)
                    {
                        if (role.ToUpper() == item.Value.ToUpper()) Level = item.Key;
                    }
                }
                //END
                return Level;
            }
            catch
            {
                return Level;
            }
        }

        //
        public async Task<PermissionsRole> findPermissions(string user_id)
        {
            var Level = PermissionsRole.Ban;
            try
            {
                if (user_id.Equals("")) return Level;
                var list = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user_id));
                //CODE FOR CHECKIG ROLES
                foreach (var item in permissions.PermissionsRolesDictionary)
                {
                    foreach (var role in list)
                    {
                        if (role.ToUpper() == item.Value.ToUpper()) Level = item.Key;
                    }
                }
                //END
                return Level;
            }
            catch
            {
                return Level;
            }
        }

        //
        public async Task addRole(string user_Id, PermissionsRole role)
        {
            if (user_Id.Equals("")) return;
            var userIdentity = await _userManager.FindByIdAsync(user_Id);
            var result = await _userManager.AddToRoleAsync(userIdentity, permissions.PermissionsRolesDictionary[role]);
            Console.WriteLine("Adding role result: "+result);
            return;
        }

        //
        public async Task removeRole(string user_Id, PermissionsRole role)
        {
            if (user_Id.Equals("")) return;
            var userIdentity = await _userManager.FindByIdAsync(user_Id);
            var result = await _userManager.RemoveFromRoleAsync(userIdentity, permissions.PermissionsRolesDictionary[role]);
            Console.WriteLine("Removing role result: " + result);
            return;
        }
    }
}
