using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaidEasy.Models
{
    public class SignupModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}