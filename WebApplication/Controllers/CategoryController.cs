using AutoMapper; // 1. AutoMapper'ı ekledik
using Business.Abstract;
using Dto.CategoryDtos;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{

    [Authorize(Roles ="Admin")]   //Yetkilendirme
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; // 3. Mapper nesnemizi tanımladık

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper; // 4. Dependency Injection ile doldurduk
        }

        public IActionResult Index()
        {
            var values = _categoryService.TGetList();
            // Entity listesini, DTO listesine çeviriyoruz
            var mappedValues = _mapper.Map<List<CategoryListDto>>(values);
            return View(mappedValues);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryCreateDto categoryCreateDto)
        {
            // DTO'yu Entity'ye çeviriyoruz ki DB'ye kaydedebilelim
            var category = _mapper.Map<Category>(categoryCreateDto);
            _categoryService.TInsert(category);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            _categoryService.TDelete(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _categoryService.TGetById(id);
            // Güncelleme sayfasına gönderirken DTO'ya çeviriyoruz (Güvenlik!)
            var mappedValue = _mapper.Map<CategoryUpdateDto>(value);
            return View(mappedValue);
        }

        [HttpPost]
        public IActionResult UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            // Gelen DTO'yu Entity'ye çevirip güncelliyoruz
            var category = _mapper.Map<Category>(categoryUpdateDto);
            _categoryService.TUpdate(category);
            return RedirectToAction("Index");
        }
    }
}