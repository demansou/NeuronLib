using System;
using System.Collections.Generic;
using System.Linq;
using NeuronLib.Classes;
using NeuronLib.Functions;
using NeuronLib.Factories;

namespace SimpleNeuralNetwork
{
    public class SimpleNeuralNetwork
    {
        NeuralLayerFactory _layerFactory;

        internal List<NeuralLayer> _layers;
        internal double _learningRate;
        internal double[][] _expectedResult;

        public SimpleNeuralNetwork(int numberOfInputNeurons)
        {
            _layers = new List<NeuralLayer>();
            _layerFactory = new NeuralLayerFactory();

            CreateInputLayer(numberOfInputNeurons);

            _learningRate = 2.95;
        }

        public void AddLayer(NeuralLayer newLayer)
        {
            if (_layers.Any())
            {
                newLayer.ConnectLayers(_layers.Last());
            }

            _layers.Add(newLayer);
        }

        public void PushInputValues(double[] inputs)
        {
            _layers
                .First().Neurons.ForEach(x => x.PushValueOnInput(
                    inputs[_layers.First().Neurons.IndexOf(x)]));
        }

        public void PushExpectedValues(double[][] expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        public List<double> GetOutput()
        {
            var returnValue = new List<double>();

            _layers.Last().Neurons.ForEach(neuron =>
            {
                returnValue.Add(neuron.CalculateOutput());
            });

            return returnValue;
        }

        public void Train(double[][] inputs, int numberOfEpochs)
        {
            double totalError = 0;

            for (var i = 0; i < numberOfEpochs; ++i)
            {
                for (var j = 0; j < inputs.GetLength(0); ++j)
                {
                    PushInputValues(inputs[j]);

                    var outputs = new List<double>();

                    _layers.Last().Neurons.ForEach(x =>
                    {
                        outputs.Add(x.CalculateOutput());
                    });

                    totalError = CalculateTotalError(outputs, j);
                    HandleOutputLayer(j);
                    HandleHiddenLayers();
                }
            }
        }

        void CreateInputLayer(int numberOfInputNeurons)
        {
            var inputLayer = _layerFactory.CreateNeuralLayer(
                numberOfInputNeurons,
                new RectifiedActivationFunction(),
                new WeightedSumFunction());

            inputLayer.Neurons.ForEach(x => x.AddInputSynapse(0));
            AddLayer(inputLayer);
        }

        double CalculateTotalError(List<double> outputs, int row)
        {
            double totalError = 0;

            outputs.ForEach(output =>
            {
                totalError += Math.Pow(
                    output - _expectedResult[row][outputs.IndexOf(output)], 2);
            });

            return totalError;
        }

        void HandleOutputLayer(int row)
        {
            _layers.Last().Neurons.ForEach(neuron =>
            {
                neuron.Inputs.ForEach(connection =>
                {
                    var output = neuron.CalculateOutput();
                    var netInput = connection.GetOutput();

                    var expectedOutput = _expectedResult[row][_layers.Last().Neurons.IndexOf(neuron)];

                    var nodeDelta = (expectedOutput - output) * output * (1 - output);
                    var delta = -1 * netInput * nodeDelta;

                    connection.UpdateWeight(_learningRate, delta);

                    neuron.PreviousPartialDerivative = nodeDelta;
                });
            });
        }

        void HandleHiddenLayers()
        {
            for (var i = _layers.Count - 2; i > 0; --i)
            {
                _layers[i].Neurons.ForEach(neuron =>
                {
                    neuron.Inputs.ForEach(connection =>
                    {
                        var output = neuron.CalculateOutput();
                        var netInput = connection.GetOutput();
                        double sumPartial = 0;

                        _layers[i + 1].Neurons.ForEach(outputNeuron =>
                        {
                            outputNeuron.Inputs.Where(x => x.IsFromNeuron(neuron.Id))
                                        .ToList()
                                        .ForEach(outConnection =>
                            {
                                sumPartial += outConnection.PreviousWeight * outputNeuron.PreviousPartialDerivative;
                            });
                        });

                        var delta = -1 * netInput * sumPartial * output * (1 - output);
                        connection.UpdateWeight(_learningRate, delta);
                    });
                });
            }
        }
    }
}
