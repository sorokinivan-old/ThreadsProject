using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ThreadWindowsService;

namespace ThreadWindowsServiceTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CheckCreatingFile()
        {
            bool check = Program.CreateThreads(10);
            Assert.AreEqual(true, check);
        }   
    }
}
