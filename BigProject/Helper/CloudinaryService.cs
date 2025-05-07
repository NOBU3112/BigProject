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
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new RawUploadParams()
                        {
                            File = new FileDescription(file.FileName, stream),
                            PublicId = Path.GetFileNameWithoutExtension(file.FileName)
                        };

                        var result = await _cloudinary.UploadAsync(uploadParams);
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
                var split = url.Split(new[] { $"raw/upload/" }, StringSplitOptions.None);
                if (split.Length < 2) return null;

                var path = split[1];

                // Bỏ domain version nếu có (vXXXXXXXXX)
                var versionPattern = @"^v\d+/";
                path = Regex.Replace(path, versionPattern, "");

                // Decode URL nếu có ký tự đặc biệt bị mã hóa
                path = Uri.UnescapeDataString(path);

                // Xoá phần mở rộng (".xlsx", ".pdf",...) để lấy publicId
                var lastDot = path.LastIndexOf('.');
                if (lastDot > 0)
                {
                    path = path.Substring(0, lastDot);
                }

                return path;
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
    }
}
