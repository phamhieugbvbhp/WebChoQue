using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.Entities
{
    [Table("Bills")]
    public class Bill
    {
        public int ID { get; set; }
        public string NameCus { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int ProductID { get; set; }
    }
}
