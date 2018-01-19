using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xmlDemo.Models
{
    public class ListModel
    {
        public IEnumerable<string> SelectedLeftName{ get; set; }
        public IEnumerable<string> SelectedRightName { get; set; }
        public IEnumerable<SelectListItem> LeftList { get; set; }
        public IEnumerable<SelectListItem> RightList { get; set; }
        
    }
}