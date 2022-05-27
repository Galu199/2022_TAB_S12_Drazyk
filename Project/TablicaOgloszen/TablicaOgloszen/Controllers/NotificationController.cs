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
    public class NotificationController : Controller
    {
        private readonly MyDataBaseManagerService _myDataBaseService;
        private readonly MyPermissionsManagerService _myPermissionsService;
        public NotificationController(MyDataBaseManagerService myDataBaseService,
            MyPermissionsManagerService myPermissionsManagerService)
        {
            _myDataBaseService = myDataBaseService;
            _myPermissionsService = myPermissionsManagerService;
        }

        public async Task<IActionResult> Index()
        {
            await _myPermissionsService.getPermissions(User);
            var notifications = new List<Notification>();
            try
            {
                if (_myPermissionsService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                    throw (new Exception("No permissions for this tab"));
                }
                using (var scope = new TransactionScope())
                {
                    notifications = _myDataBaseService.QueryNotifications($"SELECT * FROM Notifications WHERE Users_Id='{_myPermissionsService.permissions.Id}' ORDER BY Date DESC;");
                    scope.Complete();
                }
                return View(notifications);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                ModelState.AddModelError(string.Empty, "error massage.");
                return View(notifications);
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _myPermissionsService.getPermissions(User);
            var notifications = new List<Notification>();
            try
            {
                if (_myPermissionsService.permissions.Level < PermissionsRole.User)
                {
                    ModelState.AddModelError(string.Empty, "Proszę się zarejestrować!");
                    throw (new Exception("No permissions for this tab"));
                }
                using (var scope = new TransactionScope())
                {
                    _myDataBaseService.DeleteNotification(Id);
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ModelState.AddModelError(string.Empty, "error massage.");
                return View();
            }
        }
    }
}
