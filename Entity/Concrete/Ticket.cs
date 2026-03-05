using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // Düşük, Orta, Yüksek  (Seviyelendirme!)
        public string Status { get; set; }   // Açık, İşlemde, Çözüldü

        // --- İLİŞKİ BÖLÜMÜ ---

        // Hangi kategoriye ait? (ForeignKey)
        public int CategoryId { get; set; }

        // Navigation Property: Kod tarafında kategori detayına ulaşmak için
        public Category Category { get; set; }

        // Talebi açan kullanıcı (Şimdilik Identity kurmadığımız için string tutalım)
        public int AppUserId { get; set; }

        // Bu talebi açan kullanıcının kendisi (Navigation Property)
        public AppUser AppUser { get; set; }
    }
}
