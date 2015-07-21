using java.util.function;
using org.jenetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack.ConsoleUI
{
    /// <summary>
    /// The knapsack fitness function class, which is parametrized with
    /// the available items and the size of the knapsack.
    /// </summary>
    public sealed class KnapsackFunction : Function
    {
        private Item[] Items { get; set; }
        private java.lang.Double Size { get; set; }

        public KnapsackFunction(Item[] items, double size)
        {
            this.Items = items;
            this.Size = java.lang.Double.valueOf(size);
        }

        public Function andThen(Function after)
        {
            throw new NotImplementedException();
        }

        public object apply(object t)
        {
            var gt = (Genotype)t;

            var collector = Item.toSum();

            Item sum = (Item)((BitChromosome)gt.getChromosome()).ones()
                .mapToObj(new IntFunctionImpl(this.Items))
                .collect(collector);

            return sum.Size.doubleValue() <= this.Size.doubleValue() ? sum.Value : java.lang.Double.valueOf(0);
        }

        public Function compose(Function before)
        {
            throw new NotImplementedException();
        }

        public Function identity()
        {
            throw new NotImplementedException();
        }

        #region functional

        private class IntFunctionImpl : IntFunction
        {
            private Item[] Items { get; set; }

            public IntFunctionImpl(Item[] items)
            {
                this.Items = items;
            }

            public object apply(int value)
            {
                var i = (int)value;
                var item = this.Items[i];

                return item;
            }
        }


        #endregion
    }
}
