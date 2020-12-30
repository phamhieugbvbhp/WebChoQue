using ChoQueVN.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.ViewModels
{
    public class BillsVM:Bill
    {
        public string NameProduct { get; set; }
    }
}
