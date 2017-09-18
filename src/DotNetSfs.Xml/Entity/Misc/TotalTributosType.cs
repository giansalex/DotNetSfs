using System;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Clase para especificar una sumatoria de impuesto o impuestos de la venta.
    /// </summary>
    public class TotalTributosType
    {
        #region Field
        private decimal _montopagable;
        private decimal? _montoTotal;
        private decimal? _percent;
        #endregion

        /// <summary>
        /// Código de otros conceptos tributarios, según Catálogo No. 14
        /// /Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformation/sac:Additi onalMonetaryTotal/cbc:ID 
        /// </summary>
        public OtrosConceptosTributarios Id { get; set; }

        /// <summary>
        /// Indica el monto total de cada operacion de la venta, según Catalogo No 14.
        /// /Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformation/sac:Additi onalMonetaryTotal/cbc:PayableAmount
        /// </summary>
        /// <value>The monto pagable.</value>
        /// <exception cref="System.ArgumentException">El campo MontoPagable es obligatorio y no negativo</exception>
        public decimal MontoPagable
        {
            get { return _montopagable; }
            set
            {
                if (value >= 0)
                {
                    _montopagable = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    throw new ArgumentException("El campo MontoPagable es obligatorio y no negativo");
                }
            }
        }

        /// <summary>
        /// Utilizado en Percepciones
        /// </summary>
        /// <value>The monto total.</value>
        /// <exception cref="System.ArgumentException">El campo MontoTotal no debe ser negativo</exception>
        public decimal? MontoTotal
        {
            get
            {
                return _montoTotal;
            }
            set
            {
                if(value.HasValue && value >= 0)
                    _montoTotal = Math.Round(value.Value, 2);
                else
                    throw new ArgumentException("El campo MontoTotal no debe ser negativo");
            }
        }

        /// <summary>
        /// Porcentaje de detracción
        /// </summary>
        /// <value>The porcentaje.</value>
        /// <exception cref="System.ArgumentException">El campo Porcentaje no debe ser negativo</exception>
        public decimal? Porcentaje
        {
            get { return _percent; }
            set
            {
                if (value.HasValue && value >= 0)
                    _percent = Math.Round(value.Value, 2);
                else
                    throw new ArgumentException("El campo Porcentaje no debe ser negativo");
            }
        }
    }
}
