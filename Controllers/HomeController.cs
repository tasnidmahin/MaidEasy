using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBHelper db = DBHelper.getDB();
            db.DBConnection();
            string Date = DateTime.Now.ToString("dd");
            string Month = DateTime.Now.ToString("MM");
            if(Date.Equals("28"))
            {
                string sql = "SELECT COUNT(DISTINCT WorkerId) from contracts WHERE status = 'current'";
                var table = db.getData(sql);
                table.Read();
                int count = Int32.Parse(table.GetString(0));
                table.Close();
                if(count>0)
                {
                    int[] wID = new int[count];
                    sql = "SELECT DISTINCT WorkerId from contracts WHERE status = 'current'";
                    table = db.getData(sql);
                    int i = 0;
                    while(table.Read())
                    {
                        wID[i] = Int32.Parse(table.GetString(0));
                        System.Diagnostics.Debug.WriteLine("---------H O M E-----------");
                        System.Diagnostics.Debug.WriteLine(wID[i]);
                        System.Diagnostics.Debug.WriteLine("------------------------------");
                        i++;
                    }
                    table.Close();


                    sql = "UPDATE Worker SET experience = experience +1 WHERE WorkerId IN (";
                    for(i=0;i<count;i++)
                    {
                        if (i != 0) sql += ", ";
                        sql += wID[i];
                    }
                    sql += ")";
                    System.Diagnostics.Debug.WriteLine(sql);
                    db.setData(sql);
                }
            }
            else if(Date.Equals("01"))
            {
                string Year = DateTime.Now.ToString("yyyy");
                Year = Month + "/" + Year;
                string sql = "UPDATE Contracts SET status = 'previous' WHERE EndMonth = '" + Year + "'";
                db.setData(sql);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}