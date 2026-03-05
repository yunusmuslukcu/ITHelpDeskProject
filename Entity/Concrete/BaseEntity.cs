using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }

        // Soft Delete: Veri silinmesin, sadece "silindi" işaretlensin.
        public bool IsDeleted { get; set; } = false;
    }
}
