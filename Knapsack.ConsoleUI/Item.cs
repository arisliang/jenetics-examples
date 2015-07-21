using java.util.function;
using java.util.stream;
using org.jenetics.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack.ConsoleUI
{
    public sealed class Item
    {
        public java.lang.Double Size { get; set; }

        public java.lang.Double Value { get; set; }

        public Item(double size, double value)
        {
            this.Size = java.lang.Double.valueOf(size);
            this.Value = java.lang.Double.valueOf(value);
        }

        /// <summary>
        /// Create a new random knapsack item.
        /// </summary>
        /// <returns></returns>
        public static Item random()
        {
            var r = RandomRegistry.getRandom();

            var item = new Item(r.nextDouble() * 100, r.nextDouble() * 100);

            return item;
        }

        /// <summary>
        /// Create a new collector for summing up the knapsack items.
        /// </summary>
        /// <returns></returns>
        public static Collector toSum()
        {
            var collector = Collector.__Methods.of(new SupplierImpl(), new BiConsumerImpl(),
                new BinaryOperatorImp(), new FunctionImp(),
                new Collector.Characteristics[2] { Collector.Characteristics.UNORDERED,
                    Collector.Characteristics.UNORDERED });

            return collector;
        }

        #region nested classes

        /// <summary>
        /// Provides initial seed item.
        /// </summary>
        private class SupplierImpl : Supplier
        {
            public object get()
            {
                var seed = new double[2] { 0, 0 };

                return seed;
            }
        }

        /// <summary>
        /// Stores size and value in a plain array.
        /// </summary>
        private class BiConsumerImpl : BiConsumer
        {
            public void accept(object t, object u)
            {
                var a_t = (double[])t;
                var item_u = (Item)u;

                a_t[0] = a_t[0] + item_u.Size.doubleValue();
                a_t[1] = a_t[1] + item_u.Value.doubleValue();
            }

            public BiConsumer andThen(BiConsumer after)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Accumulates sizes and values.
        /// </summary>
        public class BinaryOperatorImp : BinaryOperator
        {
            public BinaryOperator maxBy(java.util.Comparator comparator)
            {
                throw new NotImplementedException();
            }

            public BinaryOperator minBy(java.util.Comparator comparator)
            {
                throw new NotImplementedException();
            }

            public BiFunction andThen(Function after)
            {
                throw new NotImplementedException();
            }

            public object apply(object t, object u)
            {
                var a_t = (double[])t;
                var a_u = (double[])u;

                a_t[0] += a_u[0];
                a_t[1] += a_u[1];

                return a_t;
            }
        }

        public class FunctionImp : Function
        {
            public Function andThen(Function after)
            {
                throw new NotImplementedException();
            }

            public object apply(object t)
            {
                var a_t = (double[])t;
                var item = new Item(a_t[0], a_t[1]);

                return item;
            }

            public Function compose(Function before)
            {
                throw new NotImplementedException();
            }

            public Function identity()
            {
                throw new NotImplementedException();
            }
        }


        #endregion
    }



}
