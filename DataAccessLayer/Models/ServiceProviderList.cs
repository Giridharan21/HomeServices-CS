using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ServiceProvider
    {
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public decimal? Rating { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int CustomerId { get; set; }
    }
}
