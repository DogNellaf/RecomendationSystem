using RecommendationSystem.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem.Neural
{
    public class NeuronNetwork
    {
        public Topology Topology { get; }
        public List<Layer> Layers { get; }
        public NeuronNetwork(Topology topology)
        {
            Topology = topology;

            Layers = new List<Layer>();

            CreateInputLayer();
            CreateHiddenLayer();
            CreateOutputLayer();

            
        }

        public Neuron FeedForward(List<double> inputSignals)
        {
            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayers();

            if (Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];
            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(x => x.Output).First();
            }
        }

        private void FeedForwardAllLayers()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var previousLayerSignals = Layers[i - 1].GetSignals();
                var layer = Layers[i];

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSignals);
                }
            }
        }

        private void SendSignalsToInputNeurons(List<double> inputSignals)
        {
            for (int i = 0; i < inputSignals.Count; i++)
            {
                var signal = new List<double> { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];
                neuron.FeedForward(signal);
            }
        }

        private void CreateOutputLayer()
        {
            var neurons = new List<Neuron>();
            var lastLayer = Layers.Last();
            for (int i = 0; i < Topology.InputCount; i++)
            {
                var neuron = new Neuron(lastLayer.Count, NeuronType.Input);
                neurons.Add(neuron);
            }
            var outputLayer = new Layer(neurons, NeuronType.Input);
            Layers.Add(outputLayer);
        }

        private void CreateHiddenLayer()
        {
            for (int j = 0; j < Topology.HiddenLayers.Count; j++)
            {
                var neurons = new List<Neuron>();
                var lastLayer = Layers.Last();
                for (int i = 0; i < Topology.HiddenLayers[j]; i++)
                { 
                    var neuron = new Neuron(lastLayer.Count);
                    neurons.Add(neuron);
                }
                var outputLayer = new Layer(neurons);
                Layers.Add(outputLayer);
            }
        }

        private void CreateInputLayer()
        {
            var neurons = new List<Neuron>();
            for (int i = 0; i < Topology.InputCount; i++)
            {
                var neuron = new Neuron(1, NeuronType.Input);
                neurons.Add(neuron);
            }
            var layer = new Layer(neurons, NeuronType.Input);
            Layers.Add(layer);
        }
    }
}
