using System.Security.Claims;
using System.Threading.Tasks;
using LinkendInSecurity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToolBox.TOs;

namespace LinkendInSecurity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserTO> _userManager;
        private readonly SignInManager<UserTO> _signInManager;

        public AccountController(UserManager<UserTO> userManager, SignInManager<UserTO> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Subscription()
        {
            return View(new SubscriptionViewModel());
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscription(SubscriptionViewModel subscriptionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(subscriptionVM);
            }

            var user = new UserTO
            {
                Email = subscriptionVM.Email,
                FirstName = subscriptionVM.FirstName,
                LastName = subscriptionVM.LastName,
                UserName = subscriptionVM.Email
            };

            var result = await _userManager.CreateAsync(user, subscriptionVM.Password);

            if (result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, subscriptionVM.RoleSelected);

                if (resultRole.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Age", subscriptionVM.Age.ToString()));
                    return RedirectToAction("Index", "home");
                }
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return View(subscriptionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(
                    userName: loginVM.Email,
                    password: loginVM.Password,
                    isPersistent: true,
                    lockoutOnFailure: false
                    );
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Identifiant incorrect", "Identifiant incorrect");

                    return View(loginVM);
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}