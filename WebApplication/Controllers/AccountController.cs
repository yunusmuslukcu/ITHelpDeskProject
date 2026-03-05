using Business.Abstract;
using Dto.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [AllowAnonymous] // Herkes giriş yapabilir!
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDto);
            }

            var result = await _accountService.CreateUserAsync(userRegisterDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRegisterDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }

            var result = await _accountService.LoginAsync(userLoginDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Çok fazla hatalı deneme yaptınız. Hesabınız geçici olarak kilitlendi.");
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            }

            return View(userLoginDto);
        }

        // GÜNCELLEME: Güvenlik için POST olarak işaretledik
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        // Erişim engellendiğinde yönlendirilecek sayfa
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}