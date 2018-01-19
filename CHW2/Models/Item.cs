using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHW2.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public int Amont { get; set; }

        public Item()
        {
            Id = Guid.Empty;
            Product = null;
            Amont = 0;
        }

        public Item(Product product, int amount)
        {
            Id = Guid.Empty;
            Product = product;
            Amont = amount;
        }
    }
}
