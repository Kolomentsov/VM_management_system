using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CHW2.Context;
using CHW2.Models;

namespace CHW2.Assets
{
    public class Helper
    {
        public void Init()
        {
            using (var context = new DataContext())
            {
                var cashes = context.Cashes.ToArray();
                if (!cashes.Any())
                {
                    var cashesArray = new[]
                    {
                        new Cash(1, true),
                        new Cash(2, true),
                        new Cash(5, true),
                        new Cash(10, true),
                        new Cash(50, false),
                        new Cash(100, false),
                        new Cash(500, false),
                        new Cash(1000, false)
                    };

                    foreach (var cash in cashesArray)
                    {
                        context.Cashes.Add(cash);
                        context.SaveChanges();
                    }
                }
            }
            using (var context = new DataContext())
            {
                var products = context.Products.ToArray();
                if (!products.Any())
                {
                    var productsArray = new[]
                    {
                        new Product("Твикс", 15, 10),
                        new Product("Сникерс", 17, 12),
                        new Product("Кока-кола", 20, 13),
                        new Product("Печенье", 10, 7),
                        new Product("Вода", 5, 3),
                        new Product("Орешки", 15, 15),
                        new Product("Шоколад", 12, 11),
                        new Product("Спрайт", 19, 18),
                        new Product("Сендвич", 30, 3),
                        new Product("Поп-корн", 21, 16)
                    };

                    foreach (var product in productsArray)
                    {
                        context.Products.Add(product);
                        context.SaveChanges();
                    }
                }
            }
            using (var context = new DataContext())
            {
                var terminals = context.Terminals.ToArray();
                if (!terminals.Any())
                {
                    var updatedProducts = context.Products.ToArray();
                    var items = updatedProducts.Select(updatedProduct => new Item(updatedProduct, 5)).ToList();

                    items[0].Amont = 0;

                    foreach (var item in items)
                    {
                        context.Items.Add(item);
                        context.SaveChanges();
                    }

                    items = context.Items.ToList();

                    var updatedCashes = context.Cashes.ToArray();

                    var monies = updatedCashes.Select(updatedCash => new Money(updatedCash, 20)).ToList();

                    foreach (var money in monies)
                    {
                        context.Monies.Add(money);
                        context.SaveChanges();
                    }

                    monies = context.Monies.ToList();

                    context.Terminals.Add(new Terminal
                    {
                        Id = Guid.Empty,
                        Items = items,
                        Location = "Москва",
                        Monies = monies
                    });
                    context.SaveChanges();
                }
            }
            using (var context = new DataContext())
            {
                var statistics = context.Statistics.Where(x => x.Date.Month == DateTime.Today.Month).ToArray();
                if (statistics.Any())
                    return;

                context.Statistics.Add(new Statistic
                {
                    Id = Guid.Empty,
                    Date = DateTime.Today,
                    Product = context.Products.FirstOrDefault(),
                    Terminal = context.Terminals.FirstOrDefault()
                });
                context.SaveChanges();
            }
        }

        public void UpdateTerminalAdress(Guid terminalId, string address)
        {
            using (var context = new DataContext())
            {
                var terminal = context.Terminals.FirstOrDefault(x => x.Id == terminalId);

                if (terminal == null)
                    return;

                terminal.Location = address;
                context.SaveChanges();
            }
        }

        public Terminal[] GetTerminalsWithEmptyItems()
        {
            using (var context = new DataContext())
            {
                return context.Terminals.Where(x => x.Items.Any(y => y.Amont == 0)).Include(x => x.Items.Select(y => y.Product)).Include(x => x.Monies.Select(y => y.Cash)).ToArray();
            }
        }

        public Terminal[] GetTerminals()
        {
            using (var context = new DataContext())
            {
                return context.Terminals.Include(x => x.Items.Select(y => y.Product)).Include(x => x.Monies.Select(y => y.Cash)).ToArray();
            }
        }

        public KeyValuePair<Terminal, decimal>[] GetTerminalsByProfit(DateTime date)
        {
            using (var context = new DataContext())
            {
                return context.Statistics.Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month)
                                         .Include(x => x.Product)
                                         .Include(x => x.Terminal)
                                         .ToArray()
                                         .GroupBy(x => x.Terminal)
                                         .ToDictionary(x => x.Key, x => x.Where(y => y.Terminal == x.Key)
                                                                         .Sum(z => z.Product.SellingPrice - z.Product.PurchasePrice))
                                         .OrderByDescending(x => x.Value).ToArray();
            }
        }

        public KeyValuePair<Product, int>[] GetLeastSoldProducts()
        {
            using (var context = new DataContext())
            {
                var stats = context.Statistics.Select(x => x.Product).Select(x => x.Id).ToArray();

                var products = context.Products.Select(x => x.Id).ToArray();

                var resultIds = products.Where(product => !stats.Contains(product)).Take(5).ToList();

                if (resultIds.Count == 5)
                    return GetProductsStats(GetProductsById(resultIds));

                resultIds.AddRange(stats.GroupBy(x => x)
                                        .ToDictionary(y => y.Key, y => y.Count())
                                        .OrderBy(x => x.Value)
                                        .Take(5 - resultIds.Count)
                                        .Select(x => x.Key));

                return GetProductsStats(GetProductsById(resultIds));
            }
        }

        private static Product[] GetProductsById(IEnumerable<Guid> productGuids)
        {
            using (var context = new DataContext())
            {
                return productGuids.Select(x => context.Products.FirstOrDefault(y => y.Id == x)).ToArray();
            }
            
        }

        private KeyValuePair<Product, int>[] GetProductsStats(IEnumerable<Product> products)
        {
            using (var context = new DataContext())
            {
                return products.Select(x => new KeyValuePair<Product, int>(x, context.Statistics.Count(y => y.Product.Id == x.Id))).ToArray();
            }
        }
    }
}
