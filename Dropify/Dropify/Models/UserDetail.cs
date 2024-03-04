using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Dropify.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            Orders = new HashSet<Order>();
            UserAddresses = new HashSet<UserAddress>();
        }
        [Key]
        public int Udid { get; set; }
        public int? Uid { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Input Date")]
        [Display(Name = "Date of bird")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
