using MaidEasy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MaidEasy.Controllers
{
    public class UserController : Controller
    {
        CustomDbContext dbContext = new CustomDbContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult feedback()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            int id = Int32.Parse(Request["maid"].ToString());

            worker worker = dbContext.worker.Find(id);
            return View(worker);
        }

        private string getThana(string s)
        {
            int id = 0, len = s.Length;
            for (int i = 0; i < len; i++)
            {
                if (s[i] == '1')
                { id = i + 1; break; }
            }
            thana thana = dbContext.thana.Find(id);
            return thana.Name;

            /*DBHelper db = DBHelper.getDB();
            string sql = "SELECT Name from Thana where ThanaId  = '" + id + "'";
            var table = db.getData(sql);
            table.Read();
            string ret = table.GetString(0);
            table.Close();
            return ret;*/
        }

        [HttpGet]
        public ActionResult user_profile(int id)
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");

            if (id != Int32.Parse(Session["userID"].ToString()) )
            {
                ViewData["me"] = "No";
            }
            else
            {
                ViewData["me"] = "Yes";
            }

            user user = dbContext.users.Find(id);

            /*System.Diagnostics.Debug.WriteLine("--------Image------------");
            System.Diagnostics.Debug.WriteLine(user.image);
            System.Diagnostics.Debug.WriteLine("--------------------");*/

            ViewData["thana"] = getThana(user.thana.ToString());

            return View(user);
        }

        public ActionResult hired_workers_profile()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            /*DBHelper db = DBHelper.getDB();
            int id = Int32.Parse(Session["userID"].ToString());
            string sql = "SELECT count(WorkerId) from Contracts where UserId = '" + id + "' and status = 'current'";
            var table = db.getData(sql);
            table.Read();
            int cnt1 = Int32.Parse(table.GetString(0));
            table.Close();
            string[,] data1 = new string[cnt1, 9];
            //sql = "SELECT WorkerName, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist, WorkerId  from Contracts where UserId = '" + id + "' and status = 'current'";
            sql = "SELECT WorkerName, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist, Worker.WorkerId,image  from Contracts JOIN Worker ON Contracts.WorkerId = Worker.WorkerId where UserId = '" + id + "' and Contracts.status = 'current' ";
            table = db.getData(sql);
            int i = 0;
            while(table.Read())
            {
                data1[i, 0] = table.GetString(0);
                data1[i, 1] = table.GetString(1);
                data1[i, 2] = table.GetString(2);
                data1[i, 3] = table.GetString(3);
                data1[i, 4] = table.GetString(4);
                data1[i, 5] = table.GetString(5);
                data1[i, 6] = table.GetString(6);
                data1[i, 7] = table.GetString(7);
                data1[i, 8] = table.GetString(8);
                i++;
            }
            table.Close();


            sql = "SELECT count(WorkerId) from Contracts where UserId = '" + id + "' and status = 'previous'";
            table = db.getData(sql);
            table.Read();
            int cnt2 = Int32.Parse(table.GetString(0));
            table.Close();
            string[,] data2 = new string[cnt2, 9];
            //sql = "SELECT WorkerName, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist, WorkerId from Contracts where UserId = '" + id + "' and status = 'previous'";
            sql = "SELECT WorkerName, StartMonth, EndMonth, StartTime, EndTime, Amount, Worklist, Worker.WorkerId,image  from Contracts JOIN Worker ON Contracts.WorkerId = Worker.WorkerId where UserId = '" + id + "' and Contracts.status = 'previous' ";
            table = db.getData(sql);
            i = 0;
            while (table.Read())
            {
                data2[i, 0] = table.GetString(0);
                data2[i, 1] = table.GetString(1);
                data2[i, 2] = table.GetString(2);
                data2[i, 3] = table.GetString(3);
                data2[i, 4] = table.GetString(4);
                data2[i, 5] = table.GetString(5);
                data2[i, 6] = table.GetString(6);
                data2[i, 7] = table.GetString(7);
                data2[i, 8] = table.GetString(8);
                i++;
            }
            table.Close();


            ViewData["cnt1"] = cnt1;        ViewData["cnt2"] = cnt2;
            ViewData["data1"] = data1;      ViewData["data2"] = data2;*/



            /*IEnumerable<contract> currentWorker = dbContext.contracts.ToList().Where(m => m.status == "current");
            /IEnumerable<HiredWorker> currentWorker = dbContext.contracts.Join(dbContext.worker,
                contracts=> contracts.WorkerId,
                worker=> worker.WorkerId,
                (contracts, worker) => new {Contracts = contracts, Worker = worker })
            IEnumerable<contract> previousWorker = dbContext.contracts.ToList().Where(m => m.status == "previous");

            var viewModel = new HiredWorkerModel();
            viewModel.currentWorker = currentWorker;*/

            var viewModel = new HiredWorkerModel();
            List<HiredWorker> a = new List<HiredWorker>();
            List<HiredWorker> b = new List<HiredWorker>();

            var query = from c in dbContext.contracts
                        join w in dbContext.worker on c.WorkerId equals w.WorkerId
                        where c.status == "current"
                        select new
                        {
                            workerId = w.WorkerId,
                            workerName = c.WorkerName,
                            startMonth = c.StartMonth,
                            endMonth = c.EndMonth,
                            startTime = c.startTime,
                            endTime = c.endTime,
                            amount = c.Amount,
                            workList = c.Worklist,
                            image = w.image,
                        };


            foreach(var item in query)
            {
                HiredWorker hw = new HiredWorker();
                hw.workerId = item.workerId;
                hw.workerName = item.workerName;
                hw.startMonth = item.startMonth;
                hw.endMonth = item.endMonth;
                hw.startTime = item.startTime;
                hw.endTime = item.endTime;
                hw.amount = item.amount;
                hw.workList = item.workList;
                hw.image = item.image;

                a.Add(hw);
            }

            query = from c in dbContext.contracts
                    join w in dbContext.worker on c.WorkerId equals w.WorkerId
                    where c.status == "previous"
                    select new
                    {
                        workerId = w.WorkerId,
                        workerName = c.WorkerName,
                        startMonth = c.StartMonth,
                        endMonth = c.EndMonth,
                        startTime = c.startTime,
                        endTime = c.endTime,
                        amount = c.Amount,
                        workList = c.Worklist,
                        image = w.image,
                    };

            foreach (var item in query)
            {
                HiredWorker hw = new HiredWorker();
                hw.workerId = item.workerId;
                hw.workerName = item.workerName;
                hw.startMonth = item.startMonth;
                hw.endMonth = item.endMonth;
                hw.startTime = item.startTime;
                hw.endTime = item.endTime;
                hw.amount = item.amount;
                hw.workList = item.workList;
                hw.image = item.image;

                b.Add(hw);
            }

            viewModel.currentWorker = a;
            viewModel.previousWorker = b;

            return View(viewModel);
        }
        public ActionResult Edit_profile()
        {
            if (Session["username"] == null) return RedirectToAction("Index", "Home");
            if (Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            if (TempData["message"] != null) //It will true when Password not match with DB password 
                ViewBag.Error = TempData["message"].ToString();

            int id = Int32.Parse(Session["userID"].ToString());
            user user = dbContext.users.Find( id );
            user.thana = getThana(user.thana);
            return View(user);

        }
        [HttpPost]
        public ActionResult EntryFeedback()
        {
            var comment = Request["comment"];
            var rating = Request["rating"];
            var wID = Request["maid"];

            DBHelper db = DBHelper.getDB();
            string sql = "INSERT into WorkerReview (WorkerId, rating , username , description )VALUES( '" + wID + " ', ' " + rating + "', '" + Session["username"] + "', '" + comment + " ');";
            db.setData(sql);
            sql = "SELECT sum(rating),COUNT(Id) from workerreview WHERE WorkerId = '" + wID + "' ";
            var table = db.getData(sql);
            table.Read();
            double rat = Convert.ToDouble(table.GetString(0));
            int c = Int32.Parse(table.GetString(1));
            table.Close();

            rat = rat / c;

            sql = "UPDATE Worker SET rating  = '" + rat + "' where WorkerId = '" + wID + "'";
            db.setData(sql);

            workerreview review = new workerreview();
            review.WorkerId = Int32.Parse(wID);
            review.rating = Int32.Parse(rating);
            review.username = Session["username"].ToString();
            review.description = comment;

            //return View("~/Views/User/user_profile.cshtml");
            return RedirectToAction("user_profile", "User", new { id = Session["userID"] });
        }
    }
}