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
    public class VehicleController : Controller
    {

        // GET: /ValidityDate/Index
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            Service.VehicleControl.UpdateListDatesFromDB();
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
            DateTime tmpDate = DateTime.ParseExact(model.InitialDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            if (!model.DateValid(tmpDate))
            {
                ModelState.AddModelError("data", "Data inicial inválida");
                haveErrors = true;
            }
            if (haveErrors)
                return View(model);

            try
            {
                Service.VehicleControl.Entry(model.Board, tmpDate);
            }
            catch (NotFoundDateControl e)
            {
                ModelState.AddModelError("data", "Não foi encontrado um período vigente para associar a data ao preço cobrado. Adicione um Valor Praticado.");
                return View(model);
            }
            
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Update(int id)
        {
            VehicleUpdateViewModel model = new VehicleUpdateViewModel();
            VehicleEntrance vehicle = VehicleEntranceModel.Select(id);
            model.HourPrice = vehicle.HourPrice.ToString().Replace(".", ",");
            model.Board = vehicle.Board;
            model.InitialDate = vehicle.DateIn.ToString("dd/MM/yyyy HH:mm:ss");
            model.FinalDate = vehicle.DateOut.ToString("dd/MM/yyyy HH:mm:ss"); ;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(VehicleUpdateViewModel model, int id)
        {
            bool haveErrors = false;
            DateTime tmpDate = DateTime.ParseExact(model.FinalDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            if (!model.DateValid(tmpDate))
            {
                ModelState.AddModelError("data", "Data final inválida");
                haveErrors = true;
            }
            if (haveErrors)
                return View(model);            
            try
            {
                Service.VehicleControl.Out(model.Board, tmpDate);
            }
            catch (NotFoundDateControl e)
            {
                ModelState.AddModelError("data", "Não foi encontrado um período vigente para associar a data ao preço cobrado. Adicione um Valor Praticado.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Delete(VehicleUpdateViewModel model, int id)
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
                Service.VehicleControl.DeleteVehicleByID(id);
            }
            catch (NotFoundIDEntity e)
            {
            }

            return RedirectToAction("Index");
        }
    }
}