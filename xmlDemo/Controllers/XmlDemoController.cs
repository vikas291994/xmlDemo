using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xmlDemo.Models;


namespace xmlDemo.Controllers
{
    public class XmlDemoController : Controller
    {
        // GET: XmlDemo
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayXMl()
        {
            var data = new List<CustomerModel>();
            //get data from source
            data = ReturnData();
            //retrn data to view using model directive
            return View(data);
        }

        private List<CustomerModel> ReturnData()
        {
            //get file path from server
            string xmldata = Server.MapPath("~/XmlFile/CustomerXml.xml");
            DataSet ds = new DataSet();
            //read the xml data from file using dataset
            ds.ReadXml(xmldata);
            //get data from dataset,convert and store it in list. 
            var customerlist =  new List<CustomerModel>();
            customerlist = (from rows in ds.Tables[0].AsEnumerable()
                            select new CustomerModel
                            {
                                CustomerID = Convert.ToInt32(rows[0].ToString()),
                                CustomerName = rows[1].ToString(),
                                Contact = rows[2].ToString(),
                                DateOfPurchase = Convert.ToDateTime(rows[3].ToString()),
                                Address = rows[4].ToString()
                            }).ToList();
            return customerlist;
        }

        // GET: XmlDemo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: XmlDemo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XmlDemo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: XmlDemo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: XmlDemo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: XmlDemo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: XmlDemo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            var m = new CustomerModel();
            return View(m);
        }
        [HttpPost]
        public ActionResult UploadFile(CustomerModel model)
        {
            var file = model.Files[0];
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }
           
            return View(model);
        }

        [HttpGet]
        public ActionResult GetNameListView()
        {
            var Result = GetListData();
            return View(Result);
        }

        private ListModel GetListData()
        {
            var NameList = db.Database.SqlQuery<NameListModel>("EXEC GetNameList").ToList();
            List<SelectListItem> LeftList = new List<SelectListItem>();
            List<SelectListItem> RightList = new List<SelectListItem>();
            foreach (var item in NameList)
            {
                if (item.IsSelected == false)
                {
                    SelectListItem selectList1 = new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = item.IsSelected
                    };
                    LeftList.Add(selectList1);
                }
                else
                {
                    SelectListItem selectList2 = new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = item.IsSelected
                    };
                    RightList.Add(selectList2);
                }


            }

            ListModel LM = new ListModel()
            {
                LeftList = LeftList
                ,RightList = RightList               
            };
            
            return LM;
        }

  
        public ActionResult GetAddListDataResult(string SelectedNames)
        {
            var IsInsert = AddOrRemoveItemsInList(SelectedNames);
            var Data = GetListData();
            return PartialView("GetNameListView", Data);
        }

   
        public ActionResult GetRemoveListDataResult(string SelectedNames)
        {
            var IsRemoved = AddOrRemoveItemsInList(SelectedNames);
            var result = GetNameListView();
            return View("GetNameListView", result);
        }

        private int? AddOrRemoveItemsInList(string Ids)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("Ids", Ids);
            var Result = db.Database.SqlQuery<int>("EXEC AddOrRemoveItemsInList @Ids", param).FirstOrDefault();
            return Result;
        }
    }
}
