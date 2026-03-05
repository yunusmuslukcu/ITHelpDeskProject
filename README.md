IT Help Desk
Şirket içi IT destek taleplerini yönetmek için geliştirdiğim, ASP.NET Core MVC tabanlı bir web uygulaması.

Proje Hakkında
Kullanıcılar sisteme giriş yaparak öncelik seviyesi ve kategorisiyle birlikte destek talebi (ticket) oluşturabilir. Talepler üzerinde durum takibi yapılabilir: Açık, İşlemde veya Çözüldü. Admin ve üye rolleri ayrıştırılmış olup oturum açılmadan hiçbir sayfaya erişilemez.

Kullanılan Teknolojiler
ASP.NET Core MVC (.NET 8)
Entity Framework Core - Code First
ASP.NET Core Identity
AutoMapper
MS SQL Server
Razor View Engine

Mimari=
ITHelpDesk/
├── Entity/         # Varlık sınıfları (Ticket, Category, AppUser, AppRole)
├── DataAccess/     # EF Core context, repository ve migration'lar
├── Business/       # Servis katmanı, generic arayüzler
├── Dto/            # Veri transfer nesneleri (TicketCreateDto, TicketListDto vb.)
└── WebApplication/ # Controller'lar, View'lar, Program.cs

Öne Çıkan Özellikler
Cookie tabanlı kimlik doğrulama ve [Authorize] attribute'larıyla rol bazlı yetkilendirme
IGenericService<T> generic arayüzü üzerinden soyutlanmış CRUD operasyonları
AutoMapper ile merkezi entity-DTO dönüşümleri
Startup sırasında RoleManager aracılığıyla seed data ile rol oluşturma
Ticket entity'sinde Category ile foreign key ilişkisi ve navigation property kullanımı
