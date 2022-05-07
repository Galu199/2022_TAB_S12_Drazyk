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
            return View(_myDataBaseService.GetPosts());
        }

        public IActionResult Details()
        {
            return View();
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
