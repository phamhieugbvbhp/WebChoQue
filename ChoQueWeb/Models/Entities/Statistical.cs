using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChoQueVN.Models.Entities
{
    [Table("Statistical")]
    public class Statistical
    {
        public int ID { get; set; }
        public int Visit { get; set; }
    }
}
