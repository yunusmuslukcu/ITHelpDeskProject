using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.TicketDtos
{
    public class TicketListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; } // Entity'de olmayan ama tabloda göstereceğimiz alan!
    }
}
