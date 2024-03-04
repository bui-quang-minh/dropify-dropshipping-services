using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dropify.Models
{
    public partial class User
    {
        public User()
        {
            UserDetails = new HashSet<UserDetail>();
        }
        [Key]
        public int Uid { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage ="Input Password")]
        [Display(Name ="Password")]
        public string? Pword { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
