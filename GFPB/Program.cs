using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFPB
{
    class Program
    {
        static DateTime centuryBegin = new DateTime();
        static Operation operation = new Operation();

        public static ulong[] sum(int m, ulong[] module, ulong[] a, ulong[] b, ref long TickSum)
        {
            centuryBegin = DateTime.Now;
            var res = operation.MultiplicationModIrreducible(m, module, a, b);
            DateTime currentDate = DateTime.Now;
            TickSum += currentDate.Ticks - centuryBegin.Ticks;
            return res;
        }


        static void Main(string[] args)
        {
          Polynomial polynomial = new Polynomial(409, 15, 6, 1, 0); 
          var generator = operation.XOR(polynomial.array, operation.ShiftBitsToHigh(polynomial.one, 409));
            var alpha = new ulong[] { 2 };
            ulong[][] Tumen = new ulong[10000][];

            for (int i = 0; i < 10000; i++)
            {
                Tumen[i] = operation.MultiplicationModIrreducible(409, polynomial.array, generator, alpha);
            }

            ulong[][] Result = new ulong[10000][];
            long elapsedTicks = 0;
            for (int i = 0; i < 10000; i++)
            {
                Result[i] = sum(409, polynomial.array, Tumen[i], Tumen[i], ref elapsedTicks);
            }
           

            
            Console.WriteLine(elapsedTicks/10000);// операций на одно умножение
        }
    }
}    
