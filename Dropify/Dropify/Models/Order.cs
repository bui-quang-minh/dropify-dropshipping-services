using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dropify.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? Udid { get; set; }
        public DateTime OrderedDate { get; set; }
        public decimal? OrderedPrice { get; set; }
        public int AddressId { get; set; }
        public string? ShipStatus { get; set; }
        public string? Status { get; set; }

        public virtual UserAddress Address { get; set; } = null!;
        public virtual UserDetail? Ud { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

       
    }

}
