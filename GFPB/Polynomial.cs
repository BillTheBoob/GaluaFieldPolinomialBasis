
public class Polynomial
{
    public ulong[] Generator = new ulong[1];
    Operation operation = new Operation();
    public ulong[] arr1 = new ulong[1];
    public ulong[] one = new ulong[1];
    public Polynomial(int m, int deg1, int deg2, int deg3, int deg4)
    {
        int ulamount;
        int q = m / 64;
        if (m % 64 != 0) { ulamount = q + 1; }
        else {ulamount = q; }
        Generator = new ulong[ulamount];

        ulong[] zero = new ulong[ulamount];
        one = new ulong[ulamount];
        one[0] = 1;
       
        arr1 = operation.ShiftBitsToHigh(one, deg1);
        ulong[] arr2 = operation.ShiftBitsToHigh(one, deg2);
        ulong[] arr3 = operation.ShiftBitsToHigh(one, deg3);
        ulong[] arr4 = operation.ShiftBitsToHigh(one, deg4);

        for(int i = 0; i < ulamount; i++)
        {
            Generator[i] = arr1[i] | arr4[i] | one[i];
        }

    }
}
