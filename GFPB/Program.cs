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
            var element = new ulong[] { 5 };
            ulong[] module = new ulong[] { 13 };
            // var res = operation.Trace(element, module, 3);


            var v1 = element;       //operation.Squaring(element, module, 3);
            //var v11 =// operation.MultiplicationModIrreducible(3, module, element, element);

            var v2 = operation.Squaring(v1, module, 3);
          //  var v22 = operation.MultiplicationModIrreducible(3, module, v1, v1);
            /*
            var v3 = operation.Squaring(v2, module, 3);
            var v33 = operation.MultiplicationModIrreducible(3, module, v2, v2);

            var res = operation.XOR(v1, v2);
            res = operation.XOR(res, v3);*/
        }
    }
}    
