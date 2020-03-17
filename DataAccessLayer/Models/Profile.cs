using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
   public class CustomerProfile
    {
        public string UserName { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Location{ get; set; }
        [Required]
        public int BankFk { get; set; }

    }
    public class ServiceProviderProfile
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public int BankFk { get; set; }

    }
}
