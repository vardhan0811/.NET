using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M1.Practice.Application.Q05_LogAnalyzer;

namespace M1.Practice.Tests.Q05_LogAnalyzer
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [TestMethod]
        public void GetTopErrors_ShouldReturnMostFrequent()
        {
            var path = "testlog.txt";

            File.WriteAllLines(path, new[]
            {
                "ERR100 Something",
                "ERR200 Failed",
                "ERR100 Again",
                "ERR300 Crash",
                "ERR100 Error"
            });

            var analyzer = new LogAnalyzer();

            var result =
                analyzer.GetTopErrors(path, 2).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("ERR100", result[0].ErrorCode);
            Assert.AreEqual(3, result[0].Count);

            File.Delete(path);
        }
    }
}
