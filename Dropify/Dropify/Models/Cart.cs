namespace Dropify.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { 
            get {
                using (var context = new prn211_dropshippingContext())
                {
                    return context.Products.Find(ProductId).ProductShortenName;
                }
            }
            set { }
                
        }
        public string ProductImage { 
            get 
            {
                using (var context = new prn211_dropshippingContext())
                {
                    return context.Products.Find(ProductId).ProductThumbnailImage;
                }
            }
            set { }
        }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }
        public string ProductOptions { get; set; }

        public string? ProductColorDetail
        {
            get
            {
                using (var context = new prn211_dropshippingContext())
                {
                    try
                    {
                        ProductDetail? color = context.ProductDetails.FirstOrDefault(x => x.ProductDetailId == int.Parse(ProductColor));
                        return color?.Attribute;
                    }
                    catch (Exception e)
                    {
                        return "";
                    }
                }
            }
            set { }
        }  
        public string? ProductSizeDetail
        {
            get
            {
                using (var context = new prn211_dropshippingContext())
                {
                    try { 
                        ProductDetail? size = context.ProductDetails.FirstOrDefault(x => x.ProductDetailId == int.Parse(ProductSize));
                        return size?.Attribute;
                    }
                    catch (Exception e) {
                        return "";
                    }
                }
            }
            set { }
        }
        public string? ProductOptionsDetail
        {
            get
            {
                using (var context = new prn211_dropshippingContext())
                {
                    try { 
                        ProductDetail? options = context.ProductDetails.FirstOrDefault(x => x.ProductDetailId == int.Parse(ProductOptions));
                        return options?.Attribute;
                    }
                    catch (Exception e)
                    {
                        return "";
                    }
                }
            }
            set { }
        }
    }
}
