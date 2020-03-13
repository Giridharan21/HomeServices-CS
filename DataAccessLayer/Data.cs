using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class Data
    {
      
        public static List<OrderData> Order(int CustomerId)
        {
            
            List<OrderData> OrderDataList = new List<OrderData>();
            ServicesContext ServiceObj = new ServicesContext();
            var Result = from a in ServiceObj.Orders
                      where a.FromFK==CustomerId
                      select a;
            foreach(var rec in Result)
            {
                OrderData o = new OrderData();
                o.id = rec.Id;
                o.fromid = rec.FromFK;
                o.toid = rec.ToFK;
                o.ordereddate = rec.Date;
                o.scheduleddate = rec.ScheduleDate;
                o.status = rec.Status;
                OrderDataList.Add(o);

            }
            return OrderDataList;
        }
        public static List<string> ServiceList()
        {
            ServicesContext SerObj = new ServicesContext();
            List<string> TypeService = new List<string>();
            var result = SerObj.Users.Select(i => i.Service).Distinct();
            foreach (var rec in result)
            {
                TypeService.Add(rec);
            }

            return TypeService;
        }
        public static List<ServiceProviderList> SPList(string service)
        {
            ServicesContext SerObj = new ServicesContext();
            List<ServiceProviderList> SPList = new List<ServiceProviderList>();
            var SPResult = from a in SerObj.Users
                      where a.Service == service
                      select a;
            
            foreach(var rec in SPResult)
            {
                ServiceProviderList s = new ServiceProviderList();
                s.ServiceProviderId = rec.Id;
                s.ServiceProviderName = rec.Username;
                s.rating = Data.average(rec.Id);
                SPList.Add(s);
               
            }
            return SPList;
        }
        
        public static double average(int id)
        {
            ServicesContext service = new ServicesContext();
            var avg = service.Reviews.Where(i => i.Orders.UserTo.Id == id).Select(i => i.Stars).Average();
            double average = double.Parse(avg.ToString());
            return average;
        }
    }
}