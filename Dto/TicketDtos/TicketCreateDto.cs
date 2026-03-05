using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.TicketDtos
{
    public class TicketCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }

        public string Status { get; set; } = "Açık"; 

        public int CategoryId { get; set; }
        public string AppUserId { get; set; }
    }
}
