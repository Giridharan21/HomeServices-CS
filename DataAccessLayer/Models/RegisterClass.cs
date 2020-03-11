using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
  public  class RegisterClass
    {

        
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Emailid { get; set; }

        public string Type { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        public string Service { get; set; }
        public string BankName { get; set; }

        public string AccountNumber { get; set; }
        



    }
}
