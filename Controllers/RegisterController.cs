using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MaidEasy.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            if (TempData["message"] != null) //It will true when Password and ConfirmPassword not match
                ViewBag.Error = TempData["message"].ToString();
            return View();
        }

        public ActionResult LogIn()
        {
            if (TempData["message"] != null) //It will true when Password not match with DB password or wrong usrname
                ViewBag.Error = TempData["message"].ToString();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("username");
            Session.Remove("thanaID");
            Session.Remove("SearchTimeForWorker");
            Session.Remove("CurWorker");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VerificationCode()
        {
            return View();
        }

        private string getThanaString(string thana)
        {
            DBHelper db = DBHelper.getDB();
            string sql = "SELECT ThanaId from Thana where Name = '" + thana + "'";
            var table = db.getData(sql);
            table.Read();
            int t = Int32.Parse(table.GetString(0));
            StringBuilder r = new StringBuilder("00000000000000000000000000000000000000000000000000");

            if(t-1 >= 0 && t-1<r.Length) r[t - 1] = '1';

            string ret = r.ToString();
            table.Close();


            Session["thanaID"] = t - 1;

            return ret;
        }

        private int getThanaID(string thana)
        {
            int t = 0;
            int len = thana.Length;
            for(int i=0;i<len;i++){
                if(thana[i] == '1'){
                    t = i;  break;
                }
            }
            return t;
        }

        [HttpPost]
        public ActionResult CheckInfo(SignupModel signupModel)
        {
            //get info from form in signup html
            var user = Request["Username"];
            var name = Request["Name"];
            var phone = Request["Phone"];
            var presentAddress = Request["presentAddress"];
            var permanentAddress = Request["permanentAddress"];
            var pass = Request["signUpPassword"];
            var conpass = Request["confirmPassword"];
            var thana = Request["thana"];
            string thanastring = getThanaString(thana);

            TempData["username"] = user;
            TempData["name"] = name;
            TempData["phone"] = phone;
            TempData["presentAddress"] = presentAddress;
            TempData["permanentAddress"] = permanentAddress;
            TempData["pass"] = pass;
            TempData["conpass"] = conpass;
            TempData["thana"] = thana;
            TempData["thanastring"] = thanastring;



            DBHelper db = DBHelper.getDB();
            string sql = "SELECT count(UserId) from users where username = '" + user + "'" ;

            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            table.Close();
            if (count > 0)
            {
                TempData["message"] = "Username allready exits";
                return RedirectToAction("SignUp", "Register");
            }

            /*sql = "SELECT count(UserId) from users where mobile = '" + phone + "'";
            table = db.getData(sql);
            table.Read();
            count = Int32.Parse(table.GetString(0));
            if (count > 0)
            {
                TempData["message"] = "Phone No allready used";
                return RedirectToAction("SignUp", "Register");
            }*/
            pass = hash(pass);
            TempData["pass"] = pass;
            ViewData["phoneNumber"] = phone;

            return View("~/Views/Register/VerificationCode.cshtml");
        }

        private string hash(string password)
        {
            // Create the salt value with a cryptographic PRNG:
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        private bool checkPassword(string savedPasswordHash, string password)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i]) return false;
            
            return true;
        }

        [HttpGet]
        public ActionResult AddUser()
        {

            DBHelper db = DBHelper.getDB();
            //string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress ) VALUES('" + TempData["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + " ');" ;
            string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress , thana ) VALUES('" + TempData["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + "', '" + TempData["thanastring"] + " ');" ;
            db.setData(sql);


            //System.Diagnostics.Debug.WriteLine("--------------------");
            //System.Diagnostics.Debug.WriteLine(ViewData["phoneNumber"]);
            //System.Diagnostics.Debug.WriteLine(TempData["username"]);
            

            Session["username"] = TempData["username"];

            TempData["username"] = null;

            return RedirectToAction("Index", "Home");
        }


        public ActionResult VerifyUser()
        {
            var user = Request["Username"];
            var pass = Request["loginpassword"];

            TempData["user"] = user;
            TempData["pass"] = pass;

            string sql = "SELECT count(UserId),password,thana from Users where username = '" + user + "'";

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            if(count == 0)
            {
                TempData["message"] = "Username doesn't exist";
                table.Close();
                return RedirectToAction("LogIn", "Register");
            }

            var p = table.GetString(1);

            if(!checkPassword(p, pass))
            {
                TempData["message"] = "Password didn't match";
                table.Close();
                return RedirectToAction("LogIn", "Register");
            }
            var thanaS = table.GetString(2);
            var thana = getThanaID(thanaS);
            table.Close();

            Session["thanaID"] = thana;
            Session["username"] = user;
            TempData["user"] = null;
            TempData["pass"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}