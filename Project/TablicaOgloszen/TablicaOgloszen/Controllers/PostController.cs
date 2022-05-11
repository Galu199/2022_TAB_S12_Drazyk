using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TablicaOgloszen.Services;

namespace TablicaOgloszen.Controllers
{

    public class PostController : Controller
    {
        private readonly MyDataBaseService _myDataBaseService;
        public PostController(
            MyDataBaseService myDataBaseService
            )
        {
            _myDataBaseService = myDataBaseService;
        }

        public IActionResult Index()
        {
            var postList = _myDataBaseService.GetPosts();
            var postIndexList = new List<Models.PostIndex>();
            foreach(var post in postList)
            {
                var postIndex = new Models.PostIndex(post);
                postIndex.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{post.Users_Id}';").First();
                postIndex.Comments = _myDataBaseService.QueryComments($"SELECT TOP 3 * FROM Comments WHERE Posts_Id={post.Id} AND Deleted = 0;");
                postIndexList.Add(postIndex);
            }
            return View(postIndexList);
        }

        public IActionResult Details(int Id)
        {
            Models.Post post = null;
            var postlist = _myDataBaseService.QueryPosts($"SELECT * FROM Posts WHERE Id={Id}");
            if (postlist.Count > 0) post = postlist.First();
            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Report()
        {
            return RedirectToAction("Details");
        }

        public IActionResult Tags()
        {
            return RedirectToAction("Details");
        }

        public IActionResult AddTag()
        {
            return RedirectToAction("Details");
        }

        public IActionResult DeleteTag()
        {
            return RedirectToAction("Details");
        }

    }
}
