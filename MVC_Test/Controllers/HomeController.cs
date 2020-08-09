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
        /*取得首頁的資料傳值*/
        public ActionResult Index()
        {
            SQLConnection db_connect = new SQLConnection();
            List<Test> Lists = db_connect.GetLists();
            ViewBag.lists = Lists;

            return View();
        }

        /*新增*/
        public ActionResult Add()
        {
            ViewBag.Message = "Add New Item";

            return View();
        }

        /*接Post的function*/
        [HttpPost]
        [Route("addItem")]
        public string addItem(string name, int age)
        {
            SQLConnection sqlconnect = new SQLConnection();
            return JsonConvert.SerializeObject(sqlconnect.AddLists(name, age));
        }
    }
}