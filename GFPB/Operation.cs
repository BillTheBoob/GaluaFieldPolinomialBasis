using System;

public class Operation
{

    public void LengthControl(ref ulong[] a, ref ulong[] b)
    {
        var requiredlenght = Math.Max(a.Length, b.Length);
        Array.Resize(ref a, requiredlenght);
        Array.Resize(ref b, requiredlenght);
    }


    public int LongCmp( ulong[] z, ulong[] x)
    {
        LengthControl(ref z,ref x);
        for (int i = z.Length - 1; i > -1; i--)
        {
            if (z[i] > x[i]) return 1;
            if (z[i] < x[i]) return -1;
        }
        return 0;
    }

    public ulong[] RemoveHighZeros(ulong[] c)
    {
        int i = c.Length - 1;
        while(c[i] == 0)
        {
            i--;
        }
        ulong[] result = new ulong[i+1];
        Array.Copy(c,result,i+1);
        return result;
    }

    public ulong[] ShiftBitsToHigh(ulong[] a, int shift_num)
	{
        if (shift_num == 0) return a;
        ulong[] c = new ulong[a.Length];
        ulong[] surrogate = new ulong[a.Length];
        Array.Copy(a, surrogate, a.Length);
        int shift;

        while (shift_num > 0)
        {
            c = new ulong[surrogate.Length+1];
            var carriedBits = 0UL;
            if (shift_num < 64) { shift = shift_num; }
            else { shift = 63; }
            int i = 0;
            for (; i < surrogate.Length ; i++)
            {
                var temp = surrogate[i];
                c[i] = (temp << shift) | carriedBits;
                carriedBits = temp >> (64 - shift);
            }
            c[i] = surrogate[i - 1] >> (64 - shift);
            shift_num -= 63;
            surrogate = c;
        }
        return RemoveHighZeros(c);
    }

    public ulong[] ShiftBitsToLow(ulong[] a, int shift_num)
    {
        ulong[] c = new ulong[a.Length];
        ulong[] surrogate = new ulong[a.Length];
        Array.Copy(a, surrogate, a.Length);
        int shift;
        c = new ulong[a.Length];
        while (shift_num > 0)
        {
            var carriedBits = 0UL;
            if (shift_num < 64) { shift = shift_num; }
            else { shift = 63; }
            int i = a.Length - 1;
            for (; i >= 0; i--)
            {
                var temp = surrogate[i];
                c[i] = (temp >> shift) | carriedBits;
                carriedBits = temp << (64 - shift);
            }
            shift_num -= 63;
            surrogate = c;
        }
        return c;
    }


    public ulong[] BitMul(ulong[] a, ulong[] b)
    {
        LengthControl(ref a, ref b);
        ulong[] result = new ulong[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] & b[i];
        }
        return result;
    }


    public ulong[] XOR(ulong[] a, ulong[] b)
    {
        LengthControl(ref a, ref b);
        ulong[] result = new ulong[a.Length];
        for(int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] ^ b[i];
        }
        return result;
    }

    public ulong[] MultiplicationModTwo(int m, ulong[] module, ulong[] a, ulong[] b)
    {
        ulong[] product = new ulong[Math.Max(a.Length, b.Length) << 1];
        ulong[] one = new ulong[1];
        one[0] = 1;
        ulong[] zero = new ulong[1];

        for (int k = 0; k < m; k++)
        {
            if ((a[0] & 1) == 1)
            {
                product = XOR(product, ShiftBitsToHigh(b, k));
            }


            a = ShiftBitsToLow(a, 1);
            if (LongCmp(a, zero) == 0)
            {
                break;
            }
        }

        ulong[] mask = ShiftBitsToHigh(one,m);
        mask = ShiftBitsToHigh(mask, m - 2);

        for(int j = m-2; j >= 0; j--)
        {
            if(LongCmp(BitMul(product, mask), zero) != 0)
            {
                product = BitMul(product, mask);
                product = XOR(product, ShiftBitsToHigh(module, j));
            }
            mask = ShiftBitsToLow(mask, 1);
        }
        return product;
    }

}
