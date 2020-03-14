using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace DataAccessLayer
{
    public static class Data
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

      
        public static List<OrderData> Order(int CustomerId)
        {
            
            List<OrderData> OrderDataList = new List<OrderData>();
            ServicesContext ServiceObj = new ServicesContext();
            var Result = from a in ServiceObj.Orders
                      where a.FromFK==CustomerId
                      select a;
            foreach(var rec in Result)
            {
                OrderData OrderObj = new OrderData();
                OrderObj.id = rec.Id;
                OrderObj.fromid = rec.FromFK;
                OrderObj.toid = rec.ToFK;
                OrderObj.ordereddate = rec.Date;
                OrderObj.scheduleddate = rec.ScheduleDate;
                OrderObj.status = rec.Status;
                OrderDataList.Add(OrderObj);

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
        public static List<ServiceProvider> SPList(string service)
        {
            ServicesContext SerObj = new ServicesContext();
            List<ServiceProvider> SPList = new List<ServiceProvider>();
            var SPResult = from a in SerObj.Users
                      where a.Service == service
                      select a;
            
            foreach(var rec in SPResult)
            {
                ServiceProvider ServiceObj = new ServiceProvider();
                ServiceObj.ServiceProviderId = rec.Id;
                ServiceObj.ServiceProviderName = rec.Username;
                ServiceObj.Rating = Data.average(rec.Id);
                //s.CustomerId = (LoginUserInfo)Session["UserData"];
                SPList.Add(ServiceObj);
               
            }
            return SPList;
        }
        
        public static decimal? average(int id)
        {
            ServicesContext service = new ServicesContext();
            var avg = service.Reviews.Where(i => i.Orders.UserTo.Id == id).Select(i => i.Stars).Average();
            decimal? average = avg;
            return average;
        }
    

      public static UserInfoModel Addlogin(Login Model)
        {
            ServicesContext i = new ServicesContext();
            var LoggedBuffer = (from a in i.Users where a.Username == Model.UserName &&
                       a.Password == Model.Password select a).FirstOrDefault();
            UserInfoModel UserModel = new UserInfoModel();
            if (LoggedBuffer != null)
            {
                UserModel = new UserInfoModel()
                {
                    Id = LoggedBuffer.Id,
                    UserName = LoggedBuffer.Username,
                    Type = LoggedBuffer.Type,
                    Contact = LoggedBuffer.Contact,
                    Location = LoggedBuffer.Location,
                    Service = LoggedBuffer.Service,
                    BankFk = LoggedBuffer.BankFK
                };
            }
            return UserModel;

        }
       


        public static void AddUser(CustomerRegisterClass dad)
        {
            var s = new ServicesContext();
            BankAccountDetails b = new BankAccountDetails();
            b.BankName = dad.BankName;
            b.AccountNumber = dad.BankAccNumber;
            b.Balance = 1000.00m;
            s.BankAccounts.Add(b);
            //s.SaveChanges();
            //var BankId = s.Accounts.Where(g => g.AccountNumber == m.BankAccNumber).Select(g => g.Id).FirstOrDefault();
            User a= new User();
            
            a.Username = dad.Username;
            a.Password = dad.Password;
          
            a.Contact = dad.Contact;
            a.Location = dad.Location;
           
            a.BankFK =b.Id;
            a.Type = "CUSTOMER";
               
            s.Users.Add(a);
            s.SaveChanges();
           
        }
        public static void registeruser(SPRegisterClass mom)
        {
            var k = new ServicesContext();
            BankAccountDetails c = new BankAccountDetails();
            c.BankName = mom.BankName;
            c.AccountNumber = mom.BankAccNumber;
            c.Balance = 1000.00m;
            k.BankAccounts.Add(c);
            //s.SaveChanges();
            //var BankId = s.Accounts.Where(g => g.AccountNumber == m.BankAccNumber).Select(g => g.Id).FirstOrDefault();
            User d = new User();

            d.Username = mom.Username;
            d.Password = mom.Password;
            d.Service = mom.Service;
         
            d.Contact = mom.Contact;
            
            d.BankFK = c.Id;
            d.Type = "SERVICE PROVIDER";

            k.Users.Add(d);
            k.SaveChanges();



        }

        public static List<Profile> Profile()
        {
            List<Profile> UserDataList = new List<Profile>();
            ServicesContext Profile = new ServicesContext();
            var Result = from a in Profile.Users
                         select a;
              foreach(var b in Result)
            {
                Profile pro = new Profile();
                pro.Id = b.Id;
                pro.UserName = b.Username;
                pro.Passsword = b.Password;
                pro.Type = b.Type;
                pro.Contact = b.Contact;
                pro.Location = b.Location;
                pro.Service = b.Service;
                pro.BankFk = b.BankFK;
                UserDataList.Add(pro);
            }
            return UserDataList;
        }  
 
        public static void PlaceOrder(int ServiceProviderId,DateTime ScheduleDate)
        {
            ServicesContext ContextObj = new ServicesContext();
            Order orderObj = new Order() {Date=DateTime.Now,ScheduleDate=ScheduleDate, FromFK=1, Status="Active", ToFK=ServiceProviderId};
            ContextObj.Orders.Add(orderObj);
            ContextObj.SaveChanges();
            
        }
        public static void PlaceOrder(ServiceProvider NewOrder)
        {
            ServicesContext ContextObj = new ServicesContext();
            Order orderObj = new Order() { Date = DateTime.Now, ScheduleDate = NewOrder.ScheduledDate, FromFK = NewOrder.CustomerId, Status = "Active", ToFK = NewOrder.ServiceProviderId };
            ContextObj.Orders.Add(orderObj);
            ContextObj.SaveChanges();

        }
    }    
}

