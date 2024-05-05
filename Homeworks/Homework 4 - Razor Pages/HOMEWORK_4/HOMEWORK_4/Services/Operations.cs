namespace HOMEWORK_4.Services
{
    public class Operations : IOperations
    {
        public int LCM(int a, int b)
        {
            return (a * b) / GCD(a, b);
        }

        public int GCD(int a, int b)
        {
            if (a == 0) return b;

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public int Max(int n1, int n2, int n3)
        {
            return Math.Max(n1, Math.Max(n2, n3));
        }

        public int Min(int n1, int n2, int n3)
        {
            return Math.Min(n1, Math.Min(n2, n3));
        }

        public int Multiply(int n1, int n2, int n3)
        {
            return n1 * n2 * n3;
        }

        public int Sum(int n1, int n2, int n3)
        {
            return n1 + n2 + n3;
        }
    }
}
