using java.util.function;
using org.jenetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealFunction.ConsoleUI
{
    public sealed class RealFunction : Function
    {
        // This method calculates the fitness for a given genotype.
        private static java.lang.Double eval(Genotype gt)
        {
            double x = ((DoubleGene)gt.getGene()).doubleValue();

            double y = Math.Cos(0.5 + Math.Sin(x)) * Math.Cos(x);

            return new java.lang.Double(y);
        }

        public Function andThen(Function after)
        {
            throw new NotImplementedException();
        }

        public object apply(object t)
        {
            Genotype gt = (Genotype)t;

            var y = eval(gt);

            return y;
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
