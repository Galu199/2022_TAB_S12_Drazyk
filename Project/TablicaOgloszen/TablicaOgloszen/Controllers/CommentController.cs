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
        private readonly MyDataBaseManagerService _myDataBaseService;
        private readonly MyPermissionsManagerService _myPermissionsService;
        private readonly MyNotificationManagerService _myNotificationService;
        public CommentController(MyDataBaseManagerService myDataBaseService,
            MyPermissionsManagerService myPermissionsManagerService,
            MyNotificationManagerService myNotificationManagerService)
        {
            _myDataBaseService = myDataBaseService;
            _myPermissionsService = myPermissionsManagerService;
            _myNotificationService = myNotificationManagerService;
        }

        //GET
        public async Task<IActionResult> Index(int Id)
        {
            await _myPermissionsService.getPermissions(User);
            try
            {
                if (_myPermissionsService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "Proszę się Zarejestrować.");
                    return View(null);
                }
                var commentIndex = new CommentIndex();
                using (var scope = new TransactionScope())
                {
                    commentIndex.post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}").First();
                    List<Comment> comments;
                    if (_myPermissionsService.permissions.Level > PermissionsRole.User)
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
        public IActionResult Create(int Id)
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
            await _myPermissionsService.getPermissions(User);
            if (_myPermissionsService.permissions.Level < PermissionsRole.User)
            {
                ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                return View(item);
            }
            try
            {
                User sender;
                User receiver;
                Post post;
                using (var scope = new TransactionScope())
                {
                    item.Users_Id = _myPermissionsService.permissions.Id;
                    item.Date = DateTime.Now;
                    _myDataBaseService.AddComments(item);

                    sender = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{_myPermissionsService.permissions.Id}';").First();
                    post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={item.Posts_Id};").First();
                    receiver = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{post.Users_Id}';").First();

                    scope.Complete();
                }

                _myNotificationService.CommentAdded(sender, post, receiver, item);

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
            await _myPermissionsService.getPermissions(User);
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
                    if (!(_myPermissionsService.permissions.Id.Equals(comment.Users_Id) ||
                        _myPermissionsService.permissions.Level >= PermissionsRole.Moderator))
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
            await _myPermissionsService.getPermissions(User);
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
                    if (!(_myPermissionsService.permissions.Id.Equals(commentEdit.Users_Id) ||
                        _myPermissionsService.permissions.Level >= PermissionsRole.Moderator))
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
            await _myPermissionsService.getPermissions(User);
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
                    if (!(_myPermissionsService.permissions.Id.Equals(comment.Users_Id) ||
                        _myPermissionsService.permissions.Level >= PermissionsRole.Moderator))
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
            await _myPermissionsService.getPermissions(User);
            try
            {
                var commentEdit = new Comment();
                User receiver;
                using (var scope = new TransactionScope())
                {
                    var comments = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={comment.Id};");
                    if (comments.Count <= 0)
                    {
                        throw new Exception("No Comment with this Id");
                    }

                    commentEdit = comments.First();
                    commentEdit.Deleted = true;
                    commentEdit.ModedBy = _myPermissionsService.permissions.Id;

                    if (!(_myPermissionsService.permissions.Id.Equals(commentEdit.Users_Id) ||
                        _myPermissionsService.permissions.Level >= PermissionsRole.Moderator))
                    {
                        ModelState.AddModelError(string.Empty, "Brak uprawnień do edycji tego komentarza");
                        return View(comment);
                    }

                    if (_myPermissionsService.permissions.Level >= PermissionsRole.Moderator &&
                        commentEdit.Users_Id != _myPermissionsService.permissions.Id)
                    {
                        receiver = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{commentEdit.Users_Id}';").First();

                        _myNotificationService.PostRemoved(receiver, commentEdit);
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

        //GET
        public async Task<IActionResult> Report(int Id, int IdPost)
        {
            await _myPermissionsService.getPermissions(User);
            try
            {
                if (_myPermissionsService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                    throw (new Exception("No permissions for this tab"));
                }
                User sender;
                Comment comment;
                using (var scope = new TransactionScope())
                {
                    sender = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{_myPermissionsService.permissions.Id}';").First();
                    comment = _myDataBaseService.QueryComments($"SELECT TOP 1 * FROM Comments WHERE Id={Id};").First();
                    scope.Complete();
                }
                _myNotificationService.reportToMods(sender, comment);
                return RedirectToAction("Index", new { Id = IdPost });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "error massage.");
                return RedirectToAction("Index", new { Id = IdPost });
            }
        }

    }
}
