using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHW2.Models
{
    public class Terminal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<Money> Monies { get; set; }

        public Terminal()
        {
            Id = Guid.Empty;
            Location = string.Empty;
        }
    }
}