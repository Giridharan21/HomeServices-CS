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
    

      public static string Addlogin(Login Model)
        {
            ServicesContext i = new ServicesContext();
            var res = (from a in i.Users where a.Username == Model.UserName &&
                       a.Password == Model.Password select a.Type).FirstOrDefault();
            
            return res;

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


    }    
}

