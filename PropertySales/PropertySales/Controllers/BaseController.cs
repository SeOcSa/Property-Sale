using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySales.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<User> UserManager;

        public BaseController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        [NonAction]
        protected async Task<User> GetLoggedInUser()
        {
            User result = null;

            if (User.Identity.IsAuthenticated)
                result = await UserManager.FindByNameAsync(User.Identity.Name);

            return result;
        }
    }
}
