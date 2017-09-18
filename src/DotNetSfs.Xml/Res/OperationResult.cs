namespace DotNetSfs.Xml.Res
{
    /// <summary>
    /// Clase que sirve para contener y transportar los errores que pueden surgir en los diferentes métodos hasta la capa de la interfaz.
    /// </summary>
    internal class OperationResult
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Almacena el nombre del error.
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Almacena el inner exception de la excepción.
        /// </summary>
        public string InnerException { get; set; }
        /// <summary>
        /// Almacena la ubicacion donde ocurrio el error.
        /// </summary>
        public string Target { get; set; }
    }
}
