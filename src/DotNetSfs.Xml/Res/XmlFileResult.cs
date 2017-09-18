namespace DotNetSfs.Xml.Res
{
    /// <summary>
    /// Class XmlFileResult
    /// </summary>
    public class XmlFileResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="XmlFileResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }
        /// <summary>
        /// Nombre del archivo XML sin extension.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Contenido del Archivo Xml.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string Error { get; set; }
    }
}
