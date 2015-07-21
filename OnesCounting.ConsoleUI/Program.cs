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
        static void Main(string[] args)
        {
            // Configure and build the evolution engine.
            var engine = Engine.builder(new OnesCountingFunction(), BitChromosome.of(20, 0.15))
                .populationSize(500)
                .selector(new RouletteWheelSelector())
                .alterers(new Mutator(0.55), new SinglePointCrossover(0.06))
                .build();

            // Create evoluation statistics consumer
            var statistics = EvolutionStatistics.ofNumber();

            var evolution = engine.stream()
                // Truncate the evolution stream after 7 "steady" generations
                .limit(limit.bySteadyFitness(7))
                // The evolution will stop after maximal 100 generations
                .limit(100)
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
