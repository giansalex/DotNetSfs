using System;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Indica una referencia de una guía de remision.
    /// </summary>
    public class GuiaRemisionType
    {
        private string _documentoguiaremisionseriecorrelativo;
        private string _documentoguiaremisiontipodocumento;

        /// <summary>
        /// Serie y Correlativo del documento al que se está referenciando.
        /// </summary>
        /// <value>The numero guia remision.</value>
        /// <exception cref="System.ArgumentException">El campo NumeroGuiaRemision debe estar separado con un '-' y debe tener más de 1 caracter y un máximo de 30</exception>
        public string NumeroGuiaRemision
        {
            get { return _documentoguiaremisionseriecorrelativo; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Contains("-") && value.Length <= 30)
                    {
                        _documentoguiaremisionseriecorrelativo = value.Trim();
                    }
                    else
                    {
                        throw new ArgumentException("El campo NumeroGuiaRemision debe estar separado con un '-' y debe tener más de 1 caracter y un máximo de 30");
                    }
                }
                else
                {
                    _documentoguiaremisionseriecorrelativo = null;
                }
            }
        }

        /// <summary>
        /// Tipo de comprobante que se está referenciando.
        /// </summary>
        /// <value>The identifier tipo guia remision.</value>
        /// <exception cref="System.ArgumentException">El campo IdTipoGuiaRemision debe ser numérico y con un máximo de 2 caracteres.</exception>
        public string IdTipoGuiaRemision
        {
            get { return _documentoguiaremisiontipodocumento; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    int idTipo;
                    if (int.TryParse(value, out idTipo) && value.Trim().Length <= 2)
                    {
                        _documentoguiaremisiontipodocumento = idTipo.ToString("00");
                    }
                    else
                    {
                        throw new ArgumentException(
                            "El campo IdTipoGuiaRemision debe ser numérico y con un máximo de 2 caracteres.");
                    }
                }
                else
                {
                    _documentoguiaremisiontipodocumento = null;
                }
            }
        }
    }
}
