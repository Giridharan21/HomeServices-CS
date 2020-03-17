using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        public string Service { get; set; }
        public int BankFk { get; set; }
        public decimal Charge { get; set; }
    }
}
