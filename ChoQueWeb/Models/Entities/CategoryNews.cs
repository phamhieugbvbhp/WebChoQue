using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.Entities
{
    [Table("CategoryNews")]
    public class CategoryNews
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
    }
}
