    using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Text.RegularExpressions;

namespace BigProject.Helper
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            var account = new Account(
                "dkh1dujcl",    //  Cloudinary cloud name
                "327523652721919",       // Cloudinary API key
                "Ck64akZQteJXIMu5m3bLNLpzW_E"     //  Cloudinary API secret
            );
            _cloudinary = new Cloudinary(account);
        }

        // Function to delete an image from Cloudinary by public_id
        public async Task<bool> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };
            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result.Result == "ok";
        }

        // Function to upload a new image from IFormFile and return the secure URL
        public async Task<string> UploadImage(IFormFile newImage)
            {
                // Convert IFormFile to Stream and upload the image
                using (var stream = newImage.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(newImage.FileName, stream) // Use stream for the file upload
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    return uploadResult.SecureUrl.ToString(); // Returns the secure URL of the uploaded image
                }
            }

            // Function to replace an old image with a new one
            public async Task<string> ReplaceImage(string url, IFormFile newImage)
            {
                // Loại bỏ phần base URL và version
                var parts = url.Split(new[] { "image/upload/" }, StringSplitOptions.None);
                if (parts.Length < 2) return null; // Nếu URL không hợp lệ, trả về null

                var publicIdWithExtension = parts[1].Substring(parts[1].IndexOf('/') + 1);

                // Loại bỏ phần mở rộng (.jpg, .png, ...)
                var oldPublicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));
                // Step 1: Delete the old image
                bool isDeleted = await DeleteImage(oldPublicId);

                if (!isDeleted)
                {
                    throw new Exception("Failed to delete the old image from Cloudinary.");
                }

                // Step 2: Upload the new image
                string newImageUrl = await UploadImage(newImage);

                return newImageUrl; // Return the URL of the newly uploaded image
            }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Tệp không hợp lệ.");

            using (var stream = file.OpenReadStream())
            {
                // Làm sạch tên file
                var originalFileName = Path.GetFileNameWithoutExtension(file.FileName).Trim();

                // Thay ký tự đặc biệt bằng gạch dưới (chỉ giữ chữ, số, gạch dưới)
                var safeFileName = System.Text.RegularExpressions.Regex.Replace(originalFileName, @"[^a-zA-Z0-9_\-]", "_");

                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = safeFileName
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                // Kiểm tra lỗi upload
                if (result.Error != null)
                {
                    throw new Exception($"Upload lỗi: {result.Error.Message}");
                }

                // Kiểm tra SecureUrl
                if (result.SecureUrl == null)
                {
                    throw new Exception("Tải lên thất bại: không có SecureUrl được trả về.");
                }

                return result.SecureUrl.ToString();
            }
        }


        // ✅ Replace file (Excel, etc.)
        public async Task<string> ReplaceFile(string url, IFormFile newFile)
                {
                    var oldPublicId = ExtractPublicIdFromUrl(url);
                    if (string.IsNullOrEmpty(oldPublicId)) return null;

                    bool deleted = await DeleteFile(oldPublicId);
                    if (!deleted) throw new Exception("Failed to delete old file.");

                    return await UploadFile(newFile);
                }

        // ✅ Extract publicId from Cloudinary URL
        public string ExtractPublicIdFromUrl(string url)
        {
            try
            {
                var parts = url.Split(new[] { "raw/upload/" }, StringSplitOptions.None);
                if (parts.Length < 2) return null; // Nếu URL không hợp lệ, trả về null

                var publicIdWithExtension = parts[1].Substring(parts[1].IndexOf('/') + 1);

                // Loại bỏ phần mở rộng (.jpg, .png, ...)
                var oldPublicId = publicIdWithExtension.Substring(0, publicIdWithExtension.LastIndexOf('.'));

                return oldPublicId;
            }
            catch
            {
                return null;
            }
        }



        //public async Task<bool> FileExists(string publicId)
        //    {
        //        try
        //        {
        //            var result = await _cloudinary.GetResourceAsync(new GetResourceParams(publicId)
        //            {
        //                ResourceType = ResourceType.Raw
        //            });

        //            return result != null && result.PublicId == publicId;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }


        public async Task<bool> DeleteFile(string publicUrl)
        {
            var publicId = ExtractPublicIdFromUrl(publicUrl);
            if (string.IsNullOrEmpty(publicId))
            {
                Console.WriteLine("PublicId không hợp lệ.");
                return false;
            }

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Raw
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result.Result == "ok";
        }

        public string GetEmbeddedUrl(string urlFile)
        {
            if (string.IsNullOrWhiteSpace(urlFile))
                return null;

            // Chuẩn hóa url (thay dấu cách bằng %20)
            var urlWithNoSpaces = urlFile.Replace(" ", "%20");

            if (urlWithNoSpaces.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                // File PDF: dùng trực tiếp
                return urlWithNoSpaces;
            }
            else
            {
                // Các loại file Office khác: dùng Office Web Viewer
                return $"https://view.officeapps.live.com/op/embed.aspx?src={Uri.EscapeDataString(urlWithNoSpaces)}";
            }
        }
    }
}
