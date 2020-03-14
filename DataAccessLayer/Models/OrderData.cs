using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class OrderData
    {
        public int id { get; set; }
        public int fromid { get; set; }
        public int toid { get; set; }
        public string status { get; set; }
        public DateTime ordereddate { get; set; }
        public DateTime scheduleddate { get; set; }
    }
}
