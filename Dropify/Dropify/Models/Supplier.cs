using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime? CooperateDate { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
