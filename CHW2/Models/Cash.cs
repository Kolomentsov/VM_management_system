using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHW2.Models
{
    public class Cash
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Value { get; set; }

        public bool IsCoin { get; set; }

        public Cash()
        {
            Id = Guid.Empty;
            Value = 0;
            IsCoin = false;
        }

        public Cash(Guid id, int value, bool isCoin)
        {
            Id = id;
            Value = value;
            IsCoin = isCoin;
        }

        public Cash(int value, bool isCoin)
        {
            Id = Guid.Empty;
            Value = value;
            IsCoin = isCoin;
        }
    }
}
