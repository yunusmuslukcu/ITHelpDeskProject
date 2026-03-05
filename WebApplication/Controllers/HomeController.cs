using AutoMapper; // Mapper kullanıyorsan eklemelisin
using Business.Abstract;
using Dto.TicketDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; // Veriyi DTO'ya çevirmek için

        public HomeController(ITicketService ticketService, ICategoryService categoryService, IMapper mapper)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            // 1. Verileri Veritabanından Çek
            var allTickets = _ticketService.TGetList();
            var allCategories = _categoryService.TGetList();

            // 2. İstatistikleri ViewBag'e At (Dashboard Kartları İçin)
            ViewBag.TotalTickets = allTickets.Count;
            ViewBag.ActiveTickets = allTickets.Count(x => x.Status != "Çözüldü" && x.Status != "İptal Edildi");
            ViewBag.SolvedTickets = allTickets.Count(x => x.Status == "Çözüldü");
            ViewBag.TotalCategories = allCategories.Count;

            // 3. Son Aktif Talepler Tablosu İçin Son 5 Kaydı Al
            // Id'ye göre tersten sıralayıp (en yeni en üstte) ilk 5 tanesini seçiyoruz
            var lastFiveTickets = allTickets.OrderByDescending(x => x.Id).Take(5).ToList();

            // 4. Entity listesini View'da kullandığın DTO listesine dönüştür
            var mappedTickets = _mapper.Map<List<TicketListDto>>(lastFiveTickets);

            // 5. Modeli View'a Gönder
            return View(mappedTickets);
        }
    }
}