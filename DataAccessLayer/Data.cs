using System.ComponentModel;
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
                OrderObj.price = i.FinalPrice;
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
            //var ServiceProviders = SerObj.ServicesAssigned.Include("Service").Include("SP")
            //    .Join(SerObj.ServiceProviders,Assigned=>Assigned.ServiceProviderFK,SP=>SP.Id,(Assigned,SP)=>new {Assigned.Charge,SP.UserId,SP.Contact,Assigned.Service,Assigned.ServiceProviderFK })
            //    .Join(SerObj.Users, g => g.UserId, u => u.Id, (g, u) => new {g.Charge,g.Service,g.Contact,u.Username,g.ServiceProviderFK})
            //    .Where(g => g.Service.Service == service).Select(i => i);
            var ServiceProviders = from a in SerObj.Users
                                   join b in SerObj.ServicesAssigned on a.Id equals b.SP.UserId
                                   where b.Service.Service==service
                                   select new {a.Id,
                                       a.Username,
                                       b.Service.Service,
                                       b.Charge,
                                       b.ServiceProviderFK,
                                       
                                   };
                                

            foreach (var i in ServiceProviders)
            {
                var ServiceObj = new ServiceProviderModel();
               
                ServiceObj.ServiceProviderId = i.ServiceProviderFK;
                
                ServiceObj.ServiceProviderName = i.Username;
                ServiceObj.Rating = Data.Average(i.Id);
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
            
            
        }
        public static void EditProfile(ServiceProviderProfile User, int UserId)
        {
            ServicesContext contextObj = new ServicesContext();
            var UserObj = contextObj.Users.Where(g => g.Id == UserId).Select(g => g).FirstOrDefault();
            var ServiceProviderObj = contextObj.ServiceProviders.Where(g => g.UserId == UserId).Select(g => g).FirstOrDefault();
            UserObj.Username = User.UserName;
            ServiceProviderObj.Contact = User.Contact;
            ServiceProviderObj.BankFK = User.BankFk;


        }

        
        public static void PlaceOrder(ServiceProviderModel NewOrder)
        {
            ServicesContext ContextObj = new ServicesContext();
            Order orderObj = new Order() { Date = DateTime.Now,FinalPrice=NewOrder.Price, ScheduleDate = NewOrder.ScheduledDate, FromFK = NewOrder.CustomerId, Status = "Active", ToFK = NewOrder.ServiceProviderId };

            ContextObj.Orders.Add(orderObj);
            ContextObj.SaveChanges();

        }
        public static void AddReview(reviewmodel model,int orderid)
        {
            ServicesContext a = new ServicesContext();
            Review r = new Review();
            r.Stars = model.Stars;
            r.Comment = model.Comment;
            r.OrderIdFK = orderid;
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
        //Giri Get Specific Order
        public static PaymentModel GetOrder(int OrderId)
        {
            ServicesContext context = new ServicesContext();
            var OrderQuery = context.Orders.Include("UserFrom").Include("UserTo").Include("BankAccount").Include("User").Include("BankAccount")
                .Join(context.Customers,o=>o.UserFrom.Id,c=>c.UserId,(o,c)=>new {o.UserTo,o.UserFrom,o.FinalPrice,o.ScheduleDate,c.BankAccount,c.User,o.Id,c.User.Username })
                .Join(context.ServiceProviders,j=>j.UserTo.Id,s=>s.UserId,(j,s)=>new {j.Id,j.FinalPrice,j.ScheduleDate,j.User,j.Username,j.UserFrom,j.UserTo,j.BankAccount,s.BankAccount.AccountNumber })
                .Where(i => i.Id == OrderId)
                .Select(i=>i).FirstOrDefault();
            var Data = new PaymentModel() {
                Amount=OrderQuery.FinalPrice,
                FromAccountNo=OrderQuery.BankAccount.AccountNumber,
                ToAccountNo=OrderQuery.AccountNumber,
                FromBankName=OrderQuery.BankAccount.BankName,
                OrderId=OrderQuery.Id,
                Username=OrderQuery.Username
                 
            };
            
            return Data;
        }

        public static int Authenticate(PaymentModel Pay)
        {
            using(ServicesContext context = new ServicesContext())
            {
                var check = context.Users.Where(i => i.Username == Pay.Username && i.Password == Pay.Password).FirstOrDefault();
                if (check is null)
                    return 0;
                else
                    return 1;
            }
        }
        public static int MakePayment(PaymentModel Pay)
        {
            try
            {
                using (ServicesContext context = new ServicesContext())
                {
                    var customer = context.BankAccounts.Where(g => g.AccountNumber == Pay.FromAccountNo).FirstOrDefault();
                    customer.Balance = customer.Balance - Pay.Amount;
                    var ServiceProvider = context.BankAccounts.Where(g => g.AccountNumber == Pay.ToAccountNo).FirstOrDefault();
                    ServiceProvider.Balance += Pay.Amount;
                    var order = context.Orders.Where(g => g.Id == Pay.OrderId).FirstOrDefault();
                    order.Status = "Completed";
                    Payment payment = new Payment();
                    payment.Amount = Pay.Amount;
                    payment.Date = DateTime.Now;
                    payment.Status = "Paid";
                    payment.OrderIdFK = Pay.OrderId;
                    context.Payments.Add(payment);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch(Exception Exp)
            {
                ServicesContext context = new ServicesContext();
                var paymentRow = context.Payments.Where(i => i.OrderIdFK == Pay.OrderId).FirstOrDefault();
                paymentRow.Status = "Error Not Paid";
                context.SaveChanges();
                string Error = Exp.Message;
                return 0;
            }
        }
    }
}

