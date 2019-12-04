using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [DisplayName("Name")]
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }
    }
}
