using System;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Class DocReferenciaType for Summary.
    /// </summary>
    public class DocReferenciaType
    {
        #region Fields
        private string _serieNro;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the documento.
        /// </summary>
        /// <value>The documento.</value>
        /// <exception cref="System.ArgumentException">El documento no puede ser vacio ni tener mas de 13 caracteres</exception>
        public string Documento
        {
            get { return _serieNro; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 13)
                    _serieNro = value;
                else
                    throw new ArgumentException("El documento no puede ser vacio ni tener mas de 13 caracteres", nameof(Documento));
            }
        }
        /// <summary>
        /// Gets or sets the tipo de documento.
        /// </summary>
        /// <value>The tipo documento.</value>
        public TipoDocumentoElectronico TipoDocumento { get; set; }
        #endregion
    }
}
