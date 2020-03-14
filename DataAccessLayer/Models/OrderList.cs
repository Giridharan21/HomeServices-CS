using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    class OrderList
    {
        public int Id { get; set; }
        public int FromFK { get; set; }
        public int ToFK { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
}
