using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEntityTest.Models
{
    public class PriceListDto
    {
        public string name { get; set; }
        public Guid id { get; set; }
        public DateTime toDate { get; set; }
        public DateTime fromDate { get; set; }
        public bool isActive { get; set; }
        public DateTime createdOn { get; set; }
    }
}
