using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Service()
        {
            //if(Session["username"] == null) return RedirectToAction("Index", "Home");
            //if(Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            return View();
        }

        private int convert (string end)
        {
            if (end == "6:00 AM") return 0;
            else if (end == "6:30 AM") return 1;
            else if (end == "7:00 AM") return 2;
            else if (end == "7:30 AM") return 3;
            else if (end == "8:00 AM") return 4;
            else if (end == "8:30 AM") return 5;
            else if (end == "9:00 AM") return 6;
            else if (end == "9:30 AM") return 7;
            else if (end == "10:00 AM") return 8;
            else if (end == "10:30 AM") return 9;
            else if (end == "11:00 AM") return 10;
            else if (end == "11:30 AM") return 11;
            else if (end == "12:00 PM") return 12;
            else if (end == "12:30 PM") return 13;
            else if (end == "1:00 PM") return 14;
            else if (end == "1:30 PM") return 15;
            else if (end == "2:00 PM") return 16;
            else if (end == "2:30 PM") return 17;
            else if (end == "3:00 PM") return 18;
            else if (end == "3:30 PM") return 19;
            else if (end == "4:00 PM") return 20;
            else if (end == "4:30 PM") return 21;
            else if (end == "5:00 PM") return 22;
            else if (end == "5:30 PM") return 23;
            else return 24;
        }

        private int findType (string s)
        {
            if (s == "temporary") return 0;
            else if (s == "permanent") return 1;
            else if (s == "babycare") return 2;
            return 3;
        }

        [HttpGet]
        public ActionResult Searching()
        {
            var t = Request["type"].ToString();
            var sortby = Request["sortby"].ToString();
            var startTime = Request["startTime"].ToString();
            var endTime = Request["endTime"].ToString();

            int type = findType(t);

            int start = convert(startTime);
            int end = convert(endTime);

            string status = "";
            for(int i1=start;i1<=end;i1++) status += "0";
            
                
            System.Diagnostics.Debug.WriteLine(type);
            System.Diagnostics.Debug.WriteLine(status);

            // sql for count of workers
            string sql = "SELECT count(WorkerId) from Worker where SUBSTRING(type, " + (type+1)  + ", 1) = '1' and SUBSTRING(status," + (start+1) + "," + (end - start + 1 )+ ") = '" + status + "';";
            //string sql = "SELECT count(WorkerId) from Worker where (SELECT SUBSTRING(type, 0, 1) from Worker) = '1' and (SELECT SUBSTRING(status, 0, 4) from Worker) = '0000';";
            //select insert(str, 3, 1, '*')
            //select SUBSTRING(@meme,2,1)


            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();

            string[,] data = new string[count,4];
            //byte[,] photo = new byte[count, 4294967295];
            //sql for data of workers
            sql = "SELECT WorkerId,Name,rating,experience,image from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "';";

            table = db.getData(sql);
            int i = 0;
            while(table.Read())
            {
                System.Diagnostics.Debug.WriteLine("---------------------------------------------------");
                data[i, 0] = table.GetString(0);
                data[i, 1] = table.GetString(1);
                data[i, 2] = table.GetString(2);
                data[i, 3] = table.GetString(3);
                System.Diagnostics.Debug.WriteLine("---" + i + "----" );
                System.Diagnostics.Debug.WriteLine(data[i, 0]);
                System.Diagnostics.Debug.WriteLine(data[i, 1]);
                System.Diagnostics.Debug.WriteLine(data[i, 2]);
                System.Diagnostics.Debug.WriteLine(data[i, 3]);
                //photo[i] = Encoding.ASCII.GetBytes(table.GetString(4));
                //Array.Copy(photo[i], Encoding.ASCII.GetBytes(table.GetString(4)), 4294967295);
                i++;
            }
            table.Close();

            ViewData["workerData"] = data;

            ViewData["cnt_row"] = count;

            return View("~/Views/Service/Service.cshtml");
        }



    }
}