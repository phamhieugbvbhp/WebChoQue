using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.ViewModels
{
    public class CategoryProductVM:ProductCategory
    {
        public string ParentName { get; set; }
    }
}
