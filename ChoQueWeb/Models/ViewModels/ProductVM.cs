using ChoQueVN.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.ViewModels
{
    public class ProductVM:Product
    {
        public string CategoryName { get; set; }
    }
}
