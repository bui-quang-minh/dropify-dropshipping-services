using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class Product
    {
        public Product()
        {
            News = new HashSet<News>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? SellOutPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Shipdate { get; set; }
        public string? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
