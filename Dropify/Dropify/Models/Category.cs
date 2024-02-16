using Dropify.Logics;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dropify.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? CategoryParent { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime? ChangedDate { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public virtual List<Category>? ChildCategory
        {
            get
            {
                var c = new CategoryDAO().GetChildByParentId(CategoryId);
                return c;
            }
            set
            {
            }
        }
    }
}
