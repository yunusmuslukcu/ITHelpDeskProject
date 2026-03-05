using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Authorize] // Sadece giriş yapmış kullanıcılar ajandayı görebilsin
    public class CalendarController : Controller
    {
        // Şimdilik sadece sayfayı döndürüyoruz
        public IActionResult Index()
        {
            return View();
        }

        
    }
}