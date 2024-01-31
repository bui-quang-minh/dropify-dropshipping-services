using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class UserAddress
    {
        public UserAddress()
        {
            Orders = new HashSet<Order>();
        }

        public int AddressId { get; set; }
        public int? Udid { get; set; }
        public string? Address { get; set; }
        public bool? Default { get; set; }
        public string? Status { get; set; }

        public virtual UserDetail? Ud { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
