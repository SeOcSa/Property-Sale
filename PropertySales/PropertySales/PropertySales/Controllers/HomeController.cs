using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PropertySales.ViewModels;
using Data;
using Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace PropertySales.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPropertyInfos _propertyInfos;
        private readonly ISales _propertySale;

        public HomeController(IPropertyInfos propertyInfos, ISales propertySale)
        {
            _propertyInfos = propertyInfos;
            _propertySale = propertySale;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                PropertyInfos = _propertyInfos.getAll()
            };

            return View(model);
        }

        public IActionResult Details(Guid id)
        {
            var model = _propertyInfos.get(id);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles ="Admin,User")]
        public IActionResult ConfirmBuy(Guid id)
        {
            var model = new ConfirmBuyViewModel
            {
                PropertyInfos = _propertyInfos.get(id)
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [Authorize (Roles ="Admin")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult saveChanges(Guid id)
        {
           _propertyInfos.ChangeState(id);

            var saleDetails = new SaleDetails
            {
                Id = Guid.NewGuid(),
                Property = _propertyInfos.get(id),
                Buyer = User.Identity.Name,
                Saler="Admin"

            };

            _propertySale.Add(saleDetails); 

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            var addModel = new PropertyDetails
            {
                Id = Guid.NewGuid(),
                Rooms = model.propertyInfos.Rooms,
                Kitchen = model.propertyInfos.Kitchen.Equals("Yes") ? true : false,
                IsAvailable = true,
                Surface = model.propertyInfos.Surface,
                Price = model.propertyInfos.Price,
                Type = model.propertyInfos.Type,
                Bathroom = model.propertyInfos.Bathroom,
                urlPhoto = SeedData.URLImage(null, "upload", model.photosUrl[0]),
                urlPhoto2 = SeedData.URLImage(null, "upload", model.photosUrl[1]),
                urlPhoto3 = SeedData.URLImage(null, "upload", model.photosUrl[2]),
                urlPhoto4 = SeedData.URLImage(null, "upload", model.photosUrl[3]),
            };

            _propertyInfos.Add(addModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var model = new ConfirmBuyViewModel
            {
                PropertyInfos=_propertyInfos.get(id)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Guid id, string name=null)
        {
            _propertyInfos.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
