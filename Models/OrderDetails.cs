﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothesManagementSystem.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}