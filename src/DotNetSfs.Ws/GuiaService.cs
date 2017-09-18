using System;
using DotNetSfs.Ws.Properties;
using DotNetSfs.Ws.Res;

namespace DotNetSfs.Ws
{
    /// <summary>
    /// Guia de Remision.
    /// </summary>
    public class GuiaService : BaseSunat

    {
        /// <inheritdoc />
        public GuiaService(SolConfig config) : base(config)
        {
            Url = GetUrlService(config.Service);
        }

        #region Static Methods
        /// <summary>
        /// Establece el Tipo de Servicio que se utilizara para la conexion con el WebService de Sunat.
        /// </summary>
        /// <param name="service">Tipo de Servicio (Validos <see cref="ServiceSunatType.Beta"/> y <see cref="ServiceSunatType.Produccion"/>)</param>
        /// <exception cref="ArgumentException">Servicio Invalido</exception>
        /// <returns>Url of service</returns>
        private static string GetUrlService(ServiceSunatType service)
        {
            string url;
            switch (service)
            {
                case ServiceSunatType.Beta:
                    url = Resources.UrlGuiaBeta;
                    break;
                case ServiceSunatType.Produccion:
                    url = Resources.UrlGuia;
                    break;
                default:
                    throw new ArgumentException(@"Servicio Invalido, solo se acepta BETA y Produccion", nameof(service));
            }
            return url;
        }
        #endregion
    }
}
