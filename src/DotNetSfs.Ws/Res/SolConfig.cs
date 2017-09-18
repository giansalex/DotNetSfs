namespace DotNetSfs.Ws.Res
{
    /// <summary>
    /// Credenciales de Clave SOL.
    /// </summary>
    public class SolConfig
    {
        /// <summary>
        /// Gets or sets the ruc.
        /// </summary>
        /// <value>The ruc.</value>
        public string Ruc { get; set; }
        /// <summary>
        /// Gets or sets the usuario.
        /// </summary>
        /// <value>The usuario.</value>
        public string Usuario { get; set; }
        /// <summary>
        /// Gets or sets the clave.
        /// </summary>
        /// <value>The clave.</value>
        public string Clave { get; set; }
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>The service.</value>
        public ServiceSunatType Service { get; set; }
    }
}
