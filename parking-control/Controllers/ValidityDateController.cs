using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using parking_control.Models;

namespace parking_control.Controllers
{
    public class ValidityDateController : Controller
    {

        // vai listar
        // GET: /ValidityDate/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Add(string returnUrl)
        {
            AddValidityDateViewModel model = new AddValidityDateViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddValidityDateViewModel model, string returnUrl)
        {
            if (!model.IsValid())
            {
                ModelState.AddModelError("data", "Ocorreu algum erro no formato das datas ou no valor praticado");
                return View(model);
            }
                
            // (time.Year == 1 && time.Month == 1 && time.Day == 1 && time.Hour == 0 && time.Minute == 0 && time.Second == 0)
            if (!ModelState.IsValid)
                return View(model);
            

            return RedirectToAction("Index");
        }
    }
}