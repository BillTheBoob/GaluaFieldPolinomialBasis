using System;

public class Operation
{
    ulong[] zero = new ulong[1];
    public ulong[] one = new ulong[] { 1 };

    public void LengthControl(ref ulong[] a, ref ulong[] b)
    {
        var requiredlenght = Math.Max(a.Length, b.Length);
        Array.Resize(ref a, requiredlenght);
        Array.Resize(ref b, requiredlenght);
    }


    public int LongCmp(ulong[] z, ulong[] x)
    {
        LengthControl(ref z, ref x);
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
        while (c[i] == 0)
        {
            i--;
        }
        ulong[] result = new ulong[i + 1];
        Array.Copy(c, result, i + 1);
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
            c = new ulong[surrogate.Length + 1];
            var carriedBits = 0UL;
            if (shift_num < 64) { shift = shift_num; }
            else { shift = 63; }
            int i = 0;
            for (; i < surrogate.Length; i++)
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

    public ulong[] NOT(ulong[] a)
    {
        ulong[] result = new ulong[a.Length];
        Array.Copy(a, result, a.Length);
        for (int i = 0; i < a.Length; i++)
        {
            result[i] ^= 0xFFFFFFFFFFFFFFFF;
        }
        return result;
    }


    public ulong[] XOR(ulong[] x, ulong[] y)
    {
        ulong[] a = new ulong[x.Length];
        ulong[] b = new ulong[y.Length];
        Array.Copy(x, a, x.Length);
        Array.Copy(y, b, y.Length);

        LengthControl(ref a, ref b);
        ulong[] result = new ulong[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] ^ b[i];
        }
        return result;
    }


    /*public ulong[] UnitsToEnd(int m)
    {
        var temp = ShiftBitsToHigh(one, m);

    }
    */

    public ulong[] MultiplicationModIrreducible(int m, ulong[] module, ulong[] a, ulong[] b)
    {
        ulong[] prod = zero;
        int k = 0;
        /*multiply phase*/
        for (k = 0; k < m; k++)
        {
            if ((a[0] & 1) == 1)
            {
                prod = XOR(prod, ShiftBitsToHigh(b, k));
            }
            a = ShiftBitsToLow(a, 1);
            if (LongCmp(a, zero) == 0)
            {
                break;
            }
        }
        /*reduce phase*/

        ulong[] mask = ShiftBitsToHigh(one, m);
        mask = ShiftBitsToHigh(mask, m - 2);
        for (k = m - 2; k >= 0; k--)
        {
            if (LongCmp(BitMul(prod, mask), zero) > 0)
            {
                prod = BitMul(NOT(mask), prod);
                prod = XOR(prod, ShiftBitsToHigh(module, k));
            }
            mask = ShiftBitsToLow(mask, 1);
        }
        return prod;//BitMul(prod, UnitsToEnd(m));
    }
}
