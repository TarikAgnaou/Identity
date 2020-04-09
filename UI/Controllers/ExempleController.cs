using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DALC.Repository;
using DALQ.Repository;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ExempleController : Controller
    {
        private UserQuery _UserQuery = new UserQuery();
        private UserCommand _UserCommand = new UserCommand();

        public IActionResult Index()
        {
            return View();
        }
    }
}