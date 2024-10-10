using ClothesManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index(string searchString)
        {

            var proList = _dbContext.Product.Include(m => m.Category).Include(r => r.Reviews).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                /*empList = empList.Where(n => n.Department.DeptName.ToLower().Contains(searchString)).ToList();*/
                proList = proList.Where(n => n.Category.Type.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            if (proList == null)
                return HttpNotFound();
            return View(proList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}