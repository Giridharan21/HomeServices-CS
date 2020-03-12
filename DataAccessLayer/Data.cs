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
      public static string Addlogin(Login Model)
        {
            ServicesContext i = new ServicesContext();
            var res = (from a in i.Users where a.Username == Model.UserName &&
                       a.Password == Model.Password select a.Type).FirstOrDefault();

            return res;

        }
       }


    
}