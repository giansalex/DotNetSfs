using System;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Información acerca del Importe Total de Descuentos y Otros Cargos
    /// </summary>
    public class TotalImporteExtType
    {
        #region Fields
        private decimal _monto;
        #endregion

        /// <summary>
        /// Indicador booleano que determina si es un descuento (false) o si es un cargo (true).
        /// </summary>
        public bool Indicador;

        /// <summary>
        /// Monto que supone el descuento o cargo.
        /// </summary>
        /// <value>The monto.</value>
        /// <exception cref="System.ArgumentException">El campo Monto es obligatorio y debe ser mayor a 0</exception>
        public decimal Monto
        {
            get { return _monto; }
            set
            {
                if (value >= 0)
                {
                    _monto = Decimal.Round(value, 2);
                }
                else
                {
                    throw new ArgumentException("El campo Monto es obligatorio y debe ser mayor a 0");
                }
            }
        }
    }
}
