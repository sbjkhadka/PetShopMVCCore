using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public enum OrderPlaced
    {
        Yes, No
    }
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }
        public int? Quantity { get; set; }
        [DisplayName("Order Status")]
        public OrderPlaced? OrderPlaced { get; set; }
        [DisplayName("Sub-Total")]
        public decimal? SubTotal { get; set; }
        [DisplayName("Total")]
        public decimal? Total { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime? DateCreated { get; set; }
        [DisplayName("Customer Name")]
        public int? CustomerId { get; set; }
        public Customer Customers { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}
