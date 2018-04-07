using System;
using NeuronLib.Interfaces;

namespace NeuronLib.Functions
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        double _coefficient;

        public SigmoidActivationFunction(double coefficient)
        {
            _coefficient = coefficient;
        }

        public double CalculateOutput(double input)
        {
            return (1 / (1 + Math.Exp(-input * _coefficient)));
        }
    }
}
