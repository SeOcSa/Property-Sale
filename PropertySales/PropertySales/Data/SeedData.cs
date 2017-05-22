using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SeedData
    {
        private readonly PropertyDbContext _context;
        private static IHostingEnvironment _hostingEnvironment;
        public SeedData(PropertyDbContext contex, IHostingEnvironment env)
        {
            _context = contex;
            _hostingEnvironment = env;
        }

        public async Task EnsureSeedData()
        {
            await SeedPropertyInfos();
        }

        private async Task SeedPropertyInfos()
        {
            if (!_context.PropertyInfos.Any())
            {
                var propertyInfo = new PropertyDetails
                {
                    Id = Guid.NewGuid(),
                    Type = "Apartament",
                    Rooms = 3,
                    Kitchen = true,
                    Bathroom = 2,
                    Surface = 70,
                    IsAvailable = true,
                    Price = 100000,
                    urlPhoto = URLImage("/images/Property1/livingroom.jpg","seed",null),
                    urlPhoto2 = URLImage("/images/Property1/livingroom2.jpg","seed",null),
                    urlPhoto3 = URLImage("/images/Property1/livingroom3.jpg","seed",null),
                    urlPhoto4 = URLImage("/images/Property1/livingroom4.jpg","seed",null),
                };

                var propertyInfo2 = new PropertyDetails
                {
                    Id = Guid.NewGuid(),
                    Type = "Apartament",
                    Rooms = 4,
                    Kitchen = true,
                    Bathroom = 2,
                    Surface = 80,
                    IsAvailable = true,
                    Price = 120000,
                    urlPhoto = URLImage("/images/Property2/livingroom.jpg","seed",null),
                    urlPhoto2 = URLImage("/images/Property2/livingroom2.jpg","seed",null),
                    urlPhoto3 = URLImage("/images/Property2/livingroom3.jpg","seed",null),
                    urlPhoto4 = URLImage("/images/Property2/livingroom4.jpg","seed",null),
                };

               await _context.AddRangeAsync(propertyInfo, propertyInfo2);
               await _context.SaveChangesAsync();
            }
        }

        public static string URLImage(string imageUrl, string type,IFormFile file)
        {
            string path;
            if (type.Equals("seed"))
            {
                path = _hostingEnvironment.WebRootPath + imageUrl;
            }
            else
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                path = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(fileStream).Wait();
                }
            }

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

            return imageDataURL;
        }
    }
}
