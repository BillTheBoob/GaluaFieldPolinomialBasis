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
           result[i] = ~result[i];
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


    public ulong[] OR(ulong[] x, ulong[] y)
    {
        ulong[] a = new ulong[x.Length];
        ulong[] b = new ulong[y.Length];
        Array.Copy(x, a, x.Length);
        Array.Copy(y, b, y.Length);

        LengthControl(ref a, ref b);
        ulong[] result = new ulong[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = a[i] | b[i];
        }
        return result;
    }


    public ulong[] Reduce(ulong[] prod, ulong[] module ,int m)
    {
        ulong[] mask = ShiftBitsToHigh(one, m);
        mask = ShiftBitsToHigh(mask, m - 2);
        for (int k = m - 2; k >= 0; k--)
        {
            if (LongCmp(BitMul(prod, mask), zero) > 0)
            {
                prod = BitMul(NOT(ShiftBitsToHigh(mask,1)), prod);
                prod = XOR(prod, ShiftBitsToHigh(module, k));
            }
            mask = ShiftBitsToLow(mask, 1);
        }
        return prod;
    }

    public ulong[] MultiplicationModIrreducible(int m, ulong[] module, ulong[] a, ulong[] b)
    {
        ulong[] prod = zero;
        
        for (int k = 0; k < m; k++)
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
        return Reduce(prod, module, m);
    }


    public int HighNotZeroIndex(ulong[] a)
    {
        for (var i = a.Length - 1; i >= 0; i--)
        {
            if (a[i] > 0) { return i; }
        }
        return 0;
    }


    public int BitLength(ulong[] a)
    {
        var bits = 0;
        var index = HighNotZeroIndex(a);
        var temp = a[index];
        while (temp > 0)
        {
            temp >>= 1;
            bits++;
        }
        return bits + sizeof(ulong) * 8 * index;
    }


    public ulong[] Squaring(ulong[] x, ulong[] module, int m)
    {
        ulong[] a = new ulong[x.Length];
        Array.Copy(x, a, x.Length);
        ulong[] result = new ulong[1];
        ulong[] one = new ulong[1] {1};
        ulong[] two = new ulong[1] {2};
        ulong[] three = new ulong[1] {3};
        ulong[] r1 = new ulong[1];
        ulong[] r2 = new ulong[1];
        int j = 0;
        int expected_length = (BitLength(x) * 2) - 1;
        if (LongCmp(one, x) == 0) { return x; }

        while (BitLength(result) !=  expected_length)
        {
            int i = 0;
            
            if (LongCmp(BitMul(a, three), three) == 0)
            {
                if (j >= 2)
                {
                    r1 = ShiftBitsToHigh(OR(ShiftBitsToHigh(one, j), ShiftBitsToHigh(one, j - 2)) , 2);
                }
                else
                {
                    result = OR((BitMul(a, one)), ShiftBitsToHigh((BitMul(a,two)), 1));
                }
                result = OR(r1, result);
                j += 2;
            }
            else
            {
                r1 = BitMul(a , one);
                r2 = ShiftBitsToLow(a , 1);
                i = 0;
                while (LongCmp(BitMul(r2 , one), one) != 0 )
                {
                    i++;
                    r2 = ShiftBitsToLow(r2 , 1);
                }
                ulong[] temp = ShiftBitsToHigh(one, (i + 1)*2);
                result = OR(ShiftBitsToHigh(OR(temp, r1), j) , result);
                j += ((i + 1) * 2);
            }
            a = ShiftBitsToLow(a, i + 1);
        }
        return Reduce(result, module, m);
    }

     


    public ulong[] Trace(ulong[] a, ulong[] module , int m)
    {
        ulong[] result = new ulong[1];
        for (int i = 0; i < m; i++)
        {
            result = XOR(a, result);
            a = Squaring(a, module, m);
        }
        return result;
    }

}
