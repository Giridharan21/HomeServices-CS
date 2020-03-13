using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ServiceProviderList
    {
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public double rating { get; set; }
        public DateTime scheduledDate { get; set; }
    }
}
