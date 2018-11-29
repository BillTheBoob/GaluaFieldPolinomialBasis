using System;

public class Operation
{
	public ulong[] ShiftBitsToHigh(ulong[] a, int shiftnum)
	{
        if (shift_num == 0) return a;
        ulong c = new ulong[a.array.Length];
        ulong[] surrogate = new ulong[b.array.Length];
        Array.Copy(b.array, surrogate, b.array.Length);
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
            surrogate = c.array;
        }
        return c;
    }
}
