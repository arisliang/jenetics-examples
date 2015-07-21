using java.util.function;
using java.util.stream;
using org.jenetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman.ConsoleUI
{
    public class TravelingSalesman
    {
        // Problem initialization:
        // Calculating the adjacence matrix of the "city" distances.
        public static readonly int STOPS = 20;
        private static double[,] ADJACENCE = matrix(STOPS);

        private static double[,] matrix(int stops)
        {
            double radius = 10;
            var matrix = new double[stops, stops];

            for (int i = 0; i < stops; i++)
            {
                for (int j = 0; j < stops; j++)
                {
                    matrix[i, j] = chord(stops, Math.Abs(i - j), radius);
                }
            }

            return matrix;
        }

        private static double chord(int stops, int i, double radius)
        {
            return 2.0 * radius * Math.Abs(Math.Sin((Math.PI * i) / stops));
        }

        // Calculate the path length of hte current genotype.
        public static double dist(Genotype gt)
        {
            // Convert the genotype to the traveling path
            int[] path = gt.getChromosome().toSeq().stream()
                .mapToInt(new ToIntFunctionImpl())
                .toArray();

            // Calculate the path distance.
            var dist = IntStream.__Methods.range(0, STOPS)
                .mapToDouble(new IntToDoubleFunctionImpl(path))
                .sum();

            return dist;
        }

        #region functional

        private class ToIntFunctionImpl : ToIntFunction
        {
            public int applyAsInt(object value)
            {
                var result = (java.lang.Integer)((EnumGene)value).getAllele();

                return result.intValue();
            }
        }

        private class IntToDoubleFunctionImpl : IntToDoubleFunction
        {
            private readonly int[] Path = null;

            public IntToDoubleFunctionImpl(int[] path)
            {
                this.Path = path;
            }

            public double applyAsDouble(int i)
            {
                double dist = ADJACENCE[Path[i], Path[(i + 1) % STOPS]];

                return dist;
            }
        }



        #endregion
    }
}
