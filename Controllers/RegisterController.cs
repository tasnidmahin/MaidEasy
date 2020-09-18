using MaidEasy.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            Session.Remove("userID");
            Session.Remove("thanaID");
            Session.Remove("SearchTimeForWorker");
            Session.Remove("CurWorker");
            Session.Remove("uType");
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

            System.Diagnostics.Debug.WriteLine("---------Thana String-----------");
            System.Diagnostics.Debug.WriteLine(ret);
            System.Diagnostics.Debug.WriteLine(t);

            Session["thanaID"] = t - 1;

            return ret;
        }

        public int getThanaID(string thana)
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
        public ActionResult CheckInfo(HttpPostedFileBase file)
        {
            //get info from form in signup html
            SignupModel signup = new SignupModel();
            signup.Username         = Request["Username"];
            signup.Name             = Request["Name"];
            signup.PhoneNo          = Request["Phone"];
            signup.PresentAddress   = Request["presentAddress"];
            signup.thana            = Request["thana"];
            signup.thana            = getThanaString(signup.thana);
            signup.PermanentAddress = Request["permanentAddress"];
            signup.Password         = Request["signUpPassword"];
            signup.ConfirmPassword  = Request["confirmPassword"];


            /*var user = Request["Username"];
            var name = Request["Name"];
            var phone = Request["Phone"];
            var presentAddress = Request["presentAddress"];
            var permanentAddress = Request["permanentAddress"];
            var pass = Request["signUpPassword"];
            var conpass = Request["confirmPassword"];
            var thana = Request["thana"];
            string thanastring = getThanaString(thana);*/



            //var filename = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
            //var path = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
            //System.IO.File.Move(fileName, f);
            //file.SaveAs(path);



            Session["img"] = file;

            TempData["username"]            = signup.Username;
            TempData["name"]                = signup.Name;
            TempData["phone"]               = signup.PhoneNo;
            TempData["presentAddress"]      = signup.PresentAddress;
            TempData["permanentAddress"]    = signup.PermanentAddress;
            TempData["pass"]                = signup.Password;
            TempData["conpass"]             = signup.ConfirmPassword;
            TempData["thana"]               = Request["thana"];
            TempData["thanastring"]         = signup.thana;



            DBHelper db = DBHelper.getDB();
            string sql = "SELECT count(UserId) from users where username = '" + signup.Username + "'" ;

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
            signup.Password = hash(signup.Password);
            TempData["pass"] = signup.Password;
            ViewData["phoneNumber"] = signup.PhoneNo;
            ViewData["codeverify"] = 1;

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

        /*private string ReplaceFileName(string f, string name)
        {
            string s = "";
            int l = f.Length,v = 0;
            for (int i = 0; i < l; i++)
            {
                if (f[i].Equals('.')) v = 1;
                if(v == 1)s += f[i];
            }
            System.Diagnostics.Debug.WriteLine("-----------SSSSSSSSSSSSSS---------");
            System.Diagnostics.Debug.WriteLine(s);
            System.Diagnostics.Debug.WriteLine("--------------------");
            System.Diagnostics.Debug.WriteLine("---------NAME-----------");
            System.Diagnostics.Debug.WriteLine(name);
            System.Diagnostics.Debug.WriteLine("--------------------");
            name = name + s;
            System.Diagnostics.Debug.WriteLine("-------NNNNNNNNNNNNNNNNNNNNNNNNN-------------");
            System.Diagnostics.Debug.WriteLine(name);
            System.Diagnostics.Debug.WriteLine("--------------------");
            return name;
        }*/

        [HttpGet]
        public ActionResult AddUser()
        {
            Session["username"] = TempData["username"];
            string image = "default.jpg";
            DBHelper db = DBHelper.getDB();
            //string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress ) VALUES('" + TempData["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + " ');" ;
            //string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress , thana ) VALUES('" + Session["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + "', '" + TempData["thanastring"] + " ');" ;


            //System.Diagnostics.Debug.WriteLine("--------------------");
            //System.Diagnostics.Debug.WriteLine(ViewData["phoneNumber"]);
            //System.Diagnostics.Debug.WriteLine(TempData["username"]);
            
            if(Session["img"] != null)
            {
                HttpPostedFileBase file = (HttpPostedFileBase) Session["img"];
                //var fileName = Path.GetFileName(file.FileName);
                //var fileName = file.FileName.ToString();
                //var f = ReplaceFileName(fileName, Session["username"].ToString());
                //var filename = Path.GetFileNameWithoutExtension(file.FileName.ToString()) + Guid.NewGuid() + Path.GetExtension(file.FileName);
                /*System.Diagnostics.Debug.WriteLine("--------------------");
                System.Diagnostics.Debug.WriteLine(filename);
                System.Diagnostics.Debug.WriteLine("--------------------");
                var path = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
                //System.IO.File.Move(fileName, f);
                file.SaveAs(path);
                image = filename;*/


                //var filename = Path.GetFileNameWithoutExtension(fileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
                //var fullpath = Server.MapPath("~/Content/Users/") + filename;
                //var fullpath = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
                //file.SaveAs(fullpath);
                //System.Diagnostics.Debug.WriteLine((HttpPostedFileBase)Session["img"]).FileName);
                //var filename = Path.GetFileNameWithoutExtension(file.FileName) + Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filename = Session["username"].ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
                //System.IO.File.Move(fileName, f);
                
                file.SaveAs(path);


                image = filename;

                //HttpPostedFileBase file = (HttpPostedFileBase)Session["img"];
                /*string path = Path.Combine(Server.MapPath("~/App_Data/Photo/User/"), Session["username"].ToString()).ToString();
                file.SaveAs(path);*/
            }
            string sql = " INSERT INTO Users (username , password , Name , mobile , PresentAddress , PermanentAddress,image  , thana ) VALUES('" + Session["username"] + "', '" + TempData["pass"] + " ', ' " + TempData["name"] + " ', ' " + TempData["phone"] + " ', ' " + TempData["presentAddress"] + " ', ' " + TempData["permanentAddress"] + "', '" + image + "', '" + TempData["thanastring"] + " ');";
            db.setData(sql);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult VerifyUser()
        {
            LogInModel login = new LogInModel();
            login.Username = Request["Username"];
            login.Password = Request["loginpassword"];


            /*var user = Request["Username"];
            var pass = Request["loginpassword"];*/

            TempData["user"] = login.Username;
            TempData["pass"] = login.Password;

            string sql = "SELECT count(UserId),password,thana,UserId,type from Users where username = '" + login.Username + "'";

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            int count = Int32.Parse(table.GetString(0));
            if(count == 0)
            {
                TempData["message"] = "Username does not exist";
                table.Close();
                return RedirectToAction("LogIn", "Register");
            }

            var p = table.GetString(1);

            if(!checkPassword(p, login.Password))
            {
                TempData["message"] = "Password did not match";
                table.Close();
                return RedirectToAction("LogIn", "Register");
            }
            var thanaS = table.GetString(2);
            var thana = getThanaID(thanaS);
            var uID = table.GetString(3);
            var uType = table.GetString(4);
            table.Close();

            Session["thanaID"] = thana;
            Session["username"] = login.Username;
            Session["userID"] = uID;
            Session["uType"] = uType;
            TempData["user"] = null;
            TempData["pass"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult getEditProfileData(HttpPostedFileBase file)
        {
            var name = Request["name"];
            var phone = Request["Phone"];
            var presentAddress = Request["presentAddress"];
            var permanentAddress = Request["permanentAddress"];
            var Opass = Request["Password"];
            var Npass = Request["newPassword"];
            var conpass = Request["confirmPassword"];
            var thana = Request["thana"];
            var profileimage = Request["profileimage"];
            Session["profileimage"] = file; // name of file field


            TempData["Ename"] = Request["name"];
            TempData["Ephone"] = Request["Phone"];
            TempData["EpresentAddress"] = Request["presentAddress"];
            TempData["EpermanentAddress"] = Request["permanentAddress"];
            TempData["Epass"] = Request["Password"];
            TempData["Enewpass"] = Request["newPassword"];
            TempData["Econpass"] = Request["confirmPassword"];
            TempData["Ethana"] = Request["thana"];
            TempData["profileimage"] = "default.jpg";



            /*if (Session["profileimage"] != null)
            {
                var filename = Session["username"].ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
                file.SaveAs(path);

                TempData["profileimage"] = filename;

            }*/

            string sql = "SELECT password from Users where username = '" + Session["username"] + "'";

            DBHelper db = DBHelper.getDB();
            var table = db.getData(sql);
            table.Read();
            var p = table.GetString(0);
            table.Close();
            if (!checkPassword(p, Opass))
            {
                TempData["message"] = "Old Password did not match";
                return RedirectToAction("Edit_profile", "User");
            }


            ViewData["phoneNumber"] = phone;
            ViewData["codeverify"] = 2;

            return View("~/Views/Register/VerificationCode.cshtml");
        }

        public ActionResult saveEditProfile()
        {
            System.Diagnostics.Debug.WriteLine("--------SAVE   EDIT------------");
            var p = hash(TempData["Enewpass"].ToString());
            var thana = getThanaString(TempData["Ethana"].ToString());

            string sql;
            DBHelper db;

            if (Session["profileimage"] != null)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)Session["profileimage"];
                var filename = Session["username"].ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Users/"), filename);
                file.SaveAs(path);

                TempData["profileimage"] = filename;

                sql = "UPDATE Users SET Name  = '" + TempData["Ename"] + "', password = '" + p + "', mobile = '" + TempData["Ephone"] + "', image = '" + TempData["profileimage"] + "', PresentAddress  = '" + TempData["EpresentAddress"] + "', PermanentAddress = '" + TempData["EpermanentAddress"] + "', thana  = '" + thana + "' where username  = '" + Session["username"] + "'";
                db = DBHelper.getDB();
                db.setData(sql);

            }
            else
            {
                sql = "UPDATE Users SET Name  = '" + TempData["Ename"] + "', password = '" + p + "', mobile = '" + TempData["Ephone"] + "', PresentAddress  = '" + TempData["EpresentAddress"] + "', PermanentAddress = '" + TempData["EpermanentAddress"] + "', thana  = '" + thana + "' where username  = '" + Session["username"] + "'";
                db = DBHelper.getDB();
                db.setData(sql);
            }

            return RedirectToAction("user_profile", "User", new { id = Session["userID"] });
        }

    }
}