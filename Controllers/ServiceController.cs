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
            ViewData["tab"] = "temporary";
            return View();
        }

        public ActionResult Service()
        {
            //if(Session["username"] == null) return RedirectToAction("Index", "Home");
            //if(Session["username"] == null) return Content("<script language='javascript' type='text/javascript'>alert('Login to continue');</script>");
            ViewData["tab"] = "temporary";
            return View();
        }

        private int convert(string end)
        {
            if (end == "6:00 AM" || end == "6") return 0;
            else if (end == "6:30 AM" || end == "6:30") return 1;
            else if (end == "7:00 AM" || end == "7") return 2;
            else if (end == "7:30 AM" || end == "7:30") return 3;
            else if (end == "8:00 AM" || end == "8") return 4;
            else if (end == "8:30 AM" || end == "8:30") return 5;
            else if (end == "9:00 AM" || end == "9") return 6;
            else if (end == "9:30 AM" || end == "9:30") return 7;
            else if (end == "10:00 AM" || end == "10") return 8;
            else if (end == "10:30 AM" || end == "10:30") return 9;
            else if (end == "11:00 AM" || end == "11") return 10;
            else if (end == "11:30 AM" || end == "11:30") return 11;
            else if (end == "12:00 PM" || end == "12") return 12;
            else if (end == "12:30 PM" || end == "12:30") return 13;
            else if (end == "1:00 PM" || end == "1") return 14;
            else if (end == "1:30 PM" || end == "1:30") return 15;
            else if (end == "2:00 PM" || end == "2") return 16;
            else if (end == "2:30 PM" || end == "2:30") return 17;
            else if (end == "3:00 PM" || end == "3") return 18;
            else if (end == "3:30 PM" || end == "3:30") return 19;
            else if (end == "4:00 PM" || end == "4") return 20;
            else if (end == "4:30 PM" || end == "4:30") return 21;
            else if (end == "5:00 PM" || end == "5") return 22;
            else if (end == "5:30 PM" || end == "5:30") return 23;
            else return 24;
        }

        private int findType(string s)
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
            double time = (end - start) / 2.0;
            Session["SearchTimeForWorker"] = time;
            Session["startTime"] = startTime; Session["endTime"] = endTime;
            Session["start"] = start; Session["end"] = end;

            string status = "";
            for (int i1 = start; i1 <= end; i1++) status += "0";

            int thana = Int32.Parse(Session["thanaID"].ToString());


            // sql for count of workers
            //string sql = "SELECT count(WorkerId) from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "';";
            string sql = "SELECT count(WorkerId) from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "' and SUBSTRING(Area, " + (thana + 1) + ", 1) = '1' ;";
            //string sql = "SELECT count(WorkerId),WorkerId,Name,rating,experience,image from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "' and SUBSTRING(Area, " + (thana + 1) + ", 1) = '1' ORDER BY " + sortby + ";";
            //string sql = "SELECT count(WorkerId) from Worker where (SELECT SUBSTRING(type, 0, 1) from Worker) = '1' and (SELECT SUBSTRING(status, 0, 4) from Worker) = '0000';";
            //select insert(str, 3, 1, '*')
            //select SUBSTRING(@meme,2,1)


            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();

            /*System.Diagnostics.Debug.WriteLine("----------------SEARCHING(time span, start, end)------------------------------------");
            System.Diagnostics.Debug.WriteLine(Session["SearchTimeForWorker"]);
            System.Diagnostics.Debug.WriteLine(start);
            System.Diagnostics.Debug.WriteLine(end);
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------");*/

            string[,] data = new string[count, 5];
            //sql for data of workers
            //sql = "SELECT WorkerId,Name,rating,experience,image from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "';";
            sql = "SELECT WorkerId,Name,rating,experience,image from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(status," + (start + 1) + "," + (end - start + 1) + ") = '" + status + "' and SUBSTRING(Area, " + (thana + 1) + ", 1) = '1' ORDER BY " + sortby + ";";

            table = db.getData(sql);
            int i = 0;
            while (table.Read())
            {
                /*System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
                System.Diagnostics.Debug.WriteLine(i);
                System.Diagnostics.Debug.WriteLine(table.GetString(0));
                System.Diagnostics.Debug.WriteLine(table.GetString(1));
                System.Diagnostics.Debug.WriteLine(table.GetString(2));
                System.Diagnostics.Debug.WriteLine(table.GetString(3));
                System.Diagnostics.Debug.WriteLine(table[4]);
                System.Diagnostics.Debug.WriteLine("----------------------------------------------------");*/
                data[i, 0] = table.GetString(0);
                data[i, 1] = table.GetString(1);
                data[i, 2] = table.GetString(2);
                data[i, 3] = table.GetString(3);
                

                if(table[4] != DBNull.Value) data[i, 4] = table.GetString(4);
                else data[i, 4] = "defaultmaid.png";
                //Array.Copy(photo[i], Encoding.ASCII.GetBytes(table.GetString(4)), 1294967295);
                i++;
            }
            table.Close();

            Session["WData"] = data;
            Session["tab"] = "temporary";

            Session["Wcnt_row"] = count;

            return View("~/Views/Service/Service.cshtml");
        }



        [HttpGet]
        public ActionResult SearchingFull()
        {
            var t = Request["type"].ToString();
            var sortby = Request["sortby"].ToString();
            int type = findType(t);
            int thana = Int32.Parse(Session["thanaID"].ToString());

            string sql = "SELECT count(WorkerId) from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(Area, " + (thana + 1) + ", 1) = '1' ;";


            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();


            System.Diagnostics.Debug.WriteLine("---------------SEARCHING FULL-------------------------------------");
            System.Diagnostics.Debug.WriteLine(count);
            System.Diagnostics.Debug.WriteLine("----------------------------------------------------");




            sql = "SELECT WorkerId,Name,rating,experience,image from Worker where SUBSTRING(type, " + (type + 1) + ", 1) = '1' and SUBSTRING(Area, " + (thana + 1) + ", 1) = '1' ORDER BY " + sortby + ";";

            if (t.Equals("permanent"))
            {
                string[,] Pdata = new string[count, 4];
                byte[][] Pphoto = new byte[count][];


                table = db.getData(sql);
                int i = 0;
                while (table.Read())
                {
                    Pdata[i, 0] = table.GetString(0);
                    Pdata[i, 1] = table.GetString(1);
                    Pdata[i, 2] = table.GetString(2);
                    Pdata[i, 3] = table.GetString(3);
                    //Pphoto[i] = Encoding.ASCII.GetBytes(table.GetString(4));
                    i++;
                }
                table.Close();
                Session["PworkerData"] = Pdata;
                Session["PworkerPhoto"] = Pphoto;

                Session["Pcnt_row"] = count;
                Session["tab"] = "permanent";
            }
            else if (t.Equals("babycare"))
            {
                string[,] Bdata = new string[count, 4];
                byte[][] Bphoto = new byte[count][];


                table = db.getData(sql);
                int i = 0;
                while (table.Read())
                {
                    Bdata[i, 0] = table.GetString(0);
                    Bdata[i, 1] = table.GetString(1);
                    Bdata[i, 2] = table.GetString(2);
                    Bdata[i, 3] = table.GetString(3);
                    //Pphoto[i] = Encoding.ASCII.GetBytes(table.GetString(4));
                    i++;
                }
                table.Close();
                Session["BworkerData"] = Bdata;
                Session["BworkerPhoto"] = Bphoto;

                Session["Bcnt_row"] = count;
                Session["tab"] = "babycare";
            }
            else
            {
                string[,] Edata = new string[count, 4];
                byte[][] Ephoto = new byte[count][];


                table = db.getData(sql);
                int i = 0;
                while (table.Read())
                {
                    Edata[i, 0] = table.GetString(0);
                    Edata[i, 1] = table.GetString(1);
                    Edata[i, 2] = table.GetString(2);
                    Edata[i, 3] = table.GetString(3);
                    //Pphoto[i] = Encoding.ASCII.GetBytes(table.GetString(4));
                    i++;
                }
                table.Close();
                Session["EworkerData"] = Edata;
                Session["EworkerPhoto"] = Ephoto;

                Session["Ecnt_row"] = count;
                Session["tab"] = "elderlycare";
            }
            return View("~/Views/Service/Service.cshtml");
        }





        /*[HttpGet]
        public ActionResult ViewProfile()
        {
            var id = Request["maid"].ToString();
            //System.Diagnostics.Debug.WriteLine("----------------------------------------------------");
            //System.Diagnostics.Debug.WriteLine(id);

            string sql = "SELECT Name,type,rating,experience,image from Worker where WorkerId = " + id;

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            string[] data = new string[5];
            data[0] = table.GetString(0);
            data[1] = table.GetString(1);
            data[2] = table.GetString(2);
            data[3] = table.GetString(3);
            data[4] = id;
            table.Close();

            Session["CurWorker"] = data;

            return View("~/Views/Maid/MaidProfile.cshtml");
        }*/


    }
}