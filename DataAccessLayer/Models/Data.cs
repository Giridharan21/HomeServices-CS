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