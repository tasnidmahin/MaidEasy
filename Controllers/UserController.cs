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
                        where c.status == "current" && c.UserId == ( Int32.Parse(Session["userID"].ToString()) )
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
                    where c.status == "previous" && c.UserId == (Int32.Parse(Session["userID"].ToString()))
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
            review.WorkerId = Int32.Parse(wID.ToString());
            review.rating = Convert.ToDouble(rating.ToString());
            review.username = Session["username"].ToString();
            review.description = comment;

            //return View("~/Views/User/user_profile.cshtml");
            return RedirectToAction("user_profile", "User", new { id = Session["userID"] });
        }
    }
}