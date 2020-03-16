using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ServiceProviderModel
    {
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public decimal? Rating { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int CustomerId { get; set; }
    }
}
