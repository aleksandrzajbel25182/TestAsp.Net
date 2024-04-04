using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDB.Entitys
{
    public class ProductEntitys
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title{ get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
          
    }
}
