using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
using System.Data.Linq;
namespace DataAccessLayer
{
    public static class Data
    {
        public static List<ListOrder> ServiceProvider(int Id)
        {
            List<ListOrder> listOrders = new List<ListOrder>();
            ServicesContext db = new ServicesContext();
            var SPId = db.ServiceProviders.Where(g => g.UserId == Id).Select(g => g.Id).FirstOrDefault();
            var res = from a in db.Orders
                      where SPId==a.ToFK
                      select a;

            foreach (var b in res)
            {

                ListOrder OrderObj = new ListOrder {
                    Id = b.Id,
                    FromFK = b.FromFK,
                    Status = b.Status,
                    Date = b.Date,
                    ScheduleDate = b.ScheduleDate
                };
                listOrders.Add(OrderObj);
            }
            return listOrders;

        }
        public static void ChangeStatus(int orderId, string status)
        {
            ServicesContext db = new ServicesContext();
            var res = (from a in db.Orders
                       where a.Id == orderId
                       select a).FirstOrDefault();

            res.Status = status;
            db.SaveChanges();
        }
        public static List<ListOrder> PreviousOrder(int orderId)
        {
            List<ListOrder> a = new List<ListOrder>();
            ServicesContext db = new ServicesContext();
            var res = from d in db.Orders
                      where d.Id == orderId
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
                OrderObj.price=i.FinalPrice
                OrderDataList.Add(OrderObj);
            }
            return OrderDataList;
        }
        public static List<string> ServiceList()
        {
            ServicesContext SerObj = new ServicesContext();
            List<string> ServiceTypes = new List<string>();
            var result = SerObj.Services.Select(i => i.Service);
            foreach (var i in result)
            {
                ServiceTypes.Add(i);
            }

            return ServiceTypes;
        }
        public static List<ServiceProviderModel> SPList(string service,int Id)
        {
            ServicesContext SerObj = new ServicesContext();
            List<ServiceProviderModel> SPList = new List<ServiceProviderModel>();
            //var SPResult = from a in SerObj.Users where a.Service == service select a;
            //Change
            var ServiceProviders = SerObj.ServicesAssigned.Include("Service").Include("SP")
                .Join(SerObj.ServiceProviders,Assigned=>Assigned.ServiceProviderFK,SP=>SP.Id,(Assigned,SP)=>new {Assigned.Charge,SP.UserId,SP.Contact,Assigned.Service,Assigned.ServiceProviderFK })
                .Join(SerObj.Users, g => g.UserId, u => u.Id, (g, u) => new {g.Charge,g.Service,g.Contact,u.Username,g.ServiceProviderFK})
                .Where(g => g.Service.Service == service).Select(i => i);

            foreach (var i in ServiceProviders)
            {
                var ServiceObj = new ServiceProviderModel();
                ServiceObj.ServiceProviderId = i.ServiceProviderFK;
                
                ServiceObj.ServiceProviderName = i.Username;
                ServiceObj.Rating = Data.Average(i.ServiceProviderFK);
                ServiceObj.CustomerId = Id;
                ServiceObj.Price = i.Charge;
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
            service.Charge = NewSP.Charge;
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
        //public static Profile EditProfile() { return new Profile(); }
        public static void EditProfile(CustomerProfile User,int UserId)
        {
            ServicesContext contextObj = new ServicesContext();
            var UserObj = contextObj.Users.Where(g => g.Id == UserId).Select(g => g).FirstOrDefault();
            var CustomerObj = contextObj.Customers.Where(g => g.UserId == UserId).Select(g => g).FirstOrDefault();
            UserObj.Username = User.UserName;
            CustomerObj.Contact = User.Contact;
            CustomerObj.Location = User.Location;
            CustomerObj.BankFK = User.BankFk;
            contextObj.Users.Add(UserObj);
            contextObj.Customers.Add(CustomerObj);
            contextObj.SaveChanges();
            
            
        }
        public static void EditProfile(ServiceProviderProfile User, int UserId)
        {
            ServicesContext contextObj = new ServicesContext();
            var UserObj = contextObj.Users.Where(g => g.Id == UserId).Select(g => g).FirstOrDefault();
            var ServiceProviderObj = contextObj.ServiceProviders.Where(g => g.UserId == UserId).Select(g => g).FirstOrDefault();
            UserObj.Username = User.UserName;
            ServiceProviderObj.Contact = User.Contact;
            ServiceProviderObj.BankFK = User.BankFk;
            contextObj.Users.Add(UserObj);
            contextObj.ServiceProviders.Add(ServiceProviderObj);
            contextObj.SaveChanges();

        }

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
            Order orderObj = new Order() { Date = DateTime.Now,FinalPrice=NewOrder.Price, ScheduleDate = NewOrder.ScheduledDate, FromFK = NewOrder.CustomerId, Status = "Active", ToFK = NewOrder.ServiceProviderId };

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

        public static void RemoveUsers(int id)
        {
            ServicesContext obj = new ServicesContext();
            var res = (from a in obj.Users
                      where id == a.Id
                      select a).FirstOrDefault();
            obj.Users.Remove(res);
        }    
        //Admin Functions
        public static void AddService(string ServiceString) {
            ServicesContext context = new ServicesContext();
            Services NewService = new Services() { Service=ServiceString};
            context.Services.Add(NewService);
            context.SaveChanges();
        }

        public static int RemoveService(string ServiceString) {
            ServicesContext context = new ServicesContext();
            var SelectedRow = context.Services.Where(g => g.Service == ServiceString).Select(g => g).FirstOrDefault();
            if (SelectedRow is null)
                return 0;
            context.Services.Remove(SelectedRow);
            context.SaveChanges();
            return 1;
        }
        public static CustomerProfile CustomerData(int id)
        {
            ServicesContext sobj = new ServicesContext();
            CustomerProfile cobj = new CustomerProfile();
            string username = (from a in sobj.Users
                          where a.Id == id
                          select a.Username).FirstOrDefault();
            var Details = (from a in sobj.Customers
                          where a.UserId == id
                          select a).FirstOrDefault();
            cobj.BankFk = Details.BankFK;
            cobj.Contact = Details.Contact;
            cobj.Location = Details.Location;
            cobj.UserName = username;                
            return cobj;
        }
        public static ServiceProviderProfile SPData(int id)
        {
            ServicesContext serobj = new ServicesContext();
            ServiceProviderProfile sobj = new ServiceProviderProfile();
            string username = (from a in serobj.Users
                               where a.Id == id
                               select a.Username).FirstOrDefault();
            var Details = (from a in serobj.ServiceProviders
                           where a.UserId == id
                           select a).FirstOrDefault();
            sobj.BankFk = Details.BankFK;
            sobj.Contact = Details.Contact;
            sobj.UserName = username;
            return sobj;
           
        }
    }    
}

