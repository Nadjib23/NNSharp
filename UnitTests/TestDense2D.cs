﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNSharp.DataTypes;
using static NNSharp.DataTypes.Data2D;
using NNSharp.SequentialBased.SequentialLayers;
using NNSharp.IO;
using NNSharp.Models;

namespace UnitTests
{
    [TestClass]
    public class TestDense2D
    {
        [TestMethod]
        public void Test_Dense2D_Execute()
        {
            // Initialize data.
            Data2D data = new Data2D(1, 1, 18, 1);
            data[0, 0, 0, 0] = 1;
            data[0, 0, 1, 0] = 2;
            data[0, 0, 2, 0] = 0;

            data[0, 0, 3, 0] = 3;
            data[0, 0, 4, 0] = 4;
            data[0, 0, 5, 0] = 0;

            data[0, 0, 6, 0] = 2;
            data[0, 0, 7, 0] = 2;
            data[0, 0, 8, 0] = 0;


            data[0, 0, 9, 0] = 0;
            data[0, 0, 10, 0] = 3;
            data[0, 0, 11, 0] = 1;

            data[0, 0, 12, 0] = 1;
            data[0, 0, 13, 0] = 1;
            data[0, 0, 14, 0] = 1;

            data[0, 0, 15, 0] = 3;
            data[0, 0, 16, 0] = 1;
            data[0, 0, 17, 0] = 0;

            // Initialize weights.
            Data2D weights = new Data2D(1, 1, 18, 2);
            weights[0, 0, 0, 0] = 1;
            weights[0, 0, 1, 0] = 2;
            weights[0, 0, 2, 0] = 2;

            weights[0, 0, 3, 0] = 3;
            weights[0, 0, 4, 0] = 1;
            weights[0, 0, 5, 0] = 1;

            weights[0, 0, 6, 0] = 1;
            weights[0, 0, 7, 0] = 3;
            weights[0, 0, 8, 0] = 2;


            weights[0, 0, 9, 0] = 0;
            weights[0, 0, 10, 0] = 1;
            weights[0, 0, 11, 0] = 1;

            weights[0, 0, 12, 0] = 2;
            weights[0, 0, 13, 0] = 3;
            weights[0, 0, 14, 0] = 0;

            weights[0, 0, 15, 0] = 2;
            weights[0, 0, 16, 0] = 3;
            weights[0, 0, 17, 0] = 2;



            weights[0, 0, 0, 1] = 5;
            weights[0, 0, 1, 1] = 2;
            weights[0, 0, 2, 1] = 1;

            weights[0, 0, 3, 1] = 3;
            weights[0, 0, 4, 1] = 8;
            weights[0, 0, 5, 1] = 1;

            weights[0, 0, 6, 1] = 1;
            weights[0, 0, 7, 1] = 0;
            weights[0, 0, 8, 1] = 2;


            weights[0, 0, 9, 1] = 0;
            weights[0, 0, 10, 1] = 1;
            weights[0, 0, 11, 1] = 0;

            weights[0, 0, 12, 1] = 2;
            weights[0, 0, 13, 1] = 4;
            weights[0, 0, 14, 1] = 0;

            weights[0, 0, 15, 1] = 2;
            weights[0, 0, 16, 1] = 3;
            weights[0, 0, 17, 1] = 2;

            Dense2DLayer dens = new Dense2DLayer(2);
            dens.SetWeights(weights);
            dens.SetInput(data);
            dens.Execute();
            Data2D output = dens.GetOutput() as Data2D;

            // Checking sizes
            Dimension dim = output.GetDimension();
            Assert.AreEqual(dim.b, 1);
            Assert.AreEqual(dim.c, 2);

            // Checking calculation
            Assert.AreEqual(output[0, 0, 0, 0], 44.0, 0.0000001);
            Assert.AreEqual(output[0, 0, 1, 0], 70.0, 0.0000001);

        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Test_Dense2D_Null_Input()
        {
            Data2D data = null;
            Data2D weights = new Data2D(3, 3, 3, 3);
            Dense2DLayer dens = new Dense2DLayer(3);
            dens.SetWeights(weights);
            dens.SetInput(data);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Test_Dense2D_Null_Weights()
        {
            Data2D weights = null;
            Dense2DLayer dens = new Dense2DLayer(0);
            dens.SetWeights(weights);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Test_Dense2D_DifferentData_Input()
        {
            DataArray data = new DataArray(5);
            Data2D weights = new Data2D(3, 3, 3, 3);
            Dense2DLayer dens = new Dense2DLayer(3);
            dens.SetWeights(weights);
            dens.SetInput(data);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Test_Dense2D_DifferentData_Weights()
        {
            DataArray weights = new DataArray(5);
            Dense2DLayer dens = new Dense2DLayer(1);
            dens.SetWeights(weights);
        }

        [TestMethod]
        public void Test_Dense2D_KerasModel()
        {
            string path = @"tests\test_dense_model.json";
            var reader = new ReaderKerasModel(path);

            SequentialModel model = reader.GetSequentialExecutor();

            Data2D inp = new Data2D(1, 8, 1, 1);

            inp[0, 0, 0, 0] = 1;
            inp[0, 1, 0, 0] = 2;
            inp[0, 2, 0, 0] = -1;
            inp[0, 3, 0, 0] = 0;

            inp[0, 4, 0, 0] = 3;
            inp[0, 5, 0, 0] = 1;
            inp[0, 6, 0, 0] = 1;
            inp[0, 7, 0, 0] = 2;

            Data2D ou = model.ExecuteNetwork(inp) as Data2D;

            Assert.AreEqual(ou.GetDimension().c, 4);
            Assert.AreEqual(ou.GetDimension().w, 1);

            Assert.AreEqual(ou[0, 0, 0, 0], -1.0, 0.00001);
            Assert.AreEqual(ou[0, 0, 1, 0], 5.5, 0.00001);
            Assert.AreEqual(ou[0, 0, 2, 0], 18.5, 0.00001);
            Assert.AreEqual(ou[0, 0, 3, 0], 9.0, 0.00001);
        }
    }
}
