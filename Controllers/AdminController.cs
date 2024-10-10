using ClothesManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _dbContext;

        public AdminController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString)
        {

            var proList = _dbContext.Product.Include(m => m.Category).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                /*empList = empList.Where(n => n.Department.DeptName.ToLower().Contains(searchString)).ToList();*/
                proList = proList.Where(n => n.Category.Type.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            if (proList == null)
                return HttpNotFound();
            return View(proList);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddProduct(Product pro) // Get
        {
            var proList = _dbContext.Product.Include(m => m.Category).ToList();
            ViewBag.category = proList;
            if (pro.Id > 0)

                return View(pro);
            else
            {
                ModelState.Clear();
                ViewBag.NoData = 0;
                return View();
            }
        }


        [HttpPost]
        public ActionResult CreateProduct(Product pro, HttpPostedFileBase File) //Action overload
        {
            
                if (pro.Id <= 0)
                {
                ModelState.Remove("Id");
                    if (ModelState.IsValid)
                    {
                        string extension = Path.GetExtension(File.FileName);
                        if (extension.Equals(".jpg") || extension.Equals(".png") || extension.Equals(".jpeg"))
                        {
                            string filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                            string savepath = Server.MapPath("~/Content/Images/");
                            File.SaveAs(savepath + filename);
                            pro.ProductPic = filename;
                            _dbContext.Product.Add(pro); // Save Product
                            _dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return Content("Only upload JPG or PNG file");
                        }
                    }
                }
                else
                {
                    string filename = "";
                    if (File != null)
                    {
                        string extension = Path.GetExtension(File.FileName);
                        if (extension.Equals(".jpg") || extension.Equals(".png") || extension.Equals(".jpeg"))
                        {
                            filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                            string savepath = Server.MapPath("~/Content/Images/");
                            File.SaveAs(savepath + filename);
                            pro.ProductPic = filename;
                            _dbContext.Entry(pro).State = EntityState.Modified;
                            _dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }

                }

            return View("AddProduct");
            
            

        }
        public ActionResult Details(int id)
        {
            var product = _dbContext.Product
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (product != null && product.Reviews.Any())
            {
                var averageRating = product.Reviews.Average(r => r.Rating); 
                ViewBag.AverageRating = averageRating;
            }
            else
            {
                ViewBag.AverageRating = 0; 
            }

            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            var data = _dbContext.Product.Where(x => x.Id == Id).First();
            _dbContext.Product.Remove(data);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddCategory() // Get
        {
            ModelState.Clear();
            ViewBag.NoData = 0;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category Ct) // Post
        {
            if (Ct != null)
            {
                _dbContext.Category.Add(Ct); // Save Department
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}