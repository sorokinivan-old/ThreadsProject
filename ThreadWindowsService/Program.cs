using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using ThreadWindowsService.Models;
using ThreadWindowsService.Tests;

namespace ThreadWindowsService
{
    public static class Program
    {
        static readonly System.Object lockThis = new System.Object();
        static async Task Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                ThreadService service1 = new ThreadService();
                service1.TestStartupAndStop(args);
            }
            else
            {

                Console.WriteLine("Введите кол-во потоков (Для запуска тестов введите -1)");
                var count = Int32.Parse(Console.ReadLine());
                if (count == -1)
                {
                    RunTest();
                }
                else
                {
                    bool check = CreateThreads(count);
                    if(check == true)
                        Console.WriteLine("XML file created succesfully");
                    else
                        Console.WriteLine("XML file create failed");
                }

                Console.ReadKey();
            }

        }

        public static bool CreateThreads(int count)
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
            foreach(var thread in threads)
            {
                thread.Join();
            }
            if (File.Exists(@"D:\test.xml"))
                return true;
            else
                return false;
                
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

            Append(apiResponse);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine(apiResponse);
            Console.WriteLine("Time from request to response for a thread " + threadName + " - " + time);
            sw.Reset();
        }

        public static void Append(string response)
        {

            lock (lockThis)
            {

                var xmlResponse = new XElement("response", response);
                var doc = new XDocument();
                if (File.Exists(@"D:\test.xml"))
                {
                    doc = XDocument.Load(@"D:\test.xml");
                    doc.Element("responses").Add(xmlResponse);
                }
                else
                {
                    doc = new XDocument(new XElement("responses", xmlResponse));
                }

                doc.Save(@"D:\test.xml");
                
            }
                



        }
        public static void RunTest()
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
