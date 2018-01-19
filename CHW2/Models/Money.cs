using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHW2.Models
{
    public class Money
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Cash Cash { get; set; }

        public int Amount { get; set; }

        public Money()
        {
            Id = Guid.Empty;
            Cash = null;
            Amount = 0;
        }

        public Money(Cash cash, int amount)
        {
            Id = Guid.Empty;
            Cash = cash;
            Amount = amount;
        }
    }
}
