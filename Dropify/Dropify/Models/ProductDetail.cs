using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class ProductDetail
    {
        public int ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public string? Type { get; set; }
        public string? Attribute { get; set; }
        public string? Status { get; set; }

        public virtual Product? Product { get; set; }
    }
}
