using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothesManagementSystem.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public virtual Product Product { get; set; }
    }
}