using System;
using NeuronLib.Interfaces;

namespace NeuronLib.Classes
{
    public class InputSynapse : ISynapse
    {
        internal INeuron _toNeuron;

        public double Weight { get; set; }
        public double PreviousWeight { get; set; }
        public double Output { get; set; }

        public InputSynapse(INeuron toNeuron)
        {
            _toNeuron = toNeuron;
            Weight = 1;
        }

        public InputSynapse(INeuron toNueron, double output)
        {
            _toNeuron = toNueron;
            Output = output;

            Weight = 1;
            PreviousWeight = 1;
        }

        public double GetOutput()
        {
            return Output;
        }

        public bool IsFromNeuron(Guid fromNeuronId)
        {
            return false;
        }

        public void UpdateWeight(double learningRate, double delta)
        {
            throw new InvalidOperationException("UpdateWeight method not allowed for InputSynapse.");
        }
    }
}
