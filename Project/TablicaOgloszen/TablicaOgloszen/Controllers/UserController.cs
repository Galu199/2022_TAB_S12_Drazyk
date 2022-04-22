using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TablicaOgloszen.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
