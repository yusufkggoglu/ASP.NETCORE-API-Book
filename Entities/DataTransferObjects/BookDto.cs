using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record BookDto 
    { 
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
    }
}
