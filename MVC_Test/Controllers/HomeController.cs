using MVC_Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SQLConnection db_connect = new SQLConnection();
            List<Test> Lists = db_connect.GetLists();
            ViewBag.lists = Lists;

            return View();
        }

        public ActionResult Add()
        {
            ViewBag.Message = "Add New Item";

            return View();
        }

        [HttpPost]
        [Route("addItem")]
        public string addItem(string name, int age)
        {
            SQLConnection sqlconnect = new SQLConnection();
            return JsonConvert.SerializeObject(sqlconnect.AddLists(name, age));
        }
    }
}