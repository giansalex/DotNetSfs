using System;
using System.IO;
using Xunit;

namespace DotNetSfs.Xml.Tests
{
    public class XmlSignatureProviderTests
    {
        [Fact]
        public void VerifyXmlFileTest()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Resources",
                "20552256647-01-FF12-242.xml");

            var result = XmlSignatureProvider.VerifyXmlFile(path);
            Assert.True(result);
        }

        [Fact]
        public void VeifyXmlFile_NotValid_Test()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Resources",
                "20600995805-03-B001-1.xml");

            var result = XmlSignatureProvider.VerifyXmlFile(path);
            Assert.False(result);
        }
    }
}