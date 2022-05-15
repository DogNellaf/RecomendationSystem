using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem.Neural
{
    // класс, описывающий один нейрон сети
    public class Neuron
    {
        // веса, входящие в нейрон
        public List<double> Weights { get; }

        // тип нейрона
        public NeuronType NeuronType { get; }

        // выходные данные
        public double Output { get; private set; }

        public Neuron(int inputCount, NeuronType type = NeuronType.Hidden)
        {
            NeuronType = type;
            Weights = new List<double>(inputCount);

            for (int i = 0; i < inputCount; i++)
            {
                Weights.Add(1);
            }
        }

        private double Sigma(double x) => 1 / (1 - Math.Pow(Math.E, -x));

        public double FeedForward(List<double> inputs)
        {
            double sum = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }
            Output = Sigma(sum);
            return Output;
        }

        public void SetWeights(params double[] weights)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                Weights[i] = weights[i];
            }
        }

        public override string ToString() => $"{Output}";
    }
}
