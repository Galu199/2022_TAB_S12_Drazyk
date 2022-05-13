using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View(_myDataBaseService.QueryComments($"SELECT * FROM Comments WHERE Posts_Id={Id} ORDER BY DATE DESC;"));
        }

        public IActionResult Create()
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

        public IActionResult Report()
        {
            return RedirectToAction("Index");
        }

    }
}
