using Contas.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Site.Controllers
{
    public class ErroController : Controller
    {
        private readonly IUser _user;

        public ErroController(IUser user)
        {
            _user = user;
        }

        [Route("/error")]
        [Route("/error/{id}")]
        public IActionResult Error(string id)
        {
            switch (id)
            {
                case "404": return View("NotFound");
                case "403":
                case "401":
                    if (!_user.IsAuthenticated()) return RedirectToAction("Login", "Account");
                    return View("AccessDenied");
            }

            return View();
        }
    }
}
