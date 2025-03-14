﻿namespace PerfViewTutorial {
    internal class Program
    {
        public static int aStatic = 0;
        /// <summary>
        /// Spin is a simple compute bound program that lasts for 5 seconds
        /// It is a useful test program for CPU profilers.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            int numSec = 5;
            if (args.Length == 1)
                numSec = int.Parse(args[0]);

            Console.WriteLine("Spinning for {0} seconds", numSec);
            RecSpin(numSec);
            return 0;
        }

        /// <summary>
        /// Spin for 'timeSec' seconds.   We do only 1 second in this
        /// method, doing the rest in the helper.
        /// </summary>
        static void RecSpin(int timeSec)
        {
            if (timeSec <= 0)
                return;
            --timeSec;
            SpinForASecond();
            RecSpinHelper(timeSec);
        }

        /// <summary>
        /// RecSpinHelper is a clone of RecSpin.   It is repeated 
        /// to simulate mutual recursion (more interesting example)
        /// </summary>
        static void RecSpinHelper(int timeSec)
        {
            if (timeSec <= 0)
                return;
            --timeSec;
            SpinForASecond();
            RecSpin(timeSec);
        }

        /// <summary>
        /// SpingForASecond repeatedly calls DateTime.Now until for 1 second.
        /// It also does some work of its own in this
        /// methods so we get some exclusive time to look at.
        /// </summary>
        static void SpinForASecond()
        {
            DateTime start = DateTime.Now;
            for (; ; )
            {
                if ((DateTime.Now - start).TotalSeconds > 1)
                    break;

                // Do some work in this routine as well.   
                for (int i = 0; i < 10; i++)
                    aStatic += i;
            }
        }
    }
}