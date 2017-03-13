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
using parking_control.Service;
using parking_control.Service.Model;

namespace parking_control.Controllers
{
    public class ValidityDateController : Controller
    {

        // vai listar
        // GET: /ValidityDate/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            Service.ValidityControl.UpdateListDatesFromDB();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Add()
        {
            ValidityDateViewModel model = new ValidityDateViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ValidityDateViewModel model)
        {
            double price = 0;
            bool haveErrors = false;
            try
            {
                price = double.Parse(model.HourPrice);
            }
            catch (FormatException e)
            {
                ModelState.AddModelError("data", "Não foi possível reconhecer o valor do preço praticado");
                haveErrors = true;
            }
            if (price == 0)
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
        public ActionResult Update(int id)
        {
            ValidityDateViewModel model = new ValidityDateViewModel();
            ValidityDateControl date = ValidityDateControlModel.Select(id);
            model.HourPrice = date.HourPrice.ToString().Replace(".", ",");
            model.InitialDate = date.InitialDate;
            model.FinalDate = date.FinalDate;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ValidityDateViewModel model, int id)
        {
            double price = 0;
            bool haveErrors = false;
            try
            {
                price = double.Parse(model.HourPrice);
            }
            catch (FormatException e)
            {
                ModelState.AddModelError("data", "Não foi possível reconhecer o valor do preço praticado");
                haveErrors = true;
            }
            if (price == 0)
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

            ValidityDateControl vdc = new ValidityDateControl(id, price, model.InitialDate, model.FinalDate);
            Service.ValidityControl.UpdateDateControl(vdc);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]        
        public async Task<ActionResult> Delete(ValidityDateViewModel model, int id)
        {
            bool haveErrors = false;
            if (id == 0)
            {
                haveErrors = true;
                ModelState.AddModelError("data", "Chave primária inválida");
            }                
            if (haveErrors)
                return View(model);

            try
            {
                Service.ValidityControl.DeleteDateControlById(id);
            }
            catch (NotFoundIDEntity e)
            {
            }

            return RedirectToAction("Index");
        }
    }
}