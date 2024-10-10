using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothesManagementSystem.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }


        public int ProductId { get; set; }
        public string UserId { get; set; }

        public virtual Product Product { get; set; }
    }
}