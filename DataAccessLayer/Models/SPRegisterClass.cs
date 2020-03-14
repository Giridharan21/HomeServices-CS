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
        [Required(ErrorMessage ="It is Required")]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "UserName Should Be AlphaNumeric.")]

        //  [Range(4, 8)]

        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set;}
        [Required]
        public string Emailid { get; set; }
        
        public string Type { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]

        public string Service { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BankAccNumber { get; set; }

       
     

    }




}

