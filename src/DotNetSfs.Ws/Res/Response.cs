using System.Diagnostics.CodeAnalysis;

// Code
//- Del 0100 al 1999 Excepciones
//- Del 2000 al 3999 Errores que generan rechazo
//- Del 4000 en adelante Observaciones

namespace DotNetSfs.Ws.Res
{
    /// <summary>
    /// Describe un error.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Codigo del error
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Descripcion del Error
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Clase Base para las respuestas
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Base para la Respuesta de Sunat
        /// </summary>
        protected BaseResponse()
        {
            Success = false;
        }

        /// <summary>
        /// Indica que la accion se ejecuto,y hay una respuesta del WebService.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Detalla el Error que existe cuando ocurre una excepcion lanzada por el webservice o de otro tipo.
        /// </summary>
        public ErrorResponse Error { get; set; }
    }
    /// <summary>
    /// Información sobre la respuesta que se da al proceso de recepción del documento electrónico enviado por el contribuyente.
    /// </summary>
    public class SunatResponse : BaseResponse
    {
        /// <summary>
        /// Respuesta que representa el XML enviado por la Sunat, disponible si Sucess es True
        /// </summary>
        public CdrResponse ApplicationResponse{ get; set; }

        /// <summary>
        /// Bytes de un archivo zip Devuelto por Sunat
        /// </summary>
        public byte[] ContentZip{ get; set; }
    }
    /// <summary>
    /// Representa la respuesta al enviar un Resumen o Comunicacion de Baja.
    /// </summary>
    public class TicketResponse : BaseResponse
    {
        /// <summary>
        /// El Ticket que posteriormente se utilizara por el metodo getStatus.
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// Representa la respuesta al solicitar el Estado de un Comprobante.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class StatusCompResponse : SunatResponse
    {
        /// <summary>
        /// StatusCode enviado por Sunat
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Aquí se indica si CDR existe o se encuentra en proceso
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Representa un Comprobante Electronico
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable ClassNeverInstantiated.Global
    public class ComprobanteEletronico
    {
        /// <summary>
        /// Es el tipo de comprobante a consultar.
        /// 01: Factura.
        /// 07: Nota de crédito.
        /// 08: Nota de débito.
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Serie del Comprobante
        /// </summary>
        public string Serie { get; set; }

        /// <summary>
        /// Numero Correlativo del Comprobante.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Representacion en formato String
        /// </summary>
        /// <returns>Cadena {serie-correlativo}</returns>
        public override string ToString()
        {
            return Serie + "-" + Numero;
        }
    }

    /// <summary>
    /// Representa La Constancia de Recepción devuelta por Sunat.
    /// </summary>
    public class CdrResponse
    {
        /// <summary>
        /// Identificación del documento electrónico procesado.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Código de la respuesta al documento electrónico procesado.
        /// </summary>
        public string Codigo { get; internal set; }

        /// <summary>
        /// Descripción de la respuesta al documento electrónico procesado.
        /// </summary>
        /// <value>The descripcion.</value>
        public string Descripcion { get; internal set; }

        /// <summary>
        /// Mensajes o notas asociados al documento de respuesta.
        /// </summary>
        public string[] Notas { get; internal set; }
    }
}
