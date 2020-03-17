using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace DataAccessLayer.Models
{
    public class CustomerRegisterClass
    {

        

        [MinLength(4, ErrorMessage = "User Name Minimum Length is 4")]
        [MaxLength(16, ErrorMessage = "User Name Maximum Length is 16")]
        [Required(ErrorMessage = "UserName is Required")]
        [RegularExpression(@"^[a-zA-Z]{1,16}[0-9]*$", ErrorMessage = "UserName Should Be AlphaNumeric.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Emailid { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage = "Contact is Required")]
        [MinLength(10, ErrorMessage = "Contact Length is 10")]
        [MaxLength(10, ErrorMessage = "Contact Length is 10")]
        public string Contact { get; set; }


        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "BankName is Required")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Bank Account Number is Required")]
        [MinLength(9, ErrorMessage = "Bank Account Number Minimum Length is 9")]
        [MaxLength(18, ErrorMessage = "Bank Account Number Maximum Length is 18")]
        public string BankAccNumber { get; set; }





    }

}

    



