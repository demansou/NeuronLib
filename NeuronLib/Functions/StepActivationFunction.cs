using System;
using NeuronLib.Interfaces;

namespace NeuronLib.Functions
{
    public class StepActivationFunction : IActivationFunction
    {
        double _threshold;

        public StepActivationFunction(double threshold)
        {
            _threshold = threshold;
        }

        public double CalculateOutput(double input)
        {
            return Convert.ToDouble(input > _threshold);
        }
    }
}
