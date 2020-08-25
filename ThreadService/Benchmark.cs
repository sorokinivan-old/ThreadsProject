using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadService
{
    public class Benchmark
    {
        [Benchmark(Description = "150Threads")]
        public void MeasurmentMethod()
        {
            Program.CreateThreads(150);
        }

    }
}
