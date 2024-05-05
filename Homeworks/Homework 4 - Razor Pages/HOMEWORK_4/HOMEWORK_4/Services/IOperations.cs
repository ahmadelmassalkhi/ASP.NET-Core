namespace HOMEWORK_4.Services
{
    public interface IOperations
    {
        public int Sum(int n1, int n2, int n3);
        public int Multiply(int n1, int n2, int n3);
        public int Min(int n1, int n2, int n3);
        public int Max(int n1, int n2, int n3);
        public int LCM(int n1, int n2);
        public int GCD(int n1, int n2);
    }
}
