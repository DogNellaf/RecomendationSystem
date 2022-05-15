using RecommendationSystem.Neural;
using System.Collections.Generic;

namespace RecommendationSystem.Neural
{
    public class Layer
    {
        public List<Neuron> Neurons { get; }
        public int Count => Neurons?.Count ?? 0;

        public Layer(List<Neuron> neurons, NeuronType type = NeuronType.Hidden)
        {
            foreach (Neuron neuron in neurons)
            {
                if (neuron.NeuronType != type)
                {
                    throw new System.Exception("Попытка добавить нейрон неправильного типа");
                }
            }

            Neurons = neurons; 
        }

        public List<double> GetSignals()
        {
            var result = new List<double>();
            foreach (Neuron neuron in Neurons)
            {
                result.Add(neuron.Output);
            }
            return result;
        }
    }
}
