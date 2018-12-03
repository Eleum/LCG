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
            var lcg = new LinearCongruentialGenerator();


        }

        private class LinearCongruentialGenerator
        {
            //Borland C/C++ values from en wiki
            private const long m = 4294967296; // 2^32
            private const long a = 22695477;
            private const long c = 1;

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
                        //_prev = (a * Math.Pow(_prev, 2) +  + c) % m;
                        break;
                    case Mode.Three:
                        break;
                }

                return _prev;
            }

            /// <summary>
            /// Gets next random number with maximum value of (max-1)
            /// </summary>
            /// <param name="max">maximum value</param>
            /// <returns></returns>
            public long Next(long max)
            {
                return Next() % max;
            }
        }
    }
}
