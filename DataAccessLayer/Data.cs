using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class Data
    {
        public static List<ListOrder> ServiceProvider()
        {
            List<ListOrder> l = new List<ListOrder>();
            ServicesContext db = new ServicesContext();
            var res = from a in db.Orders
                      select a;
            foreach (var b in res)
            {

                ListOrder o = new ListOrder();
                o.Id = b.Id;
                o.FromFK = b.FromFK;
                o.Status = b.Status;
                o.Date = b.Date;
                o.ScheduleDate = b.ScheduleDate;
                l.Add(o);
            }
            return l;

        }
        public static void Accept(int orderId, string status)
        {
            ServicesContext db = new ServicesContext();
            var res = (from a in db.Orders
                       where a.Id == orderId
                       select a).FirstOrDefault();

            res.Status = status;
            db.Orders.Add(res);
            db.SaveChanges();


        }
        public static List<ListOrder> PreviousOrder()
        {
            List<ListOrder> a = new List<ListOrder>();
            ServicesContext db = new ServicesContext();
            var res = from d in db.Orders
                      select d;
            foreach (var b in res)
            {

                ListOrder o = new ListOrder();
                if (o.Status == "Completed")
                {
                    o.Id = b.Id;
                    o.FromFK = b.FromFK;
                    o.Status = b.Status;
                    o.Date = b.Date;
                    o.ScheduleDate = b.ScheduleDate;
                    a.Add(o);
                }
            }
            return a;

        }
    }
}