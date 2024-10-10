using ClothesManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Http.Results;

namespace ClothesManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext _dbContext;
        public CustomerController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Customer

        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var cartItems = _dbContext.CartItem.Where(ci => ci.UserId == userId).Include(m => m.Product).ToList(); 
            return View(cartItems);
        }

        [Authorize]
        public ActionResult AddToCart(int productId, CartItem CartItem)
        {
            var userId = User.Identity.GetUserId();
            var cartItem = _dbContext.CartItem.Where(ci => ci.ProductId == productId && ci.UserId == userId).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity++; 
                _dbContext.Entry(cartItem).State = EntityState.Modified;
                _dbContext.SaveChanges();

            }
            else
            {
                // Create a new cart item if it doesn't exist in the cart
                /*cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    UserId = userId // Associate the cart item with the current user
                };*/
                CartItem.ProductId = productId;
                CartItem.Quantity = 1;
                CartItem.UserId = User.Identity.GetUserId();
                _dbContext.CartItem.Add(CartItem);
                _dbContext.SaveChanges();
               // return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            string userId = User.Identity.GetUserId();
            var cartItem = _dbContext.CartItem.Where(ci => ci.ProductId == productId && ci.UserId == userId).FirstOrDefault();

            if (cartItem != null)
            {
                _dbContext.CartItem.Remove(cartItem);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShowWishList()
        {
            var UserId = User.Identity.GetUserId();
            var wishList = _dbContext.Wishlists.Where(x => x.UserId == UserId).Include(m => m.Product).ToList();
            return View(wishList);
        }

        [Authorize]
        public ActionResult AddToWishlist(int id, Wishlist w)
        {
            var UserId = User.Identity.GetUserId();
            var existdata = _dbContext.Wishlists.Where(x => x.ProductId == id && x.UserId == UserId).FirstOrDefault();
            if (existdata != null)
            {
                TempData["MsgExt"] = "Product is added in wishlist";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                w.UserId = User.Identity.GetUserId();
                w.ProductId = id;
                w.DateTime = DateTime.Now;
                _dbContext.Wishlists.Add(w);
                _dbContext.SaveChanges();
                TempData["MsgAdd"] = "Added to wishlist successfully";

                return RedirectToAction("Index", "Home");
            }

            //return View("Index");
        }

        public ActionResult DeleteWishlist(int id)
        {

            var delete_data = _dbContext.Wishlists.Where(x => x.Id == id).First();
            _dbContext.Wishlists.Remove(delete_data);
            _dbContext.SaveChanges();
            TempData["MsgRem"] = "Removed to wishlist successfully";

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult ShowOrder()
        {
            List<List<OrderDetails>> data = new List<List<OrderDetails>>();
            var UserId = User.Identity.GetUserId();
            var orderList = _dbContext.OrderDetails.Where(x => x.UserId == UserId).Include(x => x.Order).Include(x => x.Product).ToList();

            var groupedOrders = orderList.GroupBy(x => x.OrderId);
            foreach (var group in groupedOrders)
            {
                data.Add(group.ToList());
            }
            return View(data);
        }

        [Authorize]
        public ActionResult AddOrder(int total, Order o, OrderDetails od)
        {
            var UserId = User.Identity.GetUserId();
            o.ProductAmount = total;
            var result = _dbContext.Orders.Add(o);
            _dbContext.SaveChanges();

            if (result != null) 
            {
                var data = _dbContext.CartItem.Where(x => x.UserId == UserId).ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    od.ProductId = data[i].ProductId;
                    od.Quantity = data[i].Quantity;
                    od.UserId = UserId;
                    od.OrderId = result.Id;
                    _dbContext.OrderDetails.Add(od);
                    _dbContext.SaveChanges();
                    var product_data = _dbContext.Product.FirstOrDefault(x => x.Id == od.ProductId);
                    if (product_data != null)
                    {
                        product_data.Quantity -= data[i].Quantity;
                        _dbContext.Entry(product_data).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                    _dbContext.CartItem.Remove(data[i]);
                    _dbContext.SaveChanges();
                }
            }
            TempData["OrdMsgAdd"] = "Added to order successfully";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteOrder(int id)
        {
            var UserId = User.Identity.GetUserId();
            var data = _dbContext.OrderDetails.Where(x => x.UserId == UserId && x.OrderId == id).ToList();
            foreach (var items in data)
            {
                var product_data = _dbContext.Product.FirstOrDefault(x => x.Id == items.ProductId);
                if (product_data != null)
                {
                    product_data.Quantity += items.Quantity;
                    _dbContext.Entry(product_data).State = EntityState.Modified;
                }
                _dbContext.OrderDetails.Remove(items);
            }

            var delete_data = _dbContext.Orders.Where(x => x.Id == id).First();
            _dbContext.Orders.Remove(delete_data);
            _dbContext.SaveChanges();
            TempData["OrdMsgRem"] = "Removed to order successfully";

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddReview(int productId, int rating, string comment)
        {
            var userId = User.Identity.GetUserId();

            var review = new Review
            {
                ProductId = productId,
                Rating = rating,
                Comment = comment,
                UserId = userId,
                ReviewDate = DateTime.Now
            };

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", "Admin", new { id = productId });
        }

    }
}