using org.jenetics;
using org.jenetics.engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman.ConsoleUI
{
    class Program
    {
        /// <summary>
        /// Maximum individual age.
        /// </summary>
        private static readonly int MAXAGE = 11;

        /// <summary>
        /// Population size.
        /// </summary>
        private static readonly int POPULATIONSIZE = 500;

        /// <summary>
        /// Mutation probability.
        /// </summary>
        private static readonly double MUTATION = 0.2;

        /// <summary>
        /// Crossover probability.
        /// </summary>
        private static readonly double CROSSOVER = 0.35;

        private static readonly int STEADYLIMIT = 200;

        /// <summary>
        /// Generation limit.
        /// </summary>
        private static readonly int LIMIT = 1000;

        static void Main(string[] args)
        {
            var engine = Engine.builder(new FitnessFunction(), PermutationChromosome.ofInteger(TravelingSalesman.STOPS))
                .optimize(Optimize.MINIMUM)
                .maximalPhenotypeAge(MAXAGE)
                .populationSize(POPULATIONSIZE)
                .alterers(new SwapMutator(MUTATION), new PartiallyMatchedCrossover(CROSSOVER))
                .build();

            var statistics = EvolutionStatistics.ofNumber();

            var best = engine.stream()
                .limit(limit.byFixedGeneration(STEADYLIMIT))
                .limit(LIMIT)
                .peek(statistics)
                .collect(EvolutionResult.toBestPhenotype());

            Console.WriteLine(statistics);
            Console.WriteLine(best);
        }
    }
}
