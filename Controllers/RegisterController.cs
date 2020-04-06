using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            return View();
        }

        public ActionResult VerificationCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckInfo(SignupModel signupModel)
        {

            var user = Request["Username"];
            var name = Request["Name"];
            var phone = Request["Phone"];
            var presentAddress = Request["presentAddress"];
            var permanentAddress = Request["permanentAddress"];
            var pass = Request["signUpPassword"];
            var conpass = Request["confirmPassword"];

            TempData["username"] = user;
            TempData["name"] = name;
            TempData["phone"] = phone;
            TempData["presentAddress"] = presentAddress;
            TempData["permanentAddress"] = permanentAddress;
            TempData["pass"] = pass;
            TempData["conpass"] = conpass;


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

        [HttpPost]
        public ActionResult AddUser()
        {
            DBHelper db = DBHelper.getDB();
            string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress ) VALUES('" + TempData["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + " ');" ;
            db.setData(sql);


            /*System.Diagnostics.Debug.WriteLine("--------------------");
            System.Diagnostics.Debug.WriteLine(ViewData["phoneNumber"]);
            System.Diagnostics.Debug.WriteLine(TempData["username"]);
            */

            Session["username"] = TempData["username"];


            return RedirectToAction("Index", "Home");
        }
    }
}