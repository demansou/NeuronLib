using System;
using NeuronLib.Interfaces;

namespace NeuronLib.Classes
{
    public class Synapse : ISynapse
    {
        internal INeuron _fromNeuron;
        internal INeuron _toNeuron;

        public double Weight { get; set; }
        public double PreviousWeight { get; set; }

        public Synapse(INeuron fromNeuron, INeuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;

            Weight = weight;
            PreviousWeight = 0;
        }

        public Synapse(INeuron fromNeuron, INeuron toNeuron)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;

            Weight = new Random().NextDouble();
            PreviousWeight = 0;
        }

        public double GetOutput()
        {
            return _fromNeuron.CalculateOutput();
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return _fromNeuron.Id.Equals(fromNeuronId);
        }

        public void UpdateWeight(double learningRate, double delta)
        {
            PreviousWeight = Weight;
            Weight += learningRate * delta;
        }
    }
}
