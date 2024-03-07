using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [EmailAddress(ErrorMessage = "Email address incorrect format.")]
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email address (*)")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [Display(Name = "Password (*)")]
        public string Pword { get; set; }
        public string? Status { get; set; }
        
        public virtual ICollection<UserDetail> UserDetails { get; set; }

        // set ImgUrl là thuộc tính của User để lấy ảnh từ UserDetail
        // Người viết : NQT
        [NotMapped]
        public virtual string? ImgUrl
        {
            get
            {
                using (var db = new prn211_dropshippingContext())
                {
                    var user = db.UserDetails.FirstOrDefault(u => u.Uid == Uid);
                    return user?.ImgUrl;
                }
            }

            set
            {
            }
        }

        // set ImgUrl là thuộc tính của User để lấy Name user từ UserDetail 
        // Người viết : NQT

        [NotMapped]
        public virtual string? NameUser
        {
            get
            {
                using (var db = new prn211_dropshippingContext())
                {
                    var user = db.UserDetails.FirstOrDefault(u => u.Uid == Uid);
                    return user?.Name;
                }
            }

            set
            {
            }
        }
    }
}
