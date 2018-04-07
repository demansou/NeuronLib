using System.Collections.Generic;

namespace NeuronLib.Interfaces
{
    public interface IInputFunction
    {
        double CalculateInput(IList<ISynapse> inputs);
    }
}
