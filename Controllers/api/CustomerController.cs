using ClothesManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace ClothesManagementSystem.Controllers.api
{
    public class CustomerController : ApiController
    {
        ApplicationDbContext _dbContext;
        public CustomerController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET api/<controller>
        [Route("api/index")]
        [HttpGet]
        public IEnumerable<Wishlist> GetIndex()
        {
            var UserId = User.Identity.GetUserId();
            return _dbContext.Wishlists.Where(x => x.UserId == UserId).Include(m => m.Product).ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [Route("api/addWishlist/{id}")]
        [HttpPost]
        public IHttpActionResult AddWishlist(int id, Wishlist w)
        {
            var UserId = User.Identity.GetUserId();
            var existdata = _dbContext.Wishlists.Where(x => x.ProductId == id && x.UserId == UserId).FirstOrDefault();
            if (existdata != null)
            {
                return BadRequest();
            }
            else
            {
                w.UserId = User.Identity.GetUserId();
                w.ProductId = id;
                w.DateTime = DateTime.Now;
                _dbContext.Wishlists.Add(w);
                _dbContext.SaveChanges();

                return Ok(w);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        [Route("api/deletewishlist/{id}")]
        [HttpDelete]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var delete_data = _dbContext.Wishlists.Where(x => x.Id == id).First();
            _dbContext.Wishlists.Remove(delete_data);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}