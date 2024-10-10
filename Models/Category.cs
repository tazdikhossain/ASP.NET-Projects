using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothesManagementSystem.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Type is required.")]
        [StringLength(100, ErrorMessage = "Category Type cannot be longer than 100 characters.")]
        public string Type { get; set; }
    }
}