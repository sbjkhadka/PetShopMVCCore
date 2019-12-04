using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public class BasketItem
    {
        [Key]
        public int BasketItemId { get; set; }
        public int? ItemQuantity { get; set; }
        public int? ProductId { get; set; }
        public int? BasketId { get; set; }
        public Product Products { get; set; }
        public Basket Baskets { get; set; }
    }
}
