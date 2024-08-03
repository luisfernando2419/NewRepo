namespace Encadenamientoo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class ForwardChaining
    {
        // Representa una regla con una condición (premisa) y una acción (conclusión)
        class Rule
        {
            public List<string> Conditions { get; set; }
            public string Conclusion { get; set; }

            public Rule(List<string> conditions, string conclusion)
            {
                Conditions = conditions;
                Conclusion = conclusion;
            }
        }

        static void Main()
        {
            // Inicialización de las reglas
            List<Rule> rules = new List<Rule>
        {
            new Rule(new List<string> { "El cielo está nublado" }, "Va a llover"),
            new Rule(new List<string> { "Va a llover" }, "Llevar paraguas"),
            new Rule(new List<string> { "Va a llover", "El coche está afuera" }, "Cubrir el coche"),
            new Rule(new List<string> { "Está lloviendo" }, "El suelo está mojado"),
            new Rule(new List<string> { "El suelo está mojado" }, "Usar botas de lluvia")
        };

            // Ingreso de hechos por parte del usuario
            HashSet<string> facts = new HashSet<string>();
            while (true)
            {
                Console.Write("Ingrese un hecho (o escriba 'evaluar' para obtener el diagnóstico): ");
                string input = Console.ReadLine()?.Trim();

                if (input.Equals("evaluar", StringComparison.OrdinalIgnoreCase))
                {
                    // Proceso de encadenamiento hacia adelante
                    EvaluateFacts(facts, rules);

                    Console.Write("¿Desea ingresar más hechos? (s/n): ");
                    string continueInput = Console.ReadLine()?.Trim();
                    if (continueInput.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(input))
                {
                    facts.Add(input);
                }
            }
        }

        static void EvaluateFacts(HashSet<string> facts, List<Rule> rules)
        {
            bool newFactAdded;
            do
            {
                newFactAdded = false;
                foreach (var rule in rules)
                {
                    // Si todos los hechos de las condiciones están presentes, aplica la regla
                    if (rule.Conditions.All(condition => facts.Contains(condition)))
                    {
                        if (!facts.Contains(rule.Conclusion))
                        {
                            Console.WriteLine($"Aplicando regla: Si {string.Join(" y ", rule.Conditions)}, entonces {rule.Conclusion}");
                            facts.Add(rule.Conclusion);
                            newFactAdded = true;
                        }
                    }
                }
            } while (newFactAdded);

            // Resultados finales
            Console.WriteLine("\nConclusiones actuales:");
            foreach (var fact in facts)
            {
                Console.WriteLine(fact);
            }
        }
    }

}
