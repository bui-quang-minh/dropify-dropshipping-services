using Dropify.Logics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dropify.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string? NewsType { get; set; }
        public int? ProductId { get; set; }
        public string? ImgUrl { get; set; }
        public string? NewsContents { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? Statis { get; set; }

        public virtual Product? Product { get; set; }

        [NotMapped]
        public virtual string? NewsProduct
        {
            get
            {
                var cat = new ProductDAO().GetProductById(ProductId ?? 0);
                return cat?.ProductShortenName;
            }
            set
            {
            }
        }
    }
}
