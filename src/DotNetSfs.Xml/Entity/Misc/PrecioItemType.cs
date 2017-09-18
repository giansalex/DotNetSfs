using System;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Precios de Venta con codigo.
    /// </summary>
    public class PrecioItemType
    {
        private decimal _precio;

        /// <summary>
        /// Tipo de Precio: Precio Unitario o precio Referencial
        /// </summary>
        public TipoPrecioVenta TipoDePrecio { set; get; }

        /// <summary>
        /// Monto del valor
        /// </summary>
        /// <value>The monto.</value>
        /// <exception cref="System.ArgumentException">El campo Monto del Precio Item debe ser mayor igual a 0.</exception>
        public decimal Monto
        {
            get { return _precio; }
            set
            {
                if (value >= 0)
                    //Se le redondea a un máximo de 2 decimales.
                    _precio = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    throw new ArgumentException("El campo Monto del Precio Item debe ser mayor igual a 0.");
            }
        }
    }
}
