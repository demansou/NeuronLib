using System;
using System.Collections.Generic;

namespace NeuronLib.Interfaces
{
    public interface INeuron
    {
        Guid Id { get; }
        double PreviousPartialDerivative { get; set; }

        List<ISynapse> Inputs { get; set; }
        List<ISynapse> Outputs { get; set; }

        void AddInputNeuron(INeuron inputNeuron);
        void AddOutputNeuron(INeuron outputNeuron);

        double CalculateOutput();

        void AddInputSynapse(double inputValue);
        void PushValueOnInput(double inputValue);
    }
}
