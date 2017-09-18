using System;
using System.Collections.Generic;
using DotNetSfs.Xml.Entity.Details;
using DotNetSfs.Xml.Entity.Misc;
using FacturacionElectronica.GeneradorXml.Enums;
using Ubl.V2.Pe.common;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Documento para Emitir Factura.
    /// </summary>
    public class InvoiceHeader : SunatDocumentBase<InvoiceDetail>
    {
        #region Fields
        private string _seriedocumento;
        private string _correlativodocumento;
        private string _razonsocialnombreapellidos;
        private string _nroDocCliente;
        private string _codigomoneda;
        private string _documentorefseriecorrelativo;
        private decimal _totalventa;
        private decimal _descuentoGlobal;
        #endregion
        
        /// <summary>
        /// Tipo de comprobante que se está emitiendo.
        /// </summary>
        public TipoDocumentoElectronico TipoDocumento { get; set; }

        /// <summary>
        /// Id del Tipo de documento de identidad del Cliente.
        /// </summary>
        public TipoDocumentoIdentidad TipoDocumentoIdentidadCliente { get; set; }

        /// <summary>
        /// Tipo de comprobante que se está referenciando.
        /// </summary>
        public TipoDocumentoElectronico DocumentoReferenciaTipoDocumento { get; set; }

        /// <summary>
        /// Serie del Documento de Venta
        /// </summary>
        /// <value>The serie documento.</value>
        /// <exception cref="System.ArgumentException">La longitud del campo SerieDocumento es de un máximo de 4 caracteres, mínimo de 1 caracter</exception>
        public string SerieDocumento
        {
            get { return _seriedocumento; }
            set
            {
                if (Validar(value, i => i <= 4))
                {
                    _seriedocumento = value.Trim();
                }
                else
                {
#line 54
                    throw new ArgumentException("La longitud del campo SerieDocumento es de un máximo de 4 caracteres, mínimo de 1 caracter");
                }
            }
        }

        /// <summary>
        /// Correlativo del Documento de Venta
        /// </summary>
        /// <value>The correlativo documento.</value>
        /// <exception cref="System.ArgumentException">La longitud del campo CorrelativoDocumento es de un máximo de 8 caracteres y mínimo de 1 caracter.</exception>
        public string CorrelativoDocumento
        {
            get { return _correlativodocumento; }
            set
            {
                if (Validar(value, i => i <= 8))
                {
                    _correlativodocumento = value.Trim();
                }
                else
                {
                    throw new ArgumentException("La longitud del campo CorrelativoDocumento es de un máximo de 8 caracteres y mínimo de 1 caracter.");
                }
            }
        }

        /// <summary>
        /// Nombre o Razón Social de la Empresa o Persona a la que se emite el comprobante.
        /// </summary>
        /// <value>The nombre razon social cliente.</value>
        /// <exception cref="System.ArgumentException">El campo NombreRazonSocialCliente debe tener más de 1 caracter y un máximo de 100</exception>
        public string NombreRazonSocialCliente
        {
            get { return _razonsocialnombreapellidos; }
            set
            {
                if (Validar(value, i => i <= 100))
                {
                    _razonsocialnombreapellidos = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo NombreRazonSocialCliente debe tener más de 1 caracter y un máximo de 100");
                }
            }
        }

        /// <summary>
        /// Numero de Documento de la empresa o persona a la que se está emitiendo el comprobante.
        /// </summary>
        /// <value>The nro document cliente.</value>
        /// <exception cref="System.ArgumentException">El campo NroDocCliente no debe superar los 15 caracteres</exception>
        public string NroDocCliente
        {
            get { return _nroDocCliente; }
            set
            {
                if (Validar(value, i => i <= 15))
                {
                    _nroDocCliente = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo NroDocCliente no debe superar los 15 caracteres");
                }
            }
        }

        /// <summary>
        /// Código internacional de la moneda del comprobante de venta.
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
                    _codigomoneda = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo CodigoMoneda debe tener 3 caracteres.");
                }
            }
        }

        /// <summary>
        /// Serie y Correlativo del documento al que se está referenciando.
        /// </summary>
        /// <value>The documento referencia numero.</value>
        /// <exception cref="System.ArgumentException">El campo DocumentoReferenciaNumero debe estar separado con un '-' y debe tener más de 1 caracter y un máximo de 30</exception>
        public string DocumentoReferenciaNumero
        {
            get { return _documentorefseriecorrelativo; }
            set
            {
                if (Validar(value, i => i <= 30) && value.Contains("-"))
                {
                    _documentorefseriecorrelativo = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo DocumentoReferenciaNumero debe estar separado con un '-' y debe tener más de 1 caracter y un máximo de 30");
                }
            }
        }

        /// <summary>
        /// Descuento Global aplicado al total de Venta
        /// </summary>
        /// <value>The descuento global.</value>
        /// <exception cref="System.ArgumentException">El Descuento Global debe ser Mayor que 0.00</exception>
        public decimal DescuentoGlobal
        {
            get { return _descuentoGlobal; }
            set
            {
                if (value > 0)
                {
                    _descuentoGlobal = decimal.Round(value, 2);
                }
                else
                {
                    throw new ArgumentException("El Descuento Global debe ser Mayor que 0.00");
                }
            }
        }

        /// <summary>
        /// Indica el monto total de la venta.
        /// </summary>
        /// <value>The total venta.</value>
        /// <exception cref="System.ArgumentException">El campo TotalVenta es obligatorio</exception>
        public decimal TotalVenta
        {
            get { return _totalventa; }
            set
            {
                if (value >= 0)
                {
                    _totalventa = decimal.Round(value, 2);
                }
                else
                {
                    throw new ArgumentException("El campo TotalVenta es obligatorio");
                }
            }
        }

        /// <summary>
        /// Especifica la Direccion del Emisor Electronico
        /// </summary>
        public DireccionType DireccionEmisor;

        /// <summary>
        /// Indica la guía o guías de remision referenciadas al documento electrónico.
        /// </summary>
        public List<GuiaRemisionType> GuiaRemisionReferencia;

        /// <summary>
        /// Indica el monto de los aportes afectos e inafectos de los impuestos.
        /// </summary>
        public List<TotalTributosType> TotalTributosAdicionales;

        /// <summary>
        /// Resumen de impuestos pertenecientes al total del comprobante electronico.
        /// </summary>
        public List<TotalImpuestosType> Impuesto;

        /// <summary>
        /// Codigos de la Informacion Adicional como: Leyendas, Numero de registro MTC,Punto de origen y destino, etc.
        /// Ver Catalogo Nro. 15
        /// </summary>
        public List<AdditionalPropertyType> InfoAddicional;
    }
}
