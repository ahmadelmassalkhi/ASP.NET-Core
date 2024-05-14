using CVInfoApp.Data;
using CVInfoApp.Models;

namespace CVInfoApp.Services
{
    public class CVService(
        DataContext _context, 
        IWebHostEnvironment _webHostEnvironment, 
        IHttpContextAccessor _httpContextAccessor) : ICVService
    {
        public List<CV> GetAllCVs() => [.. _context.CVs];

        public async Task DeleteCVAsync(long id)
        {
            // get cv by id
            var cv = await _context.CVs.FindAsync(id);
            if (cv == null) return; // already doesn't exist

            // remove & commit changes
            _context.CVs.Remove(cv);
            await _context.SaveChangesAsync();
        }

        public async Task AddCV(CVBindModel cvBindModel)
        {
            // create cv from bound model properties
            var cv = new CV
            {
                FirstName = cvBindModel.FirstName,
                LastName = cvBindModel.LastName,
                BirthDay = cvBindModel.BirthDay,
                Gender = cvBindModel.Gender,
                Email = cvBindModel.Email,
                Nationality = cvBindModel.Nationality,
                Password = cvBindModel.Password,
                PhotoURL = await AddPhoto(cvBindModel.Photo),
                Skills = cvBindModel.Skills,
            };

            // add & commit changes
            _context.Add(cv);
            await _context.SaveChangesAsync();
        }

        // helper method
        private async Task<string> AddPhoto(IFormFile image)
        {
            /* validate image file */
            if (image == null) throw new ArgumentException("Cannot work with null as a file for CV photo !");
            if (!IsImage(image)) throw new ArgumentException($"File `{image.FileName}` is not a valid image !");

            /* save a copy of the image on the server */
            
            // Construct the destination folder path `wwwroot/images`
            string imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            // Create it if doesn't exist
            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

            // Generate a unique filename to prevent overwriting
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            // Construct destination path `images/unique_name.extension`
            string savePath = Path.Combine(imagesFolder, fileName);

            // Copy the image to destination path
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // handle case where this method is called in scenarios where
            // the service is being used outside the scope of an HTTP request,
            // such as in background tasks, scheduled jobs, or during startup/configuration phases
            // where there is no HTTP context available 
            if (_httpContextAccessor.HttpContext == null) throw new InvalidOperationException("HTTP context is not available.");

            // return the relative URL to access the image
            return
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext.Request.Host}/{Path.Combine("images", fileName)}";
        }

        // Check the file extension to determine if it's an image
        public static readonly string[] EXTENSIONS_IMAGE = [".jpg", ".jpeg", ".png", ".gif"];
        private static bool IsImage(IFormFile file)
        {
            return EXTENSIONS_IMAGE.Contains(
                Path.GetExtension(file.FileName).ToLowerInvariant()
            );
        }
    }
}
