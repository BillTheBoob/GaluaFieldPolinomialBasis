using System;
using System.Linq;

public class Polynomial
{
    public ulong[] array = new ulong[0];
    Operation operation = new Operation();
    public ulong[] one = new ulong[] { 1 };

    public int ArrayLength(int m)
    {
        int q = m / 64;
        return m % 64 != 0 ? q + 1 : q;
    }


    public ulong[] GeneratorPackage(ulong[] poly1, ulong[] poly2, ulong[] poly3, ulong[] poly4, ulong[] poly5)
    {
        ulong[] result = new ulong[poly1.Length];
        operation.LengthControl(ref poly1, ref poly2);
        operation.LengthControl(ref poly1, ref poly3);
        operation.LengthControl(ref poly1, ref poly4);
        operation.LengthControl(ref poly1, ref poly5);
        
        for(int i = 0; i < poly1.Length; i++)
        {
            result[i] = poly1[i] | poly2[i] | poly3[i] | poly4[i] | poly5[i];
        }
        return result;
    }



    public Polynomial(int m, int deg2, int deg3, int deg4, int deg5)
    {
        ulong[] poly1 = operation.ShiftBitsToHigh(one, m);
        ulong[] poly2 = operation.ShiftBitsToHigh(one, deg2);
        ulong[] poly3 = operation.ShiftBitsToHigh(one, deg3);
        ulong[] poly4 = operation.ShiftBitsToHigh(one, deg4);
        ulong[] poly5 = operation.ShiftBitsToHigh(one, deg5);

        array = GeneratorPackage(poly1, poly2, poly3, poly4, poly5);
    }



    public Polynomial(int m)
    {
        array = new ulong[ArrayLength(m)];
    }



    public override string ToString()
    {
        string result = "";
        ulong word;
        for(int i = 0; i < array.Length - 1; i++)
        {
            word = array[i];
            for (int j = 0; j != 64; j++, word >>= 1)
            {
                ulong bit = word & 1;
                result += bit.ToString();
            }
        }

        word = array[array.Length - 1];
        for (; word != 0; word >>= 1)
        {
            ulong bit = word & 1;
            result += bit.ToString();
        }
        var q = new string(result.ToCharArray().Reverse().ToArray());
        return q;
    }
}
