﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TablicaOgloszen.Services;

namespace TablicaOgloszen.Controllers
{

    public class UserController : Controller
    {
        private readonly MyDataBaseService _myDataBaseService;
        public UserController(
            MyDataBaseService myDataBaseService
            )
        {
            _myDataBaseService = myDataBaseService;
        }

        public IActionResult Index()
        {
            return View(_myDataBaseService.QueryUsers("SELECT * FROM Users"));
        }

        public IActionResult Details(string Id)
        {
            Models.UserDetails person = new Models.UserDetails(_myDataBaseService.QueryUsers($"SELECT * FROM Users WHERE Id='{Id}';").First());
            person.Posts = _myDataBaseService.QueryPosts($"SELECT * FROM Posts WHERE Users_Id='{Id}'");
            person.Comments = _myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Users_Id='{Id}'");
            return View(person);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult ModGrant()
        {
            return RedirectToAction("Index");
        }

        public IActionResult ModRevoke()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Ban()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Unban()
        {
            return RedirectToAction("Index");
        }
    }
}
