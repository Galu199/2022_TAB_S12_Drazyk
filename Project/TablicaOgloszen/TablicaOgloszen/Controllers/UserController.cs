using Microsoft.AspNetCore.Mvc;
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
            return View(_myDataBaseService.Query(null));
        }

        public IActionResult Details()
        {
            return View();
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
