using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FibonacciVariations
{
    class Program
    {
        static void Main(string[] args)
        {

            var numberOfMeasurements = 5;

            /////////////////////////////// n = 100

            //GetPerformance(FibonacciRecursive, 100, numberOfMeasurements);           // runtime: infinity
            GetPerformance(FibonacciIterative, 100, numberOfMeasurements);           // runtime: < 1ms
            GetPerformance(FibonacciIterativeSquaring, 100, numberOfMeasurements);   // runtime: < 1ms

            ///////////////////////////////  n = 1000

            GetPerformance(FibonacciIterative, 1000, numberOfMeasurements);            // runtime: < 1ms
            GetPerformance(FibonacciIterativeSquaring, 1000, numberOfMeasurements);    // runtime: < 1ms

            /////////////////////////////// n = 10000

            GetPerformance(FibonacciIterative, 10000, numberOfMeasurements);           // runtime: 6ms
            GetPerformance(FibonacciIterativeSquaring, 10000, numberOfMeasurements);   // runtime: 1ms

            /////////////////////////////// n = 100000

            GetPerformance(FibonacciIterative, 100000, numberOfMeasurements);              // runtime: 0.3s
            GetPerformance(FibonacciIterativeSquaring, 100000, numberOfMeasurements);      // runtime: 51ms

            /////////////////////////////// n = 200000

            GetPerformance(FibonacciIterative, 200000, numberOfMeasurements);              // runtime: 0.3s
            GetPerformance(FibonacciIterativeSquaring, 200000, numberOfMeasurements);      // runtime: 51ms

            /////////////////////////////// n = 300000

            GetPerformance(FibonacciIterative, 300000, numberOfMeasurements);              // runtime: 2.8s
            GetPerformance(FibonacciIterativeSquaring, 300000, numberOfMeasurements);      // runtime: 0.4s

            /////////////////////////////// n = 400000

            GetPerformance(FibonacciIterative, 400000, numberOfMeasurements);              // runtime: 2.8s
            GetPerformance(FibonacciIterativeSquaring, 400000, numberOfMeasurements);      // runtime: 0.4s

            /////////////////////////////// n = 500000

            GetPerformance(FibonacciIterative, 500000, numberOfMeasurements);              // runtime: 7.7s
            GetPerformance(FibonacciIterativeSquaring, 500000, numberOfMeasurements);      // runtime: 1.0s

            // IterativeSquaring is about 7 times faster than Iterative


            ///////////////////////////////

            Console.WriteLine("\n\nFinished calculations.");

            Console.ReadLine();
        }




        public static TimeSpan GetPerformance(Func<BigInteger, BigInteger> func, BigInteger fibonacciIndex, int numberOfMeasurements)
        {
            //Console.WriteLine($"\nMeasurements for {func.Method.Name}\n");

            var runtime = TimeSpan.Zero;
            var stopWatch = new Stopwatch();
            BigInteger fibonacciNumber = 0;

            for (int i = 1; i <= numberOfMeasurements; i++)
            {
                stopWatch.Reset();
                stopWatch.Start();

                fibonacciNumber = func(fibonacciIndex);

                stopWatch.Stop();
                runtime = runtime.Add(stopWatch.Elapsed);
                //Console.WriteLine($"Measurement {i} of {func.Method.Name}({fibonacciIndex}): {stopWatch.Elapsed.ToString()} ({stopWatch.Elapsed.Milliseconds}ms)");
            }
            //Console.WriteLine($"\nCalculated Fibonacci Number: {fibonacciNumber}");

            var meanPerformance = new TimeSpan(runtime.Ticks / numberOfMeasurements);
            Console.WriteLine($"Mean Performance for {func.Method.Name}({fibonacciIndex}): {meanPerformance.ToString()} ({meanPerformance.TotalMilliseconds}ms)\n\n");

            return meanPerformance;
        }







        public static BigInteger FibonacciRecursive(BigInteger n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater zero");

            if (n == 1 || n == 2)
                return 1;

            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }





        public static BigInteger FibonacciIterative(BigInteger n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater zero");

            if (n == 1 || n == 2)
                return 1;

            BigInteger result = 0;

            BigInteger last = 1;
            BigInteger secondLast = 1;

            for (int i = 3; i <= n; i++)
            {
                result = last + secondLast;
                secondLast = last;
                last = result;
            }

            return result;
        }





        public struct Matrix
        {
            public BigInteger A { get; set; }
            public BigInteger B { get; set; }
            public BigInteger C { get; set; }
            public BigInteger D { get; set; }
        }


        public static BigInteger FibonacciIterativeSquaring(BigInteger n)
        {
            if (n <= 0) throw new ArgumentException("n must be greater zero");

            if (n == 1 || n == 2)
                return 1;

            // n > 2
            var result = FibonacciIterativeSquaringHelper(n - 2);
            return result.A + result.C;

        }

        public static Matrix FibonacciIterativeSquaringHelper(BigInteger n)
        {
            if (n == 1)
            {
                return new Matrix() { A = 1, B = 1, C = 1, D = 0 };
            }
            else if (n % 2 == 0) // even
            {
                var res = FibonacciIterativeSquaringHelper(n / 2);
                return new Matrix()
                {
                    A = ((res.A * res.A) + (res.B * res.C)),
                    B = ((res.A * res.B) + (res.B * res.D)),
                    C = ((res.C * res.A) + (res.D * res.C)),
                    D = ((res.C * res.B) + (res.D * res.D))
                };
            }
            else // odd 
            {
                var res = FibonacciIterativeSquaringHelper(n / 2);
                return new Matrix()
                {
                    A = ((res.A * res.A) + (res.B * res.C) + (res.A * res.B) + (res.B * res.D)),
                    B = ((res.A * res.A) + (res.B * res.C)),
                    C = ((res.C * res.A) + (res.D * res.C) + (res.C * res.B) + (res.D * res.D)),
                    D = ((res.C * res.A) + (res.D * res.C))
                };
            }
        }


    }


}
