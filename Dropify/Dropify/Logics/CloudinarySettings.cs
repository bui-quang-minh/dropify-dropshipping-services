using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Principal;

namespace Dropify.Logics
{
    // Settings của Cloudinary
    // Người viết: Bùi Quang Minh
    // Ngày: 16/2/2024
    public class CloudinarySettings
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public CloudinarySettings() { }
        // Lấy thông tin settings của Cloudinary từ appsettings.json
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public static CloudinarySettings GetCloudinarySettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var cloudinarySettings = new CloudinarySettings();
            configuration.GetSection("CloudinarySettings").Bind(cloudinarySettings);
            return cloudinarySettings;
        }
        // Upload ảnh lên Cloudinary
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public ImageUploadResult CloudinaryUpload(IFormFile f)
        {
            CloudinarySettings cs = GetCloudinarySettings();
            Account account = new Account(cs.CloudName, cs.ApiKey, cs.ApiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(f.FileName, f.OpenReadStream())

            };
            return cloudinary.Upload(uploadParams);
        }
    }
}
