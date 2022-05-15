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
        public CommentController(
            MyDataBaseService myDataBaseService
            )
        {
            _myDataBaseService = myDataBaseService;
        }

        public IActionResult Index(int Id)
        {
            var commentIndex = new CommentIndex();
            try
            {
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
                ModelState.AddModelError(string.Empty, "error massage.");
                return View(commentIndex);
            }
        }

        public IActionResult Create()
        {
            return View();
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
