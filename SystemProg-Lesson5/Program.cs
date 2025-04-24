using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SystemProg_Lesson5
{
    internal class Program
    {
        static bool IsFib(int num)
        {
            int a = 1, b = 1;
            while (a < num)
            {
                int temp = b;
                b += a;
                a = temp;
            }
            return a == num;
        }
        static bool IsPrime(int num)
        {
            if (num < 2) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
                if (num % i == 0) return false;
            return true;
        }
        static bool NumHaveDigits(int num, int[] digits)
        {
            while (0 < num)
            {
                int tmp = num % 10;
                if (digits.Contains(tmp))
                    return true;
                num /= 10;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Random random = new Random();
            List<int> numbers = Enumerable.Range(0, 10000).ToList();

            int start = 10;
            int end = 4123;

            Task<List<int>>[] rep =
            {
                Task.Run(() =>
                {
                    List<int> fibNumbers = new List<int>();
                    numbers.ForEach(n => {
                        if (213 < n && IsFib(n))
                            fibNumbers.Add(n);
                    });
                    return fibNumbers;
                }),
                Task.Run(() =>
                {
                    List<int> primeNumbers = new List<int>();
                    numbers.ForEach(n => {
                        if (n % 28 == 0 && !IsPrime(n))
                            primeNumbers.Add(n);
                    });
                    return new List<int> {primeNumbers.Max() };
                }),
                Task.Run(() =>
                {
                    List<int> correct_numbers = new List<int>();
                    numbers.ForEach(n => { 
                        if (NumHaveDigits(n, new int[]{ 1, 7})) 
                            correct_numbers.Add(n); 
                    });
                    return new List<int> { Convert.ToInt32(correct_numbers.Average())} ;
                }),
                Task.Run(() =>
                {
                    return new List<int> {numbers.Where(x => x >= start && x <= end).Sum() };
                })
            };
            foreach (var task in rep)
            {
                task.Wait();
            }
            foreach (var task in rep)
            {
                task.Result.ForEach(x => Console.Write($"{x}, "));
                Console.WriteLine("\n");
            }
        }
    }
}
