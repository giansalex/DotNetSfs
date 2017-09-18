using DotNetSfs.Ws.Properties;
using DotNetSfs.Ws.Res;

namespace DotNetSfs.Ws
{
    /// <summary>
    /// Facturación Electrónica.
    /// </summary>
    public class FeService : BaseSunat
    {
        /// <inheritdoc />
        public FeService(SolConfig config) : base(config)
        {
            Url = GetUrlService(config.Service);
        }

        #region Static Methods
        /// <summary>
        /// Establece el Tipo de Servicio que se utilizara para la conexion con el WebService de Sunat.
        /// </summary>
        /// <param name="service">Tipo de Servicio al que se conectara</param>
        private static string GetUrlService(ServiceSunatType service)
        {
            string url;
            switch (service)
            {
                case ServiceSunatType.Produccion:
                    url = Resources.UrlProduccion;
                    break;
                case ServiceSunatType.Homologacion:
                    url = Resources.UrlHomologacion;
                    break;
                default:
                    url = Resources.UrlBeta;
                    break;
            }
            return url;
        }
        #endregion
    }
}
