using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ThreadService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            Console.WriteLine("Введите кол-во потоков");
            var count = Int32.Parse(Console.ReadLine());
            var threads = new List<Thread>();
            for (var i = 0; i < count; i++)
            {
                threads.Add(new Thread(x => Test(client))
                {
                    Name = i.ToString()
                });

            }

            foreach(var thread in threads)
            {
                thread.Start();
            }


        }

        public static void Test(HttpClient client)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(Thread.CurrentThread.Name), Encoding.UTF8, "application/json");
            var response = client.PostAsync(string.Format("https://localhost:44378/home/Post.xml"), stringContent).Result;
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(apiResponse);
        }
    }
}
