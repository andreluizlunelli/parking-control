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
        public ActionResult Add()
        {
            AddValidityDateViewModel model = new AddValidityDateViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddValidityDateViewModel model)
        {
            double price = 0;
            bool haveErrors = false;
            try
            {
                string doubleParsed = model.HourPrice.ToString().Replace(",", ".");
                price = double.Parse(doubleParsed);
            }
            catch (FormatException e)
            {
                ModelState.AddModelError("data", "Não foi possível reconhecer o valor do preço praticado");
                haveErrors = true;
            }
            if (model.HourPrice == 0)
            {
                ModelState.AddModelError("data", "Valor praticado não pode ser 0");
                haveErrors = true;
            }
            if (!model.DateValid(model.InitialDate))
            {
                ModelState.AddModelError("data", "Data inicial inválida");
                haveErrors = true;
            }
            if (!model.DateValid(model.FinalDate))
            {
                ModelState.AddModelError("data", "Data final inválida");
                haveErrors = true;
            }
            if (haveErrors)
                return View(model);
            
            Service.ValidityControl.AddDateControl(price, model.InitialDate, model.FinalDate);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Update(string returnUrl)
        {
            AddValidityDateViewModel model = new AddValidityDateViewModel();
            return View(model);
        }

    }
}