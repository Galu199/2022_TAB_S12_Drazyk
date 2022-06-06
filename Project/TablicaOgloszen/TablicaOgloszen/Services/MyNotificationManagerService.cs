using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TablicaOgloszen.Models;

namespace TablicaOgloszen.Services
{
    public class MyNotificationManagerService
    {
        private readonly MyDataBaseManagerService _myDataBaseManagerService;
        private readonly MyPermissionsManagerService _myPermissionsManagerService;

        public MyNotificationManagerService(MyDataBaseManagerService myDataBaseManagerService,
            MyPermissionsManagerService myPermissionsManagerService)
        {
            _myDataBaseManagerService = myDataBaseManagerService;
            _myPermissionsManagerService = myPermissionsManagerService;
        }

        public void Modded(string Id, bool bul)
        {
            var notification = new Notification();
            notification.Date = DateTime.Now;
            if (bul)
            {
                notification.Text = "You has just been granted mod";
            }
            else
            {
                notification.Text = "You has been demoded";
            }
            using (var scope = new TransactionScope())
            {
                notification.Users_Id = Id;
                _myDataBaseManagerService.AddNotification(notification);
                scope.Complete();
            }
        }

        public void PostRemoved<T>(User receiver, T _item)
        {
            var notification = new Notification();
            notification.Date = DateTime.Now;
            if (_item is Post)
            {
                var post = (Post)Convert.ChangeType(_item, typeof(Post));
                notification.Text = $"Your post: {post.Title} has been removed by moderator. Uncool!";

            }
            else if (_item is Comment)
            {
                var comment = (Comment)Convert.ChangeType(_item, typeof(Comment));
                notification.Text = $"Your comment: {comment.Text} has been removed by moderator. Uncool!";

            }
            using (var scope = new TransactionScope())
            {
                notification.Users_Id = receiver.Id;
                _myDataBaseManagerService.AddNotification(notification);
                scope.Complete();
            }
        }

        public void CommentAdded(User sender, Post post, User receiver, Comment comment)
        {
            var notification = new Notification();
            notification.Date = DateTime.Now;
            notification.Text = $"User: <a href='/User/Details/{sender.Id}'>{sender.UserName}</a> added comment: <a href='/Comment/Index/{post.Id}'>{comment.Text}</a> under your Post: <a href='/Post/Details/{post.Id}'>{post.Title}</a> So cool.";

            using (var scope = new TransactionScope())
            {
                notification.Users_Id = receiver.Id;
                _myDataBaseManagerService.AddNotification(notification);
                scope.Complete();
            }
        }

        public void reportToMods<T>(User sender, T _item)
        {
            var notification = new Notification();
            notification.Date = DateTime.Now;
            if (_item is Post)
            {
                var post = (Post)Convert.ChangeType(_item, typeof(Post));
                notification.Text = $"User: <a href='/User/Details/{sender.Id}'>{sender.UserName}</a> reported Post: <a href='/Post/Details/{post.Id}'>{post.Title}</a> Please check it out.";
            }
            else if (_item is Comment)
            {
                var comment = (Comment)Convert.ChangeType(_item, typeof(Comment));
                notification.Text = $"User: <a href='/User/Details/{sender.Id}'>{sender.UserName}</a> reported Comment: <a href='/Comment/Delete/{comment.Id}'>{comment.Text}</a> Please check it out.";
            }
            using (var scope = new TransactionScope())
            {

                var moderators = _myDataBaseManagerService.QueryUsers(
@$"SELECT us.Id,us.Email,us.UserName,us.PhoneNumber,us.Ban,us.ModedBy
FROM Users as us 
JOIN AspNetUserRoles as usrol ON (us.Id = usrol.UserId)
JOIN AspNetRoles as rol ON (usrol.RoleId = rol.Id)
WHERE
rol.Name = '{new Permissions().PermissionsRolesDictionary[PermissionsRole.Moderator]}';");

                int mods = moderators.Count();
                int notifs = _myDataBaseManagerService.QueryNotifications("SELECT * FROM Notifications WHERE Text LIKE '%reported%'").Count();

                var mod = moderators[notifs % mods];

                notification.Users_Id = mod.Id;
                _myDataBaseManagerService.AddNotification(notification);

             //   foreach (var mod in moderators)
                {
              //      notification.Users_Id = mod.Id;
             //       _myDataBaseManagerService.AddNotification(notification);
                }
                scope.Complete();
            }
        }
    }
}
