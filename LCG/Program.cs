using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCG
{
    public class Program
    {
        private enum Mode
        {
            /// <summary>
            /// Linear equation of Xn
            /// </summary>
            One,
            /// <summary>
            /// Square equation of Xn
            /// </summary>
            Two,
            /// <summary>
            /// Cube equation of Xn
            /// </summary>
            Three
        }

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //LinearCongruentialGenerator.PrimeFactors(2147483647);
            var lcg = new LinearCongruentialGenerator();

            var modes = new[] { Mode.One, Mode.Two, Mode.Three };

            foreach(var mode in modes)
            {
                Console.WriteLine($"Mode: {mode.ToString()}\n");
                await Task.Run(() =>
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Console.Write(lcg.Next(30, mode) + " ");
                    }
                });
                Console.WriteLine("\n_____________________________________________________________________\n");
            }

            Console.ReadKey();
        }

        private class LinearCongruentialGenerator
        {
            //Borland C/C++ values from en wiki
            private const long m = 2147483647; // 2^31-1
            private const long a = 48271;
            private const long c = 0;
            private const long b = 16807;

            private long _prev;

            public LinearCongruentialGenerator()
            {
                //make start value to be in 0 <= X < m
                _prev = DateTime.Now.Ticks % m; 
            }

            public LinearCongruentialGenerator(long seed)
            {
                _prev = seed;
            }

            /// <summary>
            /// Gets next random number with selected generator mode
            /// </summary>
            /// <param name="mode"></param>
            /// <returns></returns>
            public long Next(Mode mode = Mode.One)
            {
                switch (mode)
                {
                    case Mode.One:
                        _prev = (a * _prev + c) % m;
                        break;
                    case Mode.Two:
                        _prev = (b * Convert.ToInt64(Math.Pow(_prev, 2)) + a * _prev + c) % m;
                        break;
                    case Mode.Three:
                        //_prev = (b * Convert.ToInt64(Math.Pow(_prev, 2)) + a * _prev + c) % m;
                        break;
                }

                return _prev;
            }

            /// <summary>
            /// Gets next random number with maximum value of (max-1)
            /// </summary>
            /// <param name="max">maximum value</param>
            /// <param name="mode">generator mode used to calculate a value</param>
            /// <returns></returns>
            public long Next(long max, Mode mode = Mode.One)
            {
                return Next() % max;
            }
        }
    }
}
