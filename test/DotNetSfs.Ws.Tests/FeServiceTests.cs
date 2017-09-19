using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DotNetSfs.Ws.Res;
using DotNetSfs.Ws.Security;
using DotNetSfs.Ws.Sunat;
using Xunit;
using Xunit.Abstractions;

namespace DotNetSfs.Ws.Tests
{
    public class FeServiceTests
    {
        private readonly ITestOutputHelper _output;
        private readonly FeService _service;

        public FeServiceTests(ITestOutputHelper output)
        {
            _output = output;
            _service = new FeService(new SolConfig
            {
                Ruc = "20600995805",
                Usuario = "MODDATOS",
                Clave = "moddatos",
                Service = ServiceSunatType.Beta
            });
        }

        [Fact]
        public void SendDocumentTest()
        {
            var name = "20600995805-01-F001-00005214";
            var filePath = Path.Combine(Environment.CurrentDirectory, "Resources", name + ".xml");
            var content = File.ReadAllBytes(filePath);

            var task = _service.SendDocument(name, content);
            task.Wait();

            var result = task.Result;

            if (!result.Success)
                _output.WriteLine(result.Error.Code + " - " + result.Error.Description);

            Assert.True(result.Success);
            Assert.NotNull(result.ApplicationResponse);
            Assert.Contains("aceptada", result.ApplicationResponse.Descripcion);
            _output.WriteLine(result.ApplicationResponse.Descripcion);
        }

        [Fact]
        public void SendDocumentTest_with_Error()
        {
            var name = "20600995805-01-F001-00005214";
            var filePath = Path.Combine(Environment.CurrentDirectory, "Resources", name + ".xml");
            var content = File.ReadAllBytes(filePath);

            var task = _service.SendDocument("20604595805-01-F001-00005214", content);
            task.Wait();

            var result = task.Result;

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Contains("Client.1034", result.Error.Code);

            _output.WriteLine(result.Error.Code + " - " + result.Error.Description);
        }
    }
}