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
    public class CommentController : Controller
    {
        private readonly MyDataBaseService _myDataBaseService;
        private readonly MyPermissionsManagerService _myPermissionsManagerService;
        public CommentController(MyDataBaseService myDataBaseService,
            MyPermissionsManagerService myPermissionsManagerService)
        {
            _myDataBaseService = myDataBaseService;
            _myPermissionsManagerService = myPermissionsManagerService;
        }

        //GET
        public async Task<IActionResult> Index(int Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "Proszę się Zarejestrować.");
                    return View(null);
                }
                var commentIndex = new CommentIndex();
                using (var scope = new TransactionScope())
                {
                    commentIndex.post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}").First();
                    List<Comment> comments;
                    if (_myPermissionsManagerService.permissions.Level > PermissionsRole.User)
                    {
                        comments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Posts_Id={Id} ORDER BY DATE DESC");
                    }
                    else
                    {
                        comments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Posts_Id={Id} AND Deleted=0 ORDER BY DATE DESC");
                    }
                    foreach (var item in comments)
                    {
                        var owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{item.Users_Id}';").First();
                        commentIndex.comments.Add(new Tuple<Comment, User>(item, owner));
                    }
                    scope.Complete();
                }
                return View(commentIndex);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Nie można wyświetlić komentarzy");
                return View(null);
            }
        }

        //GET
        public async Task<IActionResult> Create(int Id)
        {
            var item = new Comment();
            item.Posts_Id = Id;
            return View(item);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment item)
        {
            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
            {
                ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                return View(item);
            }
            try
            {
                using (var scope = new TransactionScope())
                {
                    item.Users_Id = _myPermissionsManagerService.permissions.Id;
                    item.Date = DateTime.Now;
                    _myDataBaseService.AddComments(item);
                    scope.Complete();
                }
                return RedirectToAction("Index", new { Id = item.Posts_Id });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't create comment.");
                return View(item);
            }
        }

        //GET
        public async Task<IActionResult> Edit(int Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var com = new Comment();
            com.Id = Id;
            try
            {
                using (var scope = new TransactionScope())
                {
                    var comments = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={Id};");
                    if (comments.Count <= 0)
                    {
                        throw new Exception("No Comment with this Id");
                    }
                    var comment = comments.First();
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(comment.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
                    {
                        ModelState.AddModelError(string.Empty, "Brak uprawnień do edycji tego komentarza");
                        return View(com);
                    }
                    com = comment;
                    scope.Complete();
                }
                return View(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                return View(com);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Comment comment)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                var commentEdit = new Comment();
                using (var scope = new TransactionScope())
                {
                    var comments = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={comment.Id};");
                    if (comments.Count <= 0)
                    {
                        throw new Exception("No Comment with this Id");
                    }
                    commentEdit = comments.First();
                    commentEdit.Text = comment.Text;
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(commentEdit.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
                    {
                        ModelState.AddModelError(string.Empty, "Brak uprawnień do edycji tego komentarza");
                        return View(comment);
                    }
                    _myDataBaseService.UpdateComment(commentEdit);
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Komentarz został zedytowany poprawnie");
                return View(commentEdit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                return View(comment);
            }
        }

        //GET
        public async Task<IActionResult> Delete(int Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var item = new Comment();
            item.Id = Id;
            try
            {
                using (var scope = new TransactionScope())
                {
                    var comments = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={Id};");
                    if (comments.Count <= 0)
                    {
                        throw new Exception("No Comment with this Id");
                    }
                    var comment = comments.First();
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(comment.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
                    {
                        ModelState.AddModelError(string.Empty, "Brak uprawnień do usunięcia tego komentarza");
                        return View(item);
                    }
                    item = comment;
                    scope.Complete();
                }
                return View(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                return View(item);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Comment comment)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                var commentEdit = new Comment();
                using (var scope = new TransactionScope())
                {
                    var comments = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={comment.Id};");
                    if (comments.Count <= 0)
                    {
                        throw new Exception("No Comment with this Id");
                    }
                    commentEdit = comments.First();
                    commentEdit.Deleted = true;
                    commentEdit.ModedBy = _myPermissionsManagerService.permissions.Id;
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(commentEdit.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
                    {
                        ModelState.AddModelError(string.Empty, "Brak uprawnień do edycji tego komentarza");
                        return View(comment);
                    }
                    _myDataBaseService.UpdateComment(commentEdit);
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Komentarz został usunięty");
                return View(commentEdit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                return View(comment);
            }
        }

        public IActionResult Report()
        {
            return RedirectToAction("Index");
        }

    }
}
