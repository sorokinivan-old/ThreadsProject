using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ThreadService
{
    class Program
    {
        static async Task Main(string[] args)
        {   
           
            Console.WriteLine("Введите кол-во потоков (Для запуска тестов введите -1)");
            var count = Int32.Parse(Console.ReadLine());
            if(count == -1)
            {
                RunTest();
            }
            else
            {
                CreateThreads(count);
            }
        }

        public static void CreateThreads(int count)
        {
            var client = new HttpClient();
            var threads = new List<Thread>();
            for (var i = 0; i < count; i++)
            {
                threads.Add(new Thread(x => Test(client))
                {
                    Name = i.ToString()
                });

            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        public static void Test(HttpClient client)
        {
            var threadName = Thread.CurrentThread.Name;
            var request = new RequestView { ThreadId = threadName };
            XmlSerializer serializer = new XmlSerializer(typeof(RequestView));
            var subReq = new RequestView();
            var xml = "";
            var memoryStream = new MemoryStream();
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() { OmitXmlDeclaration = true }))
                {
                    serializer.Serialize(writer, request);
                    xml = sww.ToString();
                }
            }
            Stopwatch sw = new Stopwatch();
            var stringContent = new StringContent(xml, Encoding.UTF8, "text/xml");
            sw.Start();
            var response = client.PostAsync(string.Format("https://localhost:44378/home/Post.xml"), stringContent).Result;
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine(apiResponse);
            Console.WriteLine("Time from request to response for a thread "+ threadName+" - "+ time);
            sw.Reset();
        }
        public static void RunTest()
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
    
}
