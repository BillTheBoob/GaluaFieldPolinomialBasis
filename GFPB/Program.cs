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
            Console.WriteLine(polynomial.ToString());
          

        }
    }
}    
