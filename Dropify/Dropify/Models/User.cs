using System;
using System.Collections.Generic;

namespace Dropify.Models
{
    public partial class User
    {
        public User()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Uid { get; set; }
        public string? Email { get; set; }
        public string? Pword { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
