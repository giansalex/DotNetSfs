using System;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Details
{
    /// <summary>
    /// Item de un Documento para dar de Baja
    /// </summary>
    public class VoidedDetail
    {
        #region Fields
        private string _seriedocumento;
        private string _correlativodocumento;
        private string _reason;
        #endregion

        /// <summary>
        /// Tipo de comprobante que se dara de Baja
        /// </summary>
        public TipoDocumentoElectronico TipoDocumento { get; set; }

        /// <summary>
        /// Serie del Documento a dar de Baja
        /// </summary>
        /// <value>The serie documento.</value>
        /// <exception cref="System.ArgumentException">La longitud del campo SerieDocumento es de un máximo de 4 caracteres, mínimo de 1 caracter</exception>
        public string SerieDocumento
        {
            get { return _seriedocumento; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 4)
                {
                    _seriedocumento = value.Trim();
                }
                else
                {
                    throw new ArgumentException("La longitud del campo SerieDocumento es de un máximo de 4 caracteres, mínimo de 1 caracter");
                }
            }
        }

        /// <summary>
        /// Correlativo del Documento a dar de Baja
        /// </summary>
        /// <value>The correlativo documento.</value>
        /// <exception cref="System.ArgumentException">La longitud del campo CorrelativoDocumento es de un máximo de 8 caracteres y mínimo de 1 caracter.</exception>
        public string CorrelativoDocumento
        {
            get { return _correlativodocumento; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 8)
                {
                    _correlativodocumento = value;
                }
                else
                {
                    throw new ArgumentException("La longitud del campo CorrelativoDocumento es de un máximo de 8 caracteres y mínimo de 1 caracter.");
                }
            }
        }

        /// <summary>
        /// Descripción breve del motivo que generó la baja del documento.
        /// </summary>
        /// <value>The motivo.</value>
        /// <exception cref="System.ArgumentException">El motivo de Baja es obligatorio y no mayor de 100 caracteres.</exception>
        public string Motivo
        {
            get { return _reason; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 100)
                {
                    _reason = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El motivo de Baja es obligatorio y no mayor de 100 caracteres.");
                }
            }
        }
    }
}
