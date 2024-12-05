using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEntityTest.Models
{
    public class BaseClass<T>
    {
        public int? totalRows { get; set; }
        public List<T> items { get; set; }
    }
}
