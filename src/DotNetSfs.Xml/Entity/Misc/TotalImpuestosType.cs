using System;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Clase para especificar una sumatoria de impuesto o impuestos de la venta.
    /// </summary>
    public class TotalImpuestosType
    {
        private decimal _montopagable;

        /// <summary>
        /// Indica el código del tipo de afectacion la operacion, según Catálogo No. xx
        /// /Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformation/sac:Additi onalMonetaryTotal/cbc:ID 
        /// </summary>
        public TipoAfectacionIgv? TipoAfectacion { get; set; }

        /// <summary>
        /// Sistema de ISC
        /// </summary>
        public TipoSistemaIsc? TipoIsc { set; get; }

        /// <summary>
        /// Indica el código del impuesto de la operacion, según Catálogo No. 14
        /// /Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformation/sac:Additi onalMonetaryTotal/cbc:ID 
        /// </summary>
        public TipoTributo TipoTributo { get; set; }

        /// <summary>
        /// Indica el monto del impuesto de la operacion, según Catalogo No 14.
        /// /Invoice/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/sac:AdditionalInformation/sac:Additi onalMonetaryTotal/cbc:PayableAmount
        /// </summary>
        /// <value>The monto.</value>
        /// <exception cref="System.ArgumentException">El campo Monto es obligatorio y debe ser mayor a 0</exception>
        public decimal Monto
        {
            get { return _montopagable; }
            set
            {
                if (value >= 0)
                {
                    _montopagable = Math.Round(value, 2);
                }
                else
                {
                    throw new ArgumentException("El campo Monto es obligatorio y debe ser mayor a 0");
                }
            }
        }
    }
}
