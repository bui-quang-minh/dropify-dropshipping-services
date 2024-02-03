using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            Orders = new HashSet<Order>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public int Udid { get; set; }
        public int? Uid { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Sex { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ImgUrl { get; set; }
        public bool Admin { get; set; }
        public string? Status { get; set; }

        public virtual User? UidNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
