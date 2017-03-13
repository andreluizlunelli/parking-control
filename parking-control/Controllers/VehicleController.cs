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

namespace parking_control.Controllers
{    
    public class VehicleController : Controller
    {

        // GET: /ValidityDate/Index
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            Service.VehicleControl.UpdateDictDatesFromDB();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Add()
        {
            VehicleAddViewModel model = new VehicleAddViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(VehicleAddViewModel model)
        {            
            bool haveErrors = false;
            if (model.Board.Length == 0)
            {
                ModelState.AddModelError("data", "Placa do carro não pode ter valor vazio");
                haveErrors = true;
            }
            if (!model.DateValid(model.InitialDate))
            {
                ModelState.AddModelError("data", "Data inicial inválida");
                haveErrors = true;
            }
            if (haveErrors)
                return View(model);

            try
            {
                Service.VehicleControl.Entry(model.Board, model.InitialDate);
            }
            catch (NotFoundDateControl e)
            {
                ModelState.AddModelError("data", "Não foi encontrado um período vigente para associar a data ao preço cobrado. Adicione um Valor Praticado.");
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
    }
}