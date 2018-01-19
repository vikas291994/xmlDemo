using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xmlDemo.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public List<HttpPostedFileBase> Files { get; set; }
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public Nullable<DateTime> DateOfPurchase { get; set; }
        public string Address { get; set; }
    }
}