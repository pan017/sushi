using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication38.Models
{
    public class Cart
    {
        public List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Sushi product, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Product.SushiId == product.SushiId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Sushi product)
        {
            lineCollection.RemoveAll(l => l.Product.SushiId == product.SushiId);
        }

        public double ComputeTotalValue()
        {
            double q = lineCollection.Sum(e => e.Product.Price * e.Quantity);
            return q;

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
    public class CartLine
    {
        public Sushi Product { get; set; }
        public int Quantity { get; set; }
    }
}