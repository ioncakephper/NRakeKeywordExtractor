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
            Topic topic = new Topic(@"C:\Users\ion.gireada\Source\Repos\NRakeKeywordExtractor\NRakeKeywordExtractorTests\TestTopic.html");
            Assert.IsNotNull(topic);
        }
    }
}