using Microsoft.AspNetCore.Identity;
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
            var postList = new List<Post>();
            var postIndexList = new List<PostIndex>();
            try
            {
                using (var scope = new TransactionScope())
                {
                    postList = _myDataBaseService.GetPosts();
                    foreach (var post in postList)
                    {
                        var postIndex = new PostIndex(post);
                        postIndex.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{post.Users_Id}';").First();
                        postIndex.Comments = _myDataBaseService.QueryComments($"SELECT TOP 3 * FROM Comments WHERE Posts_Id={post.Id} AND Deleted = 0 ORDER BY DATE DESC;");
                        postIndexList.Add(postIndex);

                    }
                    scope.Complete();
                }
                return View(postIndexList);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't display posts.");
                return View(postIndexList);
            }
        }

        public IActionResult Details(int Id)
        {
            PostDetails postDetails = null;
            try
            {
                using (var scope = new TransactionScope())
                {
                    var postlist = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}");
                    if (postlist.Count <= 0) return View(postDetails);
                    postDetails = new PostDetails(postlist.First());
                    postDetails.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{postDetails.Users_Id}';").First();
                    postDetails.Comments = _myDataBaseService.QueryComments($"SELECT TOP 5 * FROM Comments WHERE Posts_Id={Id} AND Deleted = 0 ORDER BY DATE DESC;");
                    postDetails.CommentsCount = _myDataBaseService.QueryAggregate($"SELECT COUNT(Id) FROM Comments WHERE Posts_Id={Id} AND Deleted = 0;");
                    postDetails.Tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id}");
                    scope.Complete();
                }
                return View(postDetails);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "You tried to display illegal material. Your IP address has been forwarded to various law enforcemenet agencies..");
                return View(postDetails);
            }
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
            try
            {
                using (var scope = new TransactionScope())
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
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Coudn't create post.");
                return RedirectToAction("Index");
            }
        }

        //GET
        public IActionResult Delete()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Post post)
        {
            var postEdit = new Post();
            try
            {
                using (var scope = new TransactionScope())
                {
                    //postEdit = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id};").First();
                    //postEdit.Deleted = true;
                    scope.Complete();
                }
                return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't delete post.");
                return View();
            }
        }

        //GET
        public IActionResult Edit(int Id)
        {
            var postEdit = new Post();
            try
            {
                using (var scope = new TransactionScope())
                {
                    postEdit = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id};").First();
                    scope.Complete();
                }
                return View(postEdit);
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Post not found.");
                return View(postEdit);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            var postEdit = new Post();
            try
            {
                using (var scope = new TransactionScope())
                {
                    postEdit = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={post.Id};").First();
                    postEdit.Title = post.Title;
                    postEdit.Text = post.Text;
                    _myDataBaseService.UpdatePost(postEdit);
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Post editted sucesfully.");
                return View(postEdit);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't edit post.");
                return View(postEdit);
            }

        }

        public IActionResult Report()
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    scope.Complete();
                }
                return RedirectToAction("Details");
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "error massage.");
                return RedirectToAction("Details");
            }
        }

        public IActionResult Tags(int Id)
        {
            var postTag = new PostTags();
            try
            {
                using (var scope = new TransactionScope())
                {
                    postTag.post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}").First();
                    postTag.tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id}");
                    scope.Complete();
                }
                return View(postTag);

            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Coudn't display tags.");
                return View(postTag);
            }
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
            try
            {
                using (var scope = new TransactionScope())
                {
                    var tag = new Tag();
                    tag.Text = Text;
                    tag.Posts_Id = Posts_Id;
                    _myDataBaseService.AddTag(tag);
                    scope.Complete();
                }
                return RedirectToAction("Tags", new { Id = Posts_Id });
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Coudn't add that tag.");
                return RedirectToAction("Tags", new { Id = Posts_Id });
            }
        }

        public IActionResult DeleteTag(int Id, int Posts_Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _myDataBaseService.DeleteTag(Id);
                    scope.Complete();
                }
                return RedirectToAction("Tags", new { Id = Posts_Id });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't delete tag.");
                return RedirectToAction("Tags", new { Id = Posts_Id });
            }
        }

        public IActionResult UpVote(int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //.Rating += 1;
                    scope.Complete();
                }
                return RedirectToAction("Details", new { Id = Id });
            }
            catch 
            {
                ModelState.AddModelError(string.Empty, "Coudn't upvote.");
                return RedirectToAction("Details", new { Id = Id });
            }
        }

    }
}
