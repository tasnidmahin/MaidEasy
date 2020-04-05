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
        public ActionResult AddUser(SignupModel signupModel)
        {
            var p = Request["signUpPassword"];
            var cp = Request["confirmPassword"];
            //System.Diagnostics.Debug.WriteLine(p);
            //System.Diagnostics.Debug.WriteLine(cp);
            // Check Password and ConfirmPassword are match or not
            if (p != cp)
            {
                TempData["message"] = "Password not match";
                return RedirectToAction("SignUp", "Register");
            }

            ViewData["phoneNumber"] = Request["Phone"]; ;
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
    }
}