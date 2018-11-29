using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFPB
{
    class Program
    {

        static void Main(string[] args)
        {
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(4,4,1,1,1);
            var alpha = operation.ShiftBitsToHigh(polynomial.one, 1);
            var alpha_pow_max = operation.XOR(polynomial.Generator, polynomial.arr1);
            var aPOW5 = operation.MultiplicationModTwo(4,polynomial.Generator,alpha_pow_max, alpha);

        }
    }
}    
