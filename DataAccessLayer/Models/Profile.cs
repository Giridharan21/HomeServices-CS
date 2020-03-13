using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
   public class Profile
    {
        [Required]
        public int Id{ get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Passsword { get; set; }
        [Required]
        public string Type{ get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Location{ get; set; }
        [Required]
        public string Service{ get; set; }
        [Required]
        public int BankFk { get; set; }





    }
}
