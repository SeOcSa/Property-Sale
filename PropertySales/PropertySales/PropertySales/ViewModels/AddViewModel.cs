using Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySales.ViewModels
{
    public class AddViewModel
    {
        public PropertyDetails propertyInfos { get; set; }
        public List<IFormFile> photosUrl { get; set; }
    }
}
