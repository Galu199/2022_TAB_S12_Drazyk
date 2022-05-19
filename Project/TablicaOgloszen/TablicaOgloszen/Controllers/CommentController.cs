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
                    commentIndex.comments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Posts_Id={Id} ORDER BY DATE DESC");
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
                    item.Users_Id=_myPermissionsManagerService.permissions.Id;
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

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Comment comment)
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Report()
        {
            return RedirectToAction("Index");
        }

    }
}
