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
        public static void adduser(CustomerRegisterClass m)
        {
            var s = new ServicesContext();
            BankAccountDetails b = new BankAccountDetails();
            b.BankName = m.BankName;
            b.AccountNumber = m.BankAccNumber;
            b.Balance = 1000.00m;
            s.Accounts.Add(b);
            //s.SaveChanges();
            //var BankId = s.Accounts.Where(g => g.AccountNumber == m.BankAccNumber).Select(g => g.Id).FirstOrDefault();
            User a= new User();
            
            a.Username = m.Username;
            a.Password = m.Password;
          
            a.Contact = m.Contact;
            a.Location = m.Location;
           
            a.BankFK =b.Id;
            a.Type = "CUSTOMER";
               
            s.Users.Add(a);
            s.SaveChanges();

           
           
        }
        public static void registeruser(SPRegisterClass n)
        {
            var k = new ServicesContext();
            BankAccountDetails c = new BankAccountDetails();
            c.BankName = n.BankName;
            c.AccountNumber = n.BankAccNumber;
            c.Balance = 1000.00m;
            k.Accounts.Add(c);
            //s.SaveChanges();
            //var BankId = s.Accounts.Where(g => g.AccountNumber == m.BankAccNumber).Select(g => g.Id).FirstOrDefault();
            User d = new User();

            d.Username = n.Username;
            d.Password = n.Password;
            d.Service = n.Service;
         
            d.Contact = n.Contact;
            
            d.BankFK = c.Id;
            d.Type = "SERVICE PROVIDER";

            k.Users.Add(d);
            k.SaveChanges();



        }

    }
}