using java.util.function;
using java.util.stream;
using org.jenetics;
using org.jenetics.engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack.ConsoleUI
{
    class Program
    {
        private static readonly int NITEMS = 15;

        private static readonly double KSSIZE = NITEMS * 100 / 3;

        private static readonly double ONESPROBABILITY = 0.5;

        /// <summary>
        /// Population size.
        /// </summary>
        private static readonly int POPULATIONSIZE = 500;

        private static readonly int TOURNAMENTSAMPLESIZE = 5;

        /// <summary>
        /// Mutation probability.
        /// </summary>
        private static readonly double MUTATION = 0.115;

        /// <summary>
        /// Crossover probability.
        /// </summary>
        private static readonly double CROSSOVER = 0.16;

        private static readonly int STEADYLIMIT = 7;

        /// <summary>
        /// Generation limit.
        /// </summary>
        private static readonly int LIMIT = 100;

        static void Main(string[] args)
        {
            var ff = new FitnessFunction(
                Stream.__Methods.generate(new ItemSupplier()).limit(NITEMS).toArray().Cast<Item>().ToArray(), KSSIZE);

            // Configure and build the evolution engine.
            var engine = Engine.builder(ff, BitChromosome.of(NITEMS, ONESPROBABILITY))
                .populationSize(POPULATIONSIZE)
                .survivorsSelector(new TournamentSelector(TOURNAMENTSAMPLESIZE))
                .alterers(
                    new Mutator(MUTATION),
                    new SinglePointCrossover(CROSSOVER))
                .build();

            // Create evolution statistics consumer.
            var statistics = EvolutionStatistics.ofNumber();

            var best = engine.stream()
                .limit(limit.byFixedGeneration(STEADYLIMIT))
                .limit(LIMIT)
                .peek(statistics)
                .collect(EvolutionResult.toBestPhenotype());

            Console.WriteLine(statistics);
            Console.WriteLine(best);
        }

        private class ItemSupplier : Supplier
        {
            public object get()
            {
                var item = Item.random();

                return item;
            }
        }
    }
}
