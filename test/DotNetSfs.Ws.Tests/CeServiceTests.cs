using System;
using System.IO;
using DotNetSfs.Ws.Res;
using Xunit;
using Xunit.Abstractions;

namespace DotNetSfs.Ws.Tests
{
    public class CeServiceTests
    {
        private readonly ITestOutputHelper _output;
        private readonly GuiaService _service;
        public CeServiceTests(ITestOutputHelper output)
        {
            _output = output;
            _service = new GuiaService(new SolConfig
            {
                Ruc = "20600995805",
                Usuario = "MODDATOS",
                Clave = "moddatos",
                Service = ServiceSunatType.Beta
            });
        }

        [Fact]
        public void SendTest()
        {
            var name = "20600995805-09-T001-00000001";
            var filePath = Path.Combine(Environment.CurrentDirectory, "Resources", name + ".xml");
            var content = File.ReadAllBytes(filePath);
            
            var task = _service.SendDocument(name, content);
            task.Wait();

            var result = task.Result;

            if (!result.Success)
            {
                _output.WriteLine(result.Error.Code + " - " + result.Error.Description);

                if (result.Error.Code.Contains("Server"))
                {
                    return;
                }
            }
            
            Assert.True(result.Success);
            Assert.NotNull(result.ApplicationResponse);
            Assert.Contains("aceptada", result.ApplicationResponse.Descripcion);
            _output.WriteLine(result.ApplicationResponse.Descripcion);
        }
    }
}
