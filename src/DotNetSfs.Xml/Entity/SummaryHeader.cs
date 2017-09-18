
using DotNetSfs.Xml.Entity.Details;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Resumen Diario de Boletas y Notas Electronicas asociadas.
    /// </summary>
    public class SummaryHeader : SunatDocumentBase<SummaryDetail>
    {
        #region Fields
        private string _correlativoDoc;
        private string _codigomoneda;
        #endregion

        /// <summary>
        /// Código internacional de la moneda utilizada
        /// </summary>
        /// <value>The codigo moneda.</value>
        /// <exception cref="System.ArgumentException">El campo CodigoMoneda debe tener 3 caracteres.</exception>
        public string CodigoMoneda
        {
            get { return _codigomoneda; }
            set
            {
                if (Validar(value, i => i == 3))
                {
                    _codigomoneda = value;
                }
                else
                {
                    throw new System.ArgumentException("El campo CodigoMoneda debe tener 3 caracteres.");
                }
            }
        }

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
                {
                    throw new System.ArgumentException("El correlativo debe tener de 1 - 5 caracteres");
                }
            }
        }
    }
}
