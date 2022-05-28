using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TablicaOgloszen.Models;
using TablicaOgloszen.Services;

namespace TablicaOgloszen.Controllers
{

    public class UserController : Controller
    {
        private readonly MyDataBaseManagerService _myDataBaseService;
        private readonly MyPermissionsManagerService _myPermissionsManagerService;
        private readonly MyNotificationManagerService _myNotificationService;
        public UserController(MyDataBaseManagerService myDataBaseService,
            MyPermissionsManagerService myPermissionsManagerService,
            MyNotificationManagerService myNotificationManagerService)
        {
            _myDataBaseService = myDataBaseService;
            _myPermissionsManagerService = myPermissionsManagerService;
            _myNotificationService = myNotificationManagerService;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Moderator)
            {
                ModelState.AddModelError(string.Empty, "Nie posiadasz uprawnień");
                return View(null);
            }
            try
            {
                List<User> users = new List<User>();
                using (var scope = new TransactionScope())
                {
                    users = _myDataBaseService.QueryUsers("SELECT * FROM Users");
                    scope.Complete();
                }
                return View(users);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd.");
                return View(null);
            }
        }

        //GET
        public IActionResult Details(string Id)
        {
            UserDetails person = new UserDetails(new User());
            try
            {
                using (var scope = new TransactionScope())
                {
                    person = new UserDetails(_myDataBaseService.QueryUsers($"SELECT * FROM Users WHERE Id='{Id}';").First());
                    person.Posts = _myDataBaseService.QueryPosts($"SELECT * FROM Posts WHERE Users_Id='{Id}'");
                    person.Comments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Users_Id='{Id}'");
                    scope.Complete();
                }
                return View(person);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd.");
                return View(null);
            }
        }

        //GET
        public IActionResult Edit(string Id)
        {
            try
            {
                var user = new User();
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this ID"));
                    }
                    user = users.First();
                    scope.Complete();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(null);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var userEdit = new User();
            try
            {
                if (!(_myPermissionsManagerService.permissions.Id.Equals(user.Id) ||
                    _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
                {
                    throw new Exception("UnAuthorized user: " + _myPermissionsManagerService.permissions.Id + " tryed to edit user: " + user.Id);
                }
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{user.Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this ID: " + user.Id));
                    }
                    userEdit = users.First();
                    userEdit.UserName = user.UserName;
                    userEdit.Email = user.Email;
                    userEdit.PhoneNumber = user.PhoneNumber;
                    _myDataBaseService.UpdateUser(userEdit);
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Edycja udana");
                return View(userEdit);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                Console.WriteLine(ex.Message);
                return View(userEdit);
            }
        }

        //GET
        public async Task<IActionResult> Delete(string Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var user = new User();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this Id: " + Id));
                    }
                    user = users.First();
                    user.PhoneNumber = "000 000 000";
                    scope.Complete();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd.");
                return View(user);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{user.Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this Id: " + user.Id));
                    }
                    user = users.First();
                    user.Ban = true;
                    user.ModedBy = _myPermissionsManagerService.permissions.Id;
                    _myDataBaseService.UpdateUser(user);
                    var usersPosts = _myDataBaseService.QueryPosts($"SELECT * FROM Posts WHERE Users_Id='{user.Id}';");
                    foreach (var post in usersPosts)
                    {
                        post.Pinned = false;
                        post.Deleted = true;
                        post.ModedBy = _myPermissionsManagerService.permissions.Id;
                        _myDataBaseService.UpdatePost(post);
                    }
                    var usersComments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Users_Id='{user.Id}';");
                    foreach (var comment in usersComments)
                    {
                        comment.Deleted = true;
                        comment.ModedBy = _myPermissionsManagerService.permissions.Id;
                        _myDataBaseService.UpdateComment(comment);
                    }
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Usuwanie powiodło się.");
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd.");
                return View(user);
            }
        }

        //GET
        public async Task<IActionResult> ModGrant(string Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Administrator) return RedirectToAction("Index");
            try
            {
                await _myPermissionsManagerService.addRole(Id, PermissionsRole.Moderator);
                _myNotificationService.Modded(Id, true);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //GET
        public async Task<IActionResult> ModRevoke(string Id)
        {

            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Administrator) return RedirectToAction("Index");
            try
            {
                await _myPermissionsManagerService.removeRole(Id, PermissionsRole.Moderator);
                _myNotificationService.Modded(Id, false);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //GET
        public async Task<IActionResult> Ban(string Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Moderator)
                {
                    throw new Exception("UnAuthorized user: " + _myPermissionsManagerService.permissions.Id + " tryed to ban user: " + Id);
                }
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this ID"));
                    }
                    var user = users.First();
                    user.Ban = true;
                    user.ModedBy = _myPermissionsManagerService.permissions.Id;
                    _myDataBaseService.UpdateUser(user);
                    scope.Complete();
                }
                await _myPermissionsManagerService.removeRole(Id, PermissionsRole.Moderator);
                await _myPermissionsManagerService.removeRole(Id, PermissionsRole.User);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }

        //GET
        public async Task<IActionResult> Unban(string Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Moderator)
                {
                    throw new Exception("UnAuthorized user: " + _myPermissionsManagerService.permissions.Id + " tryed to ban user: " + Id);
                }
                using (var scope = new TransactionScope())
                {
                    var users = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{Id}';");
                    if (users.Count <= 0)
                    {
                        throw (new Exception("No user with this ID"));
                    }
                    var user = users.First();
                    user.Ban = false;
                    user.ModedBy = _myPermissionsManagerService.permissions.Id;
                    _myDataBaseService.UpdateUser(user);
                    scope.Complete();
                }
                await _myPermissionsManagerService.addRole(Id, PermissionsRole.User);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
