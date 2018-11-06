using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRakeKeywordExtractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRakeKeywordExtractor.Tests
{
    [TestClass()]
    public class TopicTests
    {
        [TestMethod()]
        public void TopicTest()
        {
            var t = new Topic(@"C:\Users\ion.gireada\Source\Repos\NRakeKeywordExtractor\NRakeKeywordExtractorTests\TestTopic.html");
            Assert.IsNotNull(t);
        }

        [TestMethod()]
        public void GetTitleTest()
        {
            var t = new Topic(@"C:\Users\ion.gireada\Source\Repos\NRakeKeywordExtractor\NRakeKeywordExtractorTests\TestTopic.html");
            var title = t.GetTitle();
            Assert.AreEqual("TestTopic Title", title);
        }

        [TestMethod()]
        public void GetTextTest()
        {
            var t = new Topic(@"C:\Users\ion.gireada\Source\Repos\NRakeKeywordExtractor\NRakeKeywordExtractorTests2\TestTopic.html");
            var body = t.GetText();
            Assert.IsTrue(body.Contains("Here is the content"));
        }
    }
}