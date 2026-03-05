using AutoMapper; // 1. Eklendi
using Business.Abstract;
using Dto.TicketDtos;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication.Controllers
{
    [Authorize]  //Yetkilendirme
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; // 3. Eklendi

        public TicketController(ITicketService ticketService, ICategoryService categoryService, IMapper mapper)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
            _mapper = mapper; // 4. Eklendi
        }

        public IActionResult Index()
        {
            var values = _ticketService.TGetList();
            // Entity listesini TicketListDto'ya çeviriyoruz. 
            // Hatırla: Mapping'de "CategoryName" kuralını yazmıştık, o da dolacak.
            var mappedValues = _mapper.Map<List<TicketListDto>>(values);
            return View(mappedValues);
        }

        [HttpGet]
        public IActionResult AddTicket()
        {
            List<SelectListItem> categories = (from x in _categoryService.TGetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.CategoryList = categories;
            return View();
        }

        [HttpPost]
        public IActionResult AddTicket(TicketCreateDto ticketCreateDto) // Entity değil DTO al!
        {
            if (ModelState.IsValid)
            {
                var ticket = _mapper.Map<Ticket>(ticketCreateDto);
                _ticketService.TInsert(ticket);
                return RedirectToAction("Index");
            }
            return View(ticketCreateDto);
        }

        public IActionResult DeleteTicket(int id)
        {
            var value = _ticketService.TGetById(id);
            _ticketService.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateTicket(int id)
        {
            var value = _ticketService.TGetById(id);

            List<SelectListItem> categories = (from x in _categoryService.TGetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.CategoryList = categories;

            // Veriyi DTO'ya çevirip View'a öyle gönderiyoruz
            var mappedValue = _mapper.Map<TicketUpdateDto>(value);
            return View(mappedValue);
        }

        [HttpPost]
        public IActionResult UpdateTicket(TicketUpdateDto ticketUpdateDto) // DTO alıyoruz
        {
            // BURAYA DİKKAT: Artık alanları tek tek elle eşitlemene gerek yok.
            // Önce DB'den orijinali çekiyoruz (Tarih vb. korunması için)
            var existingTicket = _ticketService.TGetById(ticketUpdateDto.Id);

            // AutoMapper'ın en güzel özelliklerinden biri: 
            // DTO'daki değişenleri alıp existingTicket'ın üzerine yazar (Overwriting)
            _mapper.Map(ticketUpdateDto, existingTicket);

            _ticketService.TUpdate(existingTicket);
            return RedirectToAction("Index");
        }
    }
}