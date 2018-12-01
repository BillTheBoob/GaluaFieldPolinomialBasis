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
            ulong[] one = new ulong[] { 1 };
            ulong[] alpha = new ulong[] { one[0] << 1 };
            Operation operation = new Operation();
            Polynomial polynomial = new Polynomial(4, 0, 0, 1, 1);
            Console.WriteLine(polynomial.ToString());
            ulong[] mod = new ulong[] { 19 };
            ulong[] a = new ulong[] { 10 };
            ulong[] b = new ulong[] { 9 };
            var result = operation.MultiplicationModTwo(4, mod, a, b);

        }
    }
}    
