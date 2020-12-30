﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models
{
    [Table("CategoryProduct")]
    public class ProductCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
    }
}
