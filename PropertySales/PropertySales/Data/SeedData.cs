using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SeedData
    {
        private readonly PropertyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
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
                    urlPhoto = URLImage("/images/Property1/livingroom.jpg"),
                    urlPhoto2 = URLImage("/images/Property1/livingroom2.jpg"),
                    urlPhoto3 = URLImage("/images/Property1/livingroom3.jpg"),
                    urlPhoto4 = URLImage("/images/Property1/livingroom4.jpg")
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
                    urlPhoto = URLImage("/images/Property2/livingroom.jpg"),
                    urlPhoto2 = URLImage("/images/Property2/livingroom2.jpg"),
                    urlPhoto3 = URLImage("/images/Property2/livingroom3.jpg"),
                    urlPhoto4 = URLImage("/images/Property2/livingroom4.jpg"),
                };

               await _context.AddRangeAsync(propertyInfo, propertyInfo2);
               await _context.SaveChangesAsync();
            }
        }

        private string URLImage(string imageUrl)
        {
            string path = _hostingEnvironment.WebRootPath + imageUrl;
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

            return imageDataURL;
        }
    }
}
