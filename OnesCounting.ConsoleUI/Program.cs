using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.jenetics;
using org.jenetics.engine;
using java.lang;

namespace OnesCounting.ConsoleUI
{
    class Program
    {
        /// <summary>
        /// Population size.
        /// </summary>
        private static readonly int POPULATIONSIZE = 500;

        /// <summary>
        /// Mutation probability.
        /// </summary>
        private static readonly double MUTATION = 0.55;

        /// <summary>
        /// Crossover probability.
        /// </summary>
        private static readonly double CROSSOVER = 0.06;

        private static readonly int STEADYLIMIT = 7;

        /// <summary>
        /// Generation limit.
        /// </summary>
        private static readonly int LIMIT = 100;

        static void Main(string[] args)
        {
            // Configure and build the evolution engine.
            var engine = Engine.builder(new OnesCountingFunction(), BitChromosome.of(20, 0.15))
                .populationSize(POPULATIONSIZE)
                .selector(new RouletteWheelSelector())
                .alterers(new Mutator(MUTATION), new SinglePointCrossover(CROSSOVER))
                .build();

            // Create evoluation statistics consumer
            var statistics = EvolutionStatistics.ofNumber();

            var evolution = engine.stream()
                // Truncate the evolution stream after 7 "steady" generations
                .limit(limit.bySteadyFitness(STEADYLIMIT))
                // The evolution will stop after maximal 100 generations
                .limit(LIMIT)
                // Update the evaluation statistics after each generation
                .peek(statistics);

            var best = evolution
                // Collect (reduce) the evolution stream to its best phenotype.
                .collect(EvolutionResult.toBestGenotype());

            Console.WriteLine(statistics);
            Console.WriteLine(best);
        }
    }
}
