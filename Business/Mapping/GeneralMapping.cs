using AutoMapper;
using Dto.CategoryDtos;
using Dto.TicketDtos;
using Entity.Concrete; 


namespace Business.Mapping // Business katmanında olduğu için namespace böyle olmalı
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // 1. Kategori Eşleşmeleri
            // .ReverseMap() sayesinde hem Entity -> DTO hem de DTO -> Entity dönüşümü yapılır.
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            // 2. Ticket Eşleşmeleri
            CreateMap<Ticket, TicketCreateDto>().ReverseMap();
            CreateMap<Ticket, TicketUpdateDto>().ReverseMap();

            // 3. Özel Eşleşme (TicketListDto)
            // Ticket listelerken Kategori ID'si yerine Kategori Adı'nı göstermek istiyoruz.
            CreateMap<Ticket, TicketListDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();
        }
    }
}