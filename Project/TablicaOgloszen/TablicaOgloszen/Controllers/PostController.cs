﻿using Microsoft.AspNetCore.Identity;
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
        private readonly MyPermissionsManagerService _myPermissionsManagerService;
        public PostController(MyDataBaseService myDataBaseService,
            MyPermissionsManagerService myPermissionsManagerService)
        {
            _myDataBaseService = myDataBaseService;
            _myPermissionsManagerService = myPermissionsManagerService;
        }

        //
        public async Task<IActionResult> Index()
        {
            await _myPermissionsManagerService.getPermissions(User);
            var postIndexList = new List<PostIndex>();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var postList = new List<Post>();
                    if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
                    {
                        ModelState.AddModelError(string.Empty, "Zarejestruj się aby wyświetlić zawartość!");
                        return View(null);
                    }
                    if (_myPermissionsManagerService.permissions.Level > PermissionsRole.User)
                    {
                        postList.AddRange(_myDataBaseService.QueryPosts("SELECT * FROM Posts WHERE Pinned = 1 ORDER BY DATE DESC;"));
                        postList.AddRange(_myDataBaseService.QueryPosts("SELECT * FROM Posts WHERE Pinned = 0 ORDER BY DATE DESC;"));
                    }
                    else
                    {
                        postList.AddRange(_myDataBaseService.QueryPosts("SELECT * FROM Posts WHERE Deleted = 0 AND Pinned = 1 ORDER BY DATE DESC;"));
                        postList.AddRange(_myDataBaseService.QueryPosts("SELECT * FROM Posts WHERE Deleted = 0 AND Pinned = 0 ORDER BY DATE DESC;"));
                    }
                    foreach (var post in postList)
                    {
                        var postIndex = new PostIndex();
                        postIndex.post = post;
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
                ModelState.AddModelError(string.Empty, "Wystąpił błąd.");
                return View(null);
            }
        }

        //
        public async Task<IActionResult> Details(int Id)
        {
            PostDetails postDetails = new PostDetails();
            try
            {
                await _myPermissionsManagerService.getPermissions(User);
                if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "You tried to display illegal material. Your IP address has been forwarded to various law enforcemenet agencies..");
                    return View(null);
                }
                using (var scope = new TransactionScope())
                {
                    var postlist = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}");
                    if (postlist.Count <= 0) return View(postDetails);
                    postDetails.Post = postlist.First();
                    postDetails.Owner = _myDataBaseService.QueryUsers($"SELECT TOP 1 * FROM Users WHERE Id='{postDetails.Post.Users_Id}';").First();
                    postDetails.Comments = _myDataBaseService.QueryComments($"SELECT TOP 5 * FROM Comments WHERE Posts_Id={Id} AND Deleted = 0 ORDER BY DATE DESC;");
                    postDetails.CommentsCount = (int)_myDataBaseService.QueryAggregate($"SELECT COUNT(Id) FROM Comments WHERE Posts_Id={Id} AND Deleted = 0;");
                    postDetails.AvgRating = _myDataBaseService.QueryAggregate($"SELECT AVG(Cast(Value as Float)) FROM Ratings WHERE Posts_Id={Id};");
                    postDetails.Tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id};");
                    var ratings = _myDataBaseService.QueryRatings($"SELECT * FROM Ratings WHERE Posts_Id={Id} AND Users_Id='{_myPermissionsManagerService.permissions.Id}';");
                    if (ratings.Count > 0)
                    {
                        postDetails.myRating = ratings.First();
                    }
                    scope.Complete();
                }
                return View(postDetails);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd");
                return View(null);
            }
        }

        //GET
        public async Task<IActionResult> Create()
        {
            await _myPermissionsManagerService.getPermissions(User);
            return View();
        }

        //POST
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Title, string Text)
        {
            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
            {
                ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                return View(null);
            }
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
                    post.Users_Id = _myPermissionsManagerService.permissions.Id;
                    post.ModedBy = null;
                    _myDataBaseService.AddPost(post);
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't create post.");
                return View(null);
            }
        }

        //GET
        public IActionResult Delete(int Id)
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
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Post post)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var postEdit = new Post();
            if (!(_myPermissionsManagerService.permissions.Id.Equals(post.Users_Id) ||
                _myPermissionsManagerService.permissions.Level >= PermissionsRole.Moderator))
            {
                ModelState.AddModelError(string.Empty, "You can't delete this post.");
                return View(post);
            }
            try
            {
                using (var scope = new TransactionScope())
                {
                    postEdit = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={post.Id};").First();
                    postEdit.Deleted = true;
                    postEdit.ModedBy = _myPermissionsManagerService.permissions.Id;
                    _myDataBaseService.UpdatePost(postEdit);
                    scope.Complete();
                }
                ModelState.AddModelError(string.Empty, "Post deleted sucesfully.");
                return View(postEdit);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Coudn't edit post.");
                return View(post);
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
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post)
        {
            var postEdit = new Post();
            await _myPermissionsManagerService.getPermissions(User);
            if (!(_myPermissionsManagerService.permissions.Id.Equals(post.Users_Id) ||
                _myPermissionsManagerService.permissions.Level >= PermissionsRole.Administrator))
            {
                ModelState.AddModelError(string.Empty, "You can't edit this post.");
                return View(post);
            }
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
                return View(post);
            }
        }

        //
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

        //
        public async Task<IActionResult> Tags(int Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            var postTag = new PostTags();
            try
            {
                using (var scope = new TransactionScope())
                {
                    postTag.post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Id}").First();
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(postTag.post.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level>=PermissionsRole.Administrator))
                    {
                        ModelState.AddModelError(string.Empty, "Nie możesz zarządzać tagami tego postu.");
                        return View(postTag);
                    }
                    postTag.tags = _myDataBaseService.QueryTags($"SELECT * FROM Tags WHERE Posts_Id={Id}");
                    scope.Complete();
                }
                return View(postTag);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Nie możesz zarządzać tagami tego postu.");
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
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTag(string Text, int Posts_Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                using (var scope = new TransactionScope())
                {
                    var post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Posts_Id}").First();
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(post.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Administrator))
                    {
                        ModelState.AddModelError(string.Empty, "Nie możesz zarządzać tagami tego postu.");
                        return View(null);
                    }
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
                //ModelState.AddModelError(string.Empty, "Coudn't add that tag.");
                return RedirectToAction("Tags", new { Id = Posts_Id });
            }
        }

        //
        public async Task<IActionResult> DeleteTag(int Id, int Posts_Id)
        {
            await _myPermissionsManagerService.getPermissions(User);
            try
            {
                using (var scope = new TransactionScope())
                {
                    var post = _myDataBaseService.QueryPosts($"SELECT TOP 1 * FROM Posts WHERE Id={Posts_Id}").First();
                    if (!(_myPermissionsManagerService.permissions.Id.Equals(post.Users_Id) ||
                        _myPermissionsManagerService.permissions.Level >= PermissionsRole.Administrator))
                    {
                        ModelState.AddModelError(string.Empty, "Nie możesz zarządzać tagami tego postu.");
                        return View(null);
                    }
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

        //
        public async Task<IActionResult> AddRating(int Id, int Value)
        {
            await _myPermissionsManagerService.getPermissions(User);
            if (_myPermissionsManagerService.permissions.Level < PermissionsRole.User)
            {
                return RedirectToAction("Details", new { Id = Id });
            }
            if (Value < 1 || Value > 5)
            {
                return RedirectToAction("Details", new { Id = Id });
            }
            try
            {
                using (var scope = new TransactionScope())
                {
                    var rate = _myDataBaseService.QueryRatings($"SELECT * FROM Ratings WHERE Posts_Id={Id} AND Users_Id='{_myPermissionsManagerService.permissions.Id}';");
                    if (rate.Count > 0)
                    {
                        rate.First().Value = Value;
                        _myDataBaseService.UpdateRating(rate.First());
                    }
                    else
                    {
                        var newRate = new Rating();
                        newRate.Posts_Id = Id;
                        newRate.Users_Id = _myPermissionsManagerService.permissions.Id;
                        newRate.Value = Value;
                        _myDataBaseService.AddRating(newRate);
                    }
                    scope.Complete();
                }
                return RedirectToAction("Details", new { Id = Id });
            }
            catch
            {
                return RedirectToAction("Details", new { Id = Id });
            }
        }

        //
        public async Task<IActionResult> PinTogggle(int Id)
        {
            try
            {
                await _myPermissionsManagerService.getPermissions(User);
                if (_myPermissionsManagerService.permissions.Level < PermissionsRole.Moderator)
                {
                    return RedirectToAction("Index");
                }
                using (var scope = new TransactionScope())
                {
                    var item = _myDataBaseService.QueryPosts($"SELECT * FROM Posts WHERE Id={Id}").First();
                    if (item.Pinned)
                    {
                        item.Pinned = false;
                    }
                    else
                    {
                        item.Pinned = true;
                    }
                    _myDataBaseService.UpdatePost(item);
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
