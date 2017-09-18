using System;
using System.ServiceModel;
using System.Threading.Tasks;
using DotNetSfs.Ws.Res;
using DotNetSfs.Ws.Security;
using DotNetSfs.Ws.Sunat;

namespace DotNetSfs.Ws
{
    /// <summary>
    /// Sunat Base class.
    /// </summary>
    public abstract class BaseSunat
    {
        #region Fields
        private readonly SolConfig _config;
        private string _url;
        #endregion

        #region Properties
        /// <summary>
        /// Url of WebService.
        /// </summary>
        public string Url
        {
            set => _url = value;
        }
        

        #endregion

        #region Construct

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config">Config</param>
        protected BaseSunat(SolConfig config)
        {
            _config = config;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Recibe la ruta XML con un único formato digital y devuelve la Constancia de Recepción – SUNAT. 
        /// </summary>
        /// <param name="pathFile">Ruta del Archivo XML</param>
        /// <param name="content">Contenido del archivo</param>
        /// <returns>La respuesta contenida en el XML de Respuesta de la Sunat, si existe</returns>
        public async Task<SunatResponse> SendDocument(string pathFile, byte[] content)
        {
            var fileToZip = pathFile + ".xml";
            var nameOfFileZip = pathFile + ".zip";

            var response = new SunatResponse
            {
                Success = false
            };
            try
            {
                var zipBytes = ProcessZip.CompressFile(fileToZip, content);
                var service = ServiceHelper.GetService<billService>(_config, _url);
                var result = await service.sendBillAsync(new sendBillRequest(nameOfFileZip, zipBytes, string.Empty));

                using (var outputXml = ProcessZip.ExtractFile(result.applicationResponse))
                    response = new SunatResponse
                    {
                        Success = true,
                        ApplicationResponse = ProcessXml.GetAppResponse(outputXml),
                        ContentZip = result.applicationResponse
                    };
            }
            catch (FaultException ex)
            {
                response.Error = GetErrorFromFault(ex);
            }
            catch (Exception er)
            {
                response.Error = new ErrorResponse
                {
                    Description = er.Message
                };
            }
            
            return response;
        }

        /// <summary>
        /// Envia una Resumen de Boletas o Comunicaciones de Baja a Sunat
        /// </summary>
        /// <param name="pathFile">Ruta del archivo XML que contiene el resumen</param>
        /// <param name="content">Contenido del archivo</param>
        /// <returns>Retorna un estado booleano que indica si no hubo errores, con un string que contiene el Nro Ticket,
        /// con el que posteriormente, utilizando el método getStatus se puede obtener Constancia de Recepcióno</returns>
        public async Task<TicketResponse> SendSummary(string pathFile, byte[] content)
        {
            var fileToZip = pathFile + ".xml";
            var nameOfFileZip = pathFile + ".zip";
            var res = new TicketResponse();
            try
            {
                var zipBytes = ProcessZip.CompressFile(fileToZip, content);
                var service = ServiceHelper.GetService<billService>(_config, _url);
                
                var result = await service.sendSummaryAsync(new sendSummaryRequest(nameOfFileZip, zipBytes, string.Empty));
                res.Ticket = result.ticket;
                res.Success = true;
                
            }
            catch (FaultException ex)
            {
                res.Error = GetErrorFromFault(ex);
            }
            catch (Exception er)
            {
                res.Error = new ErrorResponse
                {
                    Description = er.Message
                };
            }
            return res;
        }
        /// <summary>
        /// Devuelve un objeto que indica el estado del proceso y en caso de haber terminado, devuelve adjunta la ruta del XML que contiene la Constancia de Recepción
        /// </summary>
        /// <param name="pstrTicket">Ticket proporcionado por la sunat</param>
        /// <returns>Estado del Ticket, y la ruta de la respuesta si existe</returns>
        public async Task<SunatResponse> GetStatus(string pstrTicket)
        {
            var res = new SunatResponse();
            try
            {
                var service = ServiceHelper.GetService<billService>(_config, _url);
                
                var result = await service.getStatusAsync(new getStatusRequest(pstrTicket));
                var response = result.status;
                switch (response.statusCode)
                {
                    case "0":
                    case "99":
                        res.Success = true;
                        using (var xmlCdr = ProcessZip.ExtractFile(response.content))
                            res.ApplicationResponse = ProcessXml.GetAppResponse(xmlCdr);

                        res.ContentZip = response.content;
                        break;
                    case "98":
                        res.Success = false;
                        res.Error = new ErrorResponse { Description = "En Proceso"};
                        break;
                }
            }
            catch (FaultException ex)
            {
                res.Error = GetErrorFromFault(ex);
            }
            catch (Exception er)
            {
                res.Error = new ErrorResponse
                {
                    Description = er.Message,
                };
            }
            return res;
        }
        #endregion

        #region Private Methods

        private static ErrorResponse GetErrorFromFault(FaultException ex)
        {
            var errMsg = ProcessXml.GetDescriptionError(ex.Message);
            if (string.IsNullOrEmpty(errMsg))
            {
                var msg = ex.CreateMessageFault();
                if (msg.HasDetail)
                {
                    var dets = msg.GetReaderAtDetailContents();
                    errMsg = dets.ReadElementString(dets.Name);
                }
            }
            return new ErrorResponse
            {
                Code = ex.Message,
                Description = errMsg
            };
        }

        #endregion
    }
}
