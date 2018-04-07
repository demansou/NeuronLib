using System.Collections.Generic;
using System.Linq;
using NeuronLib.Interfaces;

namespace NeuronLib.Classes
{
    public class NeuralLayer
    {
        public List<INeuron> Neurons;

        public NeuralLayer()
        {
            Neurons = new List<INeuron>();
        }

        public void ConnectLayers(NeuralLayer inputLayer)
        {
            Neurons
                .SelectMany(neuron => inputLayer.Neurons, 
                            (neuron, input) => new { neuron, input })
                .ToList()
                .ForEach(x => x.neuron.AddInputNeuron(x.input));
        }
    }
}
