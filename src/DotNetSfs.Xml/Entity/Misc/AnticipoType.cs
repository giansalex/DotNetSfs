using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Anticipo Class.
    /// </summary>
    public class AnticipoType
    {
        /// <summary>
        /// Gets or sets the tipo document relative.
        /// </summary>
        /// <value>The tipo document relative.</value>
        public DocRelTributario TipoDocRel { get; set; }
        /// <summary>
        /// Gets or sets the Número de la factura o boleta de venta referida en <see cref="RucEmisorDoc"/>.
        /// </summary>
        /// <value>The nro document relative.</value>
        public string NroDocumentRel { get; set; }
        /// <summary>
        /// Gets or sets the RUC del emisor de la factura o boleta de venta que contiene el monto referido en <see cref="MontoAnticipo"/>.
        /// </summary>
        /// <value>The ruc emisor document.</value>
        public string RucEmisorDoc { get; set; }
        /// <summary>
        /// Gets or sets the Monto pre pagado o anticipado.
        /// </summary>
        /// <value>The monto anticipo.</value>
        public decimal MontoAnticipo { get; set; }
    }
}