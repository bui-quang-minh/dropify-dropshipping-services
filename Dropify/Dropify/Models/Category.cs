using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? CategoryParent { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
