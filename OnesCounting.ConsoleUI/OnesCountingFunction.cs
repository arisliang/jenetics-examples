using java.lang;
using java.util.function;
using org.jenetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnesCounting.ConsoleUI
{
    public class OnesCountingFunction : Function
    {
        /// <summary>
        /// Calculates the fitness for a given genotype.
        /// </summary>
        /// <param name="gt">Genotype of BitGene type in Java example</param>
        /// <returns></returns>
        private static Integer count(Genotype gt)
        {
            return new Integer(((BitChromosome)(gt.getChromosome())).bitCount());
        }

        public Function andThen(Function after)
        {
            throw new NotImplementedException();
        }

        public object apply(object t)
        {
            Genotype gt = (Genotype)t;
            Integer c = count(gt);
            return c;
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
}
