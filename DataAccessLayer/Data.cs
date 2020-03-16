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
        //Jayaseelan
        public static List<ListOrder> ServiceProvider()
        {
            List<ListOrder> l = new List<ListOrder>();
            ServicesContext db = new ServicesContext();
            var res = from a in db.Orders select a;
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
            foreach(var i in Result)
            {
                OrderData OrderObj = new OrderData();
                OrderObj.id = i.Id;
                OrderObj.fromid = i.FromFK;
                OrderObj.toid = i.ToFK;
                OrderObj.ordereddate = i.Date;
                OrderObj.scheduleddate = i.ScheduleDate;
                OrderObj.status = i.Status;
                OrderDataList.Add(OrderObj);

            }
            return OrderDataList;
        }
        public static List<string> ServiceList()
        {
            ServicesContext SerObj = new ServicesContext();
            List<string> TypeService = new List<string>();
            var result = SerObj.Services.Select(i => i.Service);
            foreach (var i in result)
            {
                TypeService.Add(i);
            }

            return TypeService;
        }
        public static List<ServiceProviderModel> SPList(string service,int Id)
        {
            ServicesContext SerObj = new ServicesContext();
            List<ServiceProviderModel> SPList = new List<ServiceProviderModel>();
            //var SPResult = from a in SerObj.Users where a.Service == service select a;
            //Change
            var ServiceProviders = SerObj.ServicesAssigned.Include("Service").Include("SP")
                .Join(SerObj.ServiceProviders,Assigned=>Assigned.ServiceProviderFK,SP=>SP.Id,(Assigned,SP)=>new {SP.UserId,SP.Contact,Assigned.Service,Assigned.ServiceProviderFK })
                .Join(SerObj.Users, g => g.UserId, u => u.Id, (g, u) => new {g.Service,g.Contact,u.Username,g.ServiceProviderFK })
                .Where(g => g.Service.Service == service).Select(i => i);

            foreach (var i in ServiceProviders)
            {
                var ServiceObj = new ServiceProviderModel();
                ServiceObj.ServiceProviderId = i.ServiceProviderFK;
                
                ServiceObj.ServiceProviderName = i.Username;
                ServiceObj.Rating = Data.Average(i.ServiceProviderFK);
                ServiceObj.CustomerId = Id;
                SPList.Add(ServiceObj);
               
            }
            return SPList;
        }
        //a to Average
        public static decimal? Average(int id)
        {
            using (ServicesContext service = new ServicesContext())
            { 
                var avg = service.Reviews.Where(i => i.Orders.UserTo.Id == id).Select(i => i.Stars).Average();
                decimal? average = avg;
                return average;
            }
        }
    

      public static UserInfoModel Addlogin(Login Model)
        {
            using (ServicesContext i = new ServicesContext())
            {
                var LoggedBuffer = (from a in i.Users where a.Username == Model.UserName && a.Password == Model.Password select a)
                                    .FirstOrDefault();
                //Change 
                UserInfoModel UserModel = new UserInfoModel();
                if ((LoggedBuffer != null) && LoggedBuffer.Type.ToUpperInvariant() == "CUSTOMER")
                {
                    var LoggedInUserDetails = i.Customers.Where(g => g.UserId == LoggedBuffer.Id).Select(g => g).FirstOrDefault();
                    UserModel.Id = LoggedBuffer.Id;
                    UserModel.UserName = LoggedBuffer.Username;
                    UserModel.Type = LoggedBuffer.Type;
                    UserModel.Contact = LoggedInUserDetails.Contact;
                    UserModel.Location = LoggedInUserDetails.Location;
                    UserModel.BankFk = LoggedInUserDetails.BankFK;
                }
                else if ((LoggedBuffer != null) && LoggedBuffer.Type.ToUpperInvariant() == "SERVICE PROVIDER")
                {
                    var LoggedInUserDetails = i.ServiceProviders.Where(g => g.UserId == LoggedBuffer.Id).Select(g => g).FirstOrDefault();
                    UserModel.Id = LoggedBuffer.Id;
                    UserModel.UserName = LoggedBuffer.Username;
                    UserModel.Type = LoggedBuffer.Type;
                    UserModel.Contact = LoggedInUserDetails.Contact;
                    UserModel.BankFk = LoggedInUserDetails.BankFK;
                }

                return UserModel;
            }
        }
       


        public static void AddUser(CustomerRegisterClass NewCustomer)
        {
            var s = new ServicesContext();

            BankAccountDetails bank = new BankAccountDetails();
            bank.BankName = NewCustomer.BankName;
            bank.AccountNumber = NewCustomer.BankAccNumber;
            bank.Balance = 1000.00m;
            s.BankAccounts.Add(bank);
            
            User UserObj= new User();
            UserObj.Username = NewCustomer.Username;
            UserObj.Password = NewCustomer.Password;
            UserObj.Type = "CUSTOMER";
            s.Users.Add(UserObj);

            CustomerDetails customer = new CustomerDetails();
            customer.Contact = NewCustomer.Contact;
            customer.UserId= UserObj.Id;
            customer.Location = NewCustomer.Location;
            customer.BankFK =bank.Id;
            s.Customers.Add(customer);

            s.SaveChanges();
           
        }
        public static void registeruser(SPRegisterClass NewSP)
        {
            var k = new ServicesContext();
            BankAccountDetails bank = new BankAccountDetails();
            bank.BankName = NewSP.BankName;
            bank.AccountNumber = NewSP.BankAccNumber;
            bank.Balance = 1000.00m;
            k.BankAccounts.Add(bank);

            User UserObj = new User();
            UserObj.Username = NewSP.Username;
            UserObj.Password = NewSP.Password;
            UserObj.Type = "SERVICE PROVIDER";
            k.Users.Add(UserObj);

            ServiceProviderDetails serviceProvider = new ServiceProviderDetails();
            serviceProvider.UserId = UserObj.Id;
            serviceProvider.Contact = NewSP.Contact;
            serviceProvider.BankFK = bank.Id;
            k.ServiceProviders.Add(serviceProvider);

            var ServiceId = k.Services.Where(g => g.Service == NewSP.Service).Select(g => g.Id).FirstOrDefault();

            ServicesAssigned service = new ServicesAssigned();
            service.ServiceProviderFK = serviceProvider.Id;
            service.ServicesFK = ServiceId;
            k.ServicesAssigned.Add(service);

            k.SaveChanges();



        }
        public static List<string> GetServices()
        {
            List<string> Services = new List<string>();
            ServicesContext context = new ServicesContext();
            var QueryServices = context.Services.Select(g => g.Service);
            foreach (var i in QueryServices)
                Services.Add(i);
            
            return Services;
        }
        public static Profile EditProfile() { return new Profile(); }
        //public static Profile EditProfile(string Type)
        //{
        //    Profile UserDataList = new Profile();
        //    ServicesContext contextObj = new ServicesContext();

        //    if(Type.ToUpperInvariant() == "CUSTOMER")
        //    {
        //        var Result = contextObj.Customers.Include("User").
        //    }
        //    Profile pro = new Profile();
        //    pro.Id = b.Id;
        //    pro.UserName = b.Username;
        //    pro.Passsword = b.Password;
        //    pro.Type = b.Type;
        //    pro.Contact = b.Contact;
        //    pro.Location = b.Location;
        //    pro.Service = b.Service;
        //    pro.BankFk = b.BankFK;
        //    UserDataList.Add(pro);

        //    return UserDataList;
        //}  

        public static void PlaceOrder(int ServiceProviderId,DateTime ScheduleDate)
        {
            ServicesContext ContextObj = new ServicesContext();
            Order orderObj = new Order() {Date=DateTime.Now,ScheduleDate=ScheduleDate, FromFK=1, Status="Active", ToFK=ServiceProviderId};
            ContextObj.Orders.Add(orderObj);
            ContextObj.SaveChanges();
            
        }
        public static void PlaceOrder(ServiceProviderModel NewOrder)
        {
            ServicesContext ContextObj = new ServicesContext();
            Order orderObj = new Order() { Date = DateTime.Now, ScheduleDate = NewOrder.ScheduledDate, FromFK = NewOrder.CustomerId, Status = "Active", ToFK = NewOrder.ServiceProviderId };
            ContextObj.Orders.Add(orderObj);
            ContextObj.SaveChanges();

        }
        public static void AddReview(reviewmodel model)
        {
            ServicesContext a = new ServicesContext();
            Review r = new Review();
            r.Stars = model.Stars;
            r.Comment = model.Comment;
            r.OrderIdFK = 1;
            a.Reviews.Add(r);
            a.SaveChanges();

        }
    }    
}

