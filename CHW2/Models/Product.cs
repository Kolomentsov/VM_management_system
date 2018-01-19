using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHW2.Models
{
    public class Product
    {
        private decimal _sellingPrice;
        private decimal _purchasePrice;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal SellingPrice
        {
            get { return _sellingPrice; }
            set
            {
                CheckPrices();

                _sellingPrice = value;
            }
        }

        public decimal PurchasePrice
        {
            get { return _purchasePrice; }
            set
            {
                CheckPrices();

                _purchasePrice = value;
            }
        }

        public Product()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            SellingPrice = decimal.Zero;
            PurchasePrice = decimal.Zero;
        }

        public Product(string name, decimal sellingPrice, decimal purchasePrice)
        {
            Id = Guid.Empty;
            Name = name;
            SellingPrice = sellingPrice;
            PurchasePrice = purchasePrice;
        }

        private void CheckPrices()
        {
            if (_purchasePrice > _sellingPrice)
                throw new InvalidOperationException("Цена продажи не может быть меньше цены закупки");
        }
    }
}
