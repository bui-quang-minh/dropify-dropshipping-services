using Dropify.Logics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dropify.Models
{
    public partial class Product
    {
        public Product()
        {
            News = new HashSet<News>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal? SellOutPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Shipdate { get; set; }
        public string? Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        [NotMapped]
        public virtual string? ProductShortenName
        {
            get
            {
                if (Name.Length <= 50)
                {
                    return Name;
                }
                else
                {
                    return Name.Substring(0, 50) + "...";
                }
            }
            set
            {
            }
        }

        [NotMapped]
        public virtual string? ProductThumbnailImage
        {
            get
            {
                using (var db = new prn211_dropshippingContext())
                {
                    var thumbnailImage = db.ProductDetails.FirstOrDefault(x => x.ProductId == ProductId && x.Type.Equals("P_IMAGE_THUMBNAIL"));
                    return thumbnailImage?.Attribute;
                }
            }

            set
            {
            }
        }
        [NotMapped]
        public virtual UserDetail? ProductCreateBy
        {
            get
            {
                var u = new UserDetailDAO().GetUserDetailById(CreatedBy);
                return u;
            }
            set
            {
            }
        }
        [NotMapped]
        public virtual string? ProductCategory
        {
            get
            {
                var cat = new ProductDAO().GetProductCategoryById(CategoryId ?? 0);
                return cat?.CategoryName;
            }
            set
            {
            }
        }

        [NotMapped]
        public virtual string? ProductDescription
        {
            get
            {
                using (var db = new prn211_dropshippingContext())
                {
                    var description = db.ProductDetails.FirstOrDefault(x => x.ProductId == ProductId && x.Type.Equals("P_DESCRIPTION"));
                    if (description?.Attribute.Length <= 97)
                    return description?.Attribute;
                    else
                    {
                        return description?.Attribute.Substring(0, 97) + "...";
                    }
                }
            }
            set
            {
            }
        }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
