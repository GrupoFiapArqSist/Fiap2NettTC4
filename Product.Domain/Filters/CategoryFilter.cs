using ComandaPro.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Filters
{
    public class CategoryFilter : _BaseFilter
    {
        public string Name { get; set; }
    }
}
