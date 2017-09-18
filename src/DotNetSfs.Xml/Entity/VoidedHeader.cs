using System;
using DotNetSfs.Xml.Entity.Details;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Documento para dar de Baja
    /// </summary>
    public class VoidedHeader : SunatDocumentBase<VoidedDetail>
    {
        #region Fields
        private string _correlativoDoc;
        #endregion

        /// <summary>
        /// Numero correlativo del archivo. Este campo es variante, se espera un mínimo de 1 y máximo de 5.
        /// </summary>
        /// <value>The correlativo archivo.</value>
        /// <exception cref="System.ArgumentException">El correlativo debe tener de 1 - 5 caracteres</exception>
        public string CorrelativoArchivo
        {
            get { return _correlativoDoc; }
            set
            {
                if (Validar(value, i => i <= 5))
                {
                    _correlativoDoc = value.Trim();
                }
                else
                    throw new ArgumentException("El correlativo debe tener de 1 - 5 caracteres");
            }
        }
    }
}
