using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothesManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, ErrorMessage = "Product Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Display(Name = "Category Type")]
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Display(Name = "Product Picture")]
        [StringLength(255, ErrorMessage = "Picture URL cannot be longer than 255 characters.")]
        public string ProductPic { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Material is required.")]
        [StringLength(100, ErrorMessage = "Material cannot be longer than 100 characters.")]
        public string Material { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(100, ErrorMessage = "Brand cannot be longer than 100 characters.")]
        public string Brand { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}