using System;
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

        
        [Test]
        [Description("Verifies that constant '0' exist in current field.")]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void FindZeroTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            Polynomial result = new Polynomial(m);
            Operation operation = new Operation();
            result.array = operation.XOR(polynomial.array, polynomial.array);
            ulong[] zero = new ulong[1];
            Assert.AreEqual(0, operation.LongCmp(result.array, zero));
        }
        

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


        [Test]
        [Description("Verifies that constant '1' exist in current field.")]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void FindOneTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            var result = operation.MultiplicationModIrreducible(m, polynomial.array, alpha_start, polynomial.one);
            Assert.AreEqual(0 , operation.LongCmp(result, alpha_start));
        }


        [Test]
        [Description("Verifies that XOR function works correctly.")]
        [TestCase(163, 7, 6, 3, 0, "101011011")]
        [TestCase(409, 15, 6, 1, 0, "11000000011000101")]
        [TestCase(293, 11, 6, 1, 0, "1100011000101")]
        [TestCase(571, 10, 5, 2, 0, "110001101111")]
        public void XORTest(int m, int deg2, int deg3, int deg4, int deg5, string result)
        {
            ulong[] alpha = new ulong[] { 2 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            var alpha_next = operation.MultiplicationModIrreducible(m, polynomial.array, alpha_start, alpha);
            polynomial.array = operation.XOR(alpha_start, alpha_next);
            Assert.AreEqual(result, polynomial.ToString());
        }


        [Test]
        [Description("Verifies that  MultiplicationModIrreducible function works correctly.")]
        [TestCase(163, 7, 6, 3, 0, "110010010")]
        [TestCase(409, 15, 6, 1, 0, "10000000010000110")]
        [TestCase(293, 11, 6, 1, 0, "1000010000110")]
        [TestCase(571, 10, 5, 2, 0, "100001001010")]
        public void MultiplicationModIrreducibleTest(int m, int deg2, int deg3, int deg4, int deg5, string result)
        {
            ulong[] alpha = new ulong[] { 2 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            polynomial.array = operation.MultiplicationModIrreducible(m, polynomial.array, alpha_start, alpha);
            Assert.AreEqual(result, polynomial.ToString());
        }



        [Test]
        [TestCase(281, 9, 4, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void SquaringTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            ulong[] alpha = new ulong[] { 2 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);

            ulong[] module = new ulong[polynomial.array.Length];
            Array.Copy(polynomial.array, module, polynomial.array.Length);

            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            polynomial.array = operation.MultiplicationModIrreducible(m, polynomial.array, alpha_start, alpha_start);

            var result = operation.Squaring(alpha_start, module, m);
            Assert.AreEqual(0, operation.LongCmp(result, polynomial.array));
        }


        [Test]
        [TestCase(281, 9, 4, 1, 0, 1ul)]
        [TestCase(163, 7, 6, 3, 0, 1ul)]
        [TestCase(409, 15, 6, 1, 0, 1ul)]
        [TestCase(293, 11, 6, 1, 0, 1ul)]
        [TestCase(571, 10, 5, 2, 0, 1ul)]
        public void TraceTest(int m, int deg2, int deg3, int deg4, int deg5, ulong expected)
        {
            ulong[] alpha = new ulong[] { 2 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);

            ulong[] module = new ulong[polynomial.array.Length];
            Array.Copy(polynomial.array, module, polynomial.array.Length);

            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            var result = operation.Trace(alpha_start, module, m);
            
            Assert.AreEqual(expected, result[0]);
        }


        [Test]
        [TestCase(281, 9, 4, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void LongModPowerTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            ulong[] one = new ulong[] { 1 };
            ulong[] alpha = new ulong[] { 2 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            var power = operation.LongSub(operation.ShiftBitsToHigh(one, m), one);
            var result = operation.LongModPowerBarrett(alpha, power, polynomial.array, m);
            Assert.AreEqual(0, operation.LongCmp(one, result));
        }


        [Test]
        [TestCase(281, 9, 4, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void  InversedElementTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            ulong[] one = new ulong[] { 1 };
            ulong[] two = new ulong[] { 2 };
 
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            var power = operation.LongSub(operation.ShiftBitsToHigh(one, m), two);
            var alpha_start = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            var inverse = operation.LongModPowerBarrett(alpha_start, power, polynomial.array, m);

            var result = operation.MultiplicationModIrreducible(m, polynomial.array, alpha_start, inverse);

            Assert.AreEqual(0, operation.LongCmp(one, result));
        }


        [Test]
        [TestCase(281, 9, 4, 1, 0)]
        [TestCase(163, 7, 6, 3, 0)]
        [TestCase(409, 15, 6, 1, 0)]
        [TestCase(293, 11, 6, 1, 0)]
        [TestCase(571, 10, 5, 2, 0)]
        public void LinearTest(int m, int deg2, int deg3, int deg4, int deg5)
        {
            ulong[] one = new ulong[] { 1 };
            ulong[] two = new ulong[] { 2 };

            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);

            var a = new ulong[] { 77 };
            var b = new ulong[] { 2 };
            var c = new ulong[] { 43 };

            var result1 = operation.MultiplicationModIrreducible(m, polynomial.array, operation.XOR(a, b), c);
            var result2 = operation.XOR(operation.MultiplicationModIrreducible(m, polynomial.array, a, c), operation.MultiplicationModIrreducible(m, polynomial.array, b, c));

            Assert.AreEqual(0, operation.LongCmp(result1, result2));
        }
    }    
}
