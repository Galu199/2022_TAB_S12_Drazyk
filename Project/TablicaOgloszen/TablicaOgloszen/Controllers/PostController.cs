using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TablicaOgloszen.Models;
using TablicaOgloszen.Services;

namespace TablicaOgloszen.Controllers
{

    public class PostController : Controller
    {
        private readonly MyDataBaseService _myDataBaseService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PostController(
            MyDataBaseService myDataBaseService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _myDataBaseService = myDataBaseService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var postList = _myDataBaseService.GetPosts();
            var postIndexList = new List<PostIndex>();
            foreach (var post in postList)
            {
                var postIndex = new PostIndex(post);
                postIndex.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{post.Users_Id}';").First();
                postIndex.Comments = _myDataBaseService.QueryComments($"SELECT TOP 3 * FROM Comments WHERE Posts_Id={post.Id} AND Deleted = 0 ORDER BY DATE DESC;");
                postIndexList.Add(postIndex);
            }
            return View(postIndexList);
        }

        public IActionResult Details(int Id)
        {
            PostDetails postDetails = null;
            var postlist = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}");
            if (postlist.Count <= 0) return View(postDetails);
            postDetails = new PostDetails(postlist.First());
            postDetails.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{postDetails.Users_Id}';").First();
            postDetails.Comments = _myDataBaseService.QueryComments($"SELECT TOP 5 * FROM Comments WHERE Posts_Id={Id} AND Deleted = 0 ORDER BY DATE DESC;");
            postDetails.CommentsCount = _myDataBaseService.QueryAggregate($"SELECT COUNT(Id) FROM Comments WHERE Posts_Id={Id} AND Deleted = 0;");
            postDetails.Tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id}");
            return View(postDetails);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Title, string Text)
        {
            var post = new Post();
            post.Title = Title;
            post.Text = Text;
            post.Rating = 0;
            post.Date = DateTime.Now;
            post.Pinned = false;
            post.Deleted = false;
            post.Users_Id = _userManager.GetUserId(User);
            post.ModedBy = null;
            _myDataBaseService.AddPost(post);
            return RedirectToAction("Index");
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

        public IActionResult Tags(int Id)
        {
            var postTag = new PostTags();
            postTag.post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}").First();
            postTag.tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id}");
            return View(postTag);
        }

        //GET
        public IActionResult AddTag(int Id)
        {
            var tag = new Tag();
            tag.Posts_Id = Id;
            return View(tag);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTag(string Text, int Posts_Id)
        {
            var tag = new Tag();
            tag.Text = Text;
            tag.Posts_Id = Posts_Id;
            _myDataBaseService.AddTag(tag);
            return RedirectToAction("Tags", new { Id = Posts_Id });
        }

        public IActionResult DeleteTag(int Id, int Posts_Id)
        {
            _myDataBaseService.DeleteTag(Id);
            return RedirectToAction("Tags", new { Id = Posts_Id });
        }

        public IActionResult UpVote(int Id)
        {
            //.Rating += 1;
            return RedirectToAction("Details", new { Id = Id });
        }

    }
}
