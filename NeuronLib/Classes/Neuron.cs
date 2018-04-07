﻿using System;
using System.Collections.Generic;
using System.Linq;

using NeuronLib.Interfaces;

namespace NeuronLib.Classes
{
    public class Neuron : INeuron
    {
        IActivationFunction _activationFunction;
        IInputFunction _inputFunction;

        public List<ISynapse> Inputs { get; set; }
        public List<ISynapse> Outputs { get; set; }

        public Guid Id { get; private set; }

        public double PreviousPartialDerivative { get; set; }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();
            Id = Guid.NewGuid();

            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
        }

        public void AddInputNeuron(INeuron inputNeuron)
        {
            var synapse = new Synapse(inputNeuron, this);
            Inputs.Add(synapse);
            inputNeuron.Outputs.Add(synapse);
        }

        public void AddOutputNeuron(INeuron outputNeuron)
        {
            var synapse = new Synapse(this, outputNeuron);
            Outputs.Add(synapse);
            outputNeuron.Inputs.Add(synapse);
        }

        public double CalculateOutput()
        {
            return _activationFunction.CalculateOutput(
                _inputFunction.CalculateInput(this.Inputs));
        }

        public void AddInputSynapse(double inputValue)
        {
            var inputSynapse = new InputSynapse(this, inputValue);
            Inputs.Add(inputSynapse);
        }

        public void PushValueOnInput(double inputValue)
        {
            ((InputSynapse) Inputs.First()).Output = inputValue;
        }
    }
}