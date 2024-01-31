using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? OrderDetailParent { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? Type { get; set; }
        public string? Attribute { get; set; }
        public decimal? OrderedPrice { get; set; }
        public string? Status { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
