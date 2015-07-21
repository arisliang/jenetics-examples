using java.util.function;
using org.jenetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman.ConsoleUI
{
    public sealed class FitnessFunction : Function
    {
        public Function andThen(Function after)
        {
            throw new NotImplementedException();
        }

        public object apply(object t)
        {
            var gt = (Genotype)t;

            var fitness = TravelingSalesman.dist(gt);

            return java.lang.Double.valueOf(fitness);
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
