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
    }
}
