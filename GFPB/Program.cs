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
            var one = new ulong[] {1};
          Operation operation = new Operation();
            int m = 409;
            int deg2 = 15;
            int deg3 = 6;
            int deg4 = 1;
            int deg5 = 0;

            ulong[] alpha = new ulong[] { 2 };
            Polynomial polynomial = new Polynomial(m, deg2, deg3, deg4, deg5);
            
            ulong[] module = new ulong[polynomial.array.Length];
            Array.Copy(polynomial.array, module, polynomial.array.Length);
            var power = operation.ShiftBitsToHigh(one, 409);
            var alpha_start = new ulong[] { 2};//operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, m));
            var result = operation.LongModPowerBarrett(alpha, power, module, m);
            
           
        }
    }
}    
