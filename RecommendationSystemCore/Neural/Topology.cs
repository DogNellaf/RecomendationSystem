using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationSystem.Neural
{
    public class Topology: ICloneable
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }
        public Topology(int inputCount, int outputCount, params int[] layers)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }

        public object Clone()
        {
            int[] list = new int[HiddenLayers.Count];
            HiddenLayers.CopyTo(list);
            return new Topology(InputCount, OutputCount, list);
        }
    }
}
