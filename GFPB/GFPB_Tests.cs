﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GFPB
{
    [TestFixture]
    class GFPB_Tests
    {
        [Test]
        [Description("Verifies that Generator polynomial set correctly.")]
        [TestCase(293, 11, 6, 1, 0, "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100001000011")]
        [TestCase(163, 7, 6, 3, 0, "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000011001001")]
        [TestCase(409, 15, 6, 1, 0, "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000001000011")]
        [TestCase(571, 10, 5, 2, 0, "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000100101")]
        public void GeneratorTest(int m, int deg2, int deg3, int deg4, int deg5, string expected)
        {
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            Assert.AreEqual(expected, polynomial.ToString());
        }

        /*
        [Test]
        [Description("Verifies that using XOR function we can find zero constant.")]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void XORTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            Polynomial result = new Polynomial(m);
            Operation operation = new Operation();
            result.array = operation.XOR(polynomial.array, polynomial.array);
            ulong[] zero = new ulong[1];
            Assert.AreEqual(0, operation.LongCmp(result.array, zero));
        }
        */


        [Test]
        [Description("Verifies that NOT function does not modify the argument passed.")]
        [TestCase(293ul, 4ul, 34ul, 234ul)]
        public void ImmutabilityNOTTest(ulong a, ulong b, ulong c, ulong d)
        {
            Operation operation = new Operation();
            ulong[] arr = new ulong[4] {a, b, c, d };
            ulong[] test_arr = new ulong[4] {a, b, c, d};
            var result = operation.NOT(test_arr);
            CollectionAssert.AreEqual(arr, test_arr);
        }
    }
}
