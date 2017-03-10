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
    [Authorize]
    public class ValidityDateController : Controller
    {

        // vai listar
        // GET: /ValidityDate/Index
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ValidityDateIndexModel model = new ValidityDateIndexModel();            
            Service.ValidityControl.AddDateControl(0, new DateTime(2015, 8, 16, 0, 0, 0), new DateTime(2015, 9, 16, 15, 30, 0));
            return View(model);
        }
        
    }
}