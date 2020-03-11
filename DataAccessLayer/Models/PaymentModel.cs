using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PaymentModel
    {
        public string FromBankName { get; set; }
        public string FromAccountNo { get; set; }
        public string ToAccountNo { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }
    }
}
