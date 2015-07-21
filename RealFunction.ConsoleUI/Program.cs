using org.jenetics;
using org.jenetics.engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealFunction.ConsoleUI
{
    class Program
    {
        /// <summary>
        /// Population size.
        /// </summary>
        private static readonly int POPULATION = 500;

        /// <summary>
        /// Mutation probability.
        /// </summary>
        private static readonly double MUTATION = 0.03;

        /// <summary>
        /// Crossover probability.
        /// https://github.com/jenetics/jenetics/blob/master/org.jenetics/src/main/java/org/jenetics/MeanAlterer.java
        /// </summary>
        private static readonly double CROSSOVER = 0.6;

        /// <summary>
        /// http://jenetics.io/javadoc/org.jenetics/org/jenetics/engine/limit.html#bySteadyFitness-int-
        /// </summary>
        private static readonly int STEADYLIMIT = 7;

        /// <summary>
        /// http://jenetics.io/javadoc/org.jenetics/org/jenetics/engine/EvolutionStream.html#limit-java.util.function.Predicate-
        /// </summary>
        private static readonly int LIMIT = 100;

        static void Main(string[] args)
        {
            // Configure and build the evolution engine.
            var engine = Engine.builder(new RealFunction(), DoubleChromosome.of(0, 2.0 * Math.PI))
                .populationSize(POPULATION)
                .optimize(Optimize.MINIMUM)
                .alterers(new Mutator(MUTATION), new MeanAlterer(CROSSOVER))
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
