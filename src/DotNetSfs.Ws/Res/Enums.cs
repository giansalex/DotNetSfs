namespace DotNetSfs.Ws.Res
{
    /// <summary>
    /// Tipos de Webservices disponibles para Facturacion Electronica
    /// </summary>
    public enum ServiceSunatType
    {
        /// <summary>
        /// Servicio para pruebas previas al envio de produccion (Facturas y sus Notas, Resumen y Dara de Baja)
        /// </summary>
        Beta,
        /// <summary>
        /// Servicio usado cuando se pasa el proceso de Homologacion
        /// </summary>
        Produccion,
        /// <summary>
        /// Servico para la Homologacion
        /// </summary>
        Homologacion,
    }

}