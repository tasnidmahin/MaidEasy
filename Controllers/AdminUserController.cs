using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            List<UserLIstModel> userList = new List<UserLIstModel>();
            List<int> userIDs = new List<int>();
            DBHelper db = DBHelper.getDB();
            string sql = "SELECT UserId from users";
            var table = db.getData(sql);
            while (table.Read())
            {
                int id = Int32.Parse(table.GetString(0));
                userIDs.Add(id);
            }
            table.Close();
            for (int i = 0; i < userIDs.Count; i++)
            {
                UserLIstModel ULModel = new UserLIstModel(userIDs[i]);
                userList.Add(ULModel);
            }
            ViewData["list"] = userList;
            return View();
        }
    }
}