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
        public List<double> Weight { get; }

        // тип нейрона
        public NeuronType NeuronType { get; }

        // выходные данные
        public double Output { get; private set; }

        public Neuron(int inputCount, NeuronType type = NeuronType.Hidden)
        {

        }
    }
}
