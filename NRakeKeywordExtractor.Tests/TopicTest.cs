// <copyright file="TopicTest.cs">Copyright ©  2018</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRakeKeywordExtractor;

namespace NRakeKeywordExtractor.Tests
{
    /// <summary>This class contains parameterized unit tests for Topic</summary>
    [PexClass(typeof(Topic))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class TopicTest
    {
        /// <summary>Test stub for GetTitle(String)</summary>
        [PexMethod]
        public string GetTitleTest([PexAssumeUnderTest]Topic target, string path)
        {
            string result = target.GetTitle(path);
            return result;
            // TODO: add assertions to method TopicTest.GetTitleTest(Topic, String)
        }
    }
}
