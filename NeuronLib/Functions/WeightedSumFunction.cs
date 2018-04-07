using System.Linq;
using System.Collections.Generic;
using NeuronLib.Interfaces;

namespace NeuronLib.Functions
{
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(IList<ISynapse> inputs)
        {
            return inputs
                .Select(input => input.Weight * input.GetOutput())
                .Sum();
        }
    }
}
