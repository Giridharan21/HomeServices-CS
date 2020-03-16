using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace DataAccessLayer.Models
{
  public  class SPRegisterClass
    {

        [MinLength(4,ErrorMessage ="Minimum Length is 4")]
        [MaxLength(16, ErrorMessage = "Maximum Length is 16")]
        [Required(ErrorMessage ="User Name is Required")]
        [RegularExpression(@"^[a-zA-Z]{1,16}[0-9]+$", ErrorMessage = "UserName Should Be AlphaNumeric.")]
        public string Username { get; set; }




       [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set;}

        [Required(ErrorMessage = "Email  is Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Emailid { get; set; }
        
        public string Type { get; set; }

        [Required(ErrorMessage = "Contact Number is Required")]
        [MinLength(10, ErrorMessage = "Minimum Length is 10")]
        [MaxLength(10, ErrorMessage = "Maximum Length is 10")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Service is Required")]
        public string Service { get; set; }



        [Required(ErrorMessage = "Bank name is Required")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Bank Account Number is Required")]
        [MinLength(12, ErrorMessage = "Minimum Length is 12")]
        [MaxLength(12, ErrorMessage = "Maximum Length is 12")]
        public string BankAccNumber { get; set; }

       
        public List<string> ServiceList { get;set; }

    }




}

