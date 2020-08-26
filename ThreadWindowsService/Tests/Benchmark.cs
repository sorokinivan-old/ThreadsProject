using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadWindowsService.Tests
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
