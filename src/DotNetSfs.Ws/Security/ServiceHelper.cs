using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using DotNetSfs.Ws.Res;

namespace DotNetSfs.Ws.Security
{
    internal class ServiceHelper
    {
        #region Fields & Properties
        private static Binding _objbinding;
        #endregion

        private ServiceHelper() { }

        /// <summary>
        /// Inicializa el Binding por unica vez
        /// </summary>
        private Binding GetBinding()
        {
            //ServicePointManager.UseNagleAlgorithm = true;
            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.CheckCertificateRevocationList = false;
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls; // Activar por tls sino funciona con ssl3

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            var elements = binding.CreateBindingElements();
            return new CustomBinding(elements);
        }

        /// <summary>
        /// Crea una Instancia de Conexion a WebService.
        /// </summary>
        /// <typeparam name="TService">Type del WebService</typeparam>
        /// <param name="config">configuration</param>
        /// <param name="url">url del servicio</param>
        /// <returns>Instancia de conexion</returns>
        public static TService GetService<TService>(SolConfig config, string url)
        {
            if (_objbinding == null) _objbinding = new ServiceHelper().GetBinding();
            
            var credential = new ClientCredentials
            {
                UserName = { UserName = config.Ruc + config.Usuario, Password = config.Clave }
            };
            
            var channel = new ChannelFactory<TService>(_objbinding, new EndpointAddress(url));
            channel.Endpoint.EndpointBehaviors.Add(new SessionHeaderBehavior(credential));
            return channel.CreateChannel();
        }
    }
}
