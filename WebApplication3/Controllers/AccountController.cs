using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebApplication3.Models;
using Security.Servico;
using Security.Entidade;
using Security.UserManager;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IServicoUsuario servicoUsuario;
        private readonly IUserManager userManager;        
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(IServicoUsuario servicoUsuario, IUserManager userManager, IAuthenticationManager authenticationManager)
        {
            this.servicoUsuario = servicoUsuario;
            this.userManager = userManager;
            this.authenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await this.servicoUsuario.LogOn(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.servicoUsuario.Registrar(model.Email, model.Password);
            }

            return View(model);
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        protected override void OnException(ExceptionContext filterContext)
        {
            ModelState.AddModelError("", filterContext.Exception.Message);

            base.OnException(filterContext);
        }

        private async Task SignInAsync(Usuario usuario, bool isPersistent)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie));
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}