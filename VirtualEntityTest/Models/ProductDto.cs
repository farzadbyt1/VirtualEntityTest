using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEntityTest.Models
{
    public class ProductDto
    {
        public string name { get; set; }
        public Guid id { get; set; }
        public string image { get; set; }
        public bool? publish { get; set; }
        public Guid? priceListId { get; set; }
    }
}
