
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Referencia de Importes
    /// </summary>
    public class TotalImporteType
    {
        #region Fields
        private decimal _monto;
        #endregion

        /// <summary>
        /// Código que representa el tipo de importe asociado al item o línea de las boletas de venta o notas de credito y debito relacionadas.
        /// </summary>
        public TipoValorVenta TipoImporte;

        /// <summary>
        /// Identifica el valor del monto referenciado en el elemento TipoImporte
        /// </summary>
        /// <value>The monto.</value>
        /// <exception cref="System.ArgumentException">El campo Monto es obligatorio y debe ser mayor a 0</exception>
        public decimal Monto
        {
            get { return _monto; }
            set {
                if (value >= 0)
                {
                    _monto = decimal.Round(value, 2);
                }
                else
                {
                    throw new System.ArgumentException("El campo Monto es obligatorio y debe ser mayor a 0");
                }
            }
        }
    }
}
