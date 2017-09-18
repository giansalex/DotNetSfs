using System;
using System.Collections.Generic;
using DotNetSfs.Xml.Entity.Misc;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Entidad Base para los Documentos Nota de Credito y Debito
    /// </summary>
    /// <typeparam name="T">Tipo de Detalle de DocumentLine</typeparam>
    public class NotasBase<T> : SunatDocumentBase<T>
        where T : new()
    {
        #region Fields
        private string _seriedocumento;
        private string _correlativodocumento;
        private string _razonsocialnombreapellidos;
        private string _nroDocCliente;
        private string _codigomoneda;
        private string _idDoc;
        private string _motivo;
        private decimal _total;
        private decimal _totalCargos;
        private string _documentorefseriecorrelativo;
        private decimal _descuentoGlobal;
        #endregion

        /// <summary>
        /// Serie de la Nota Electronica
        /// </summary>
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
                    throw new ArgumentException("La longitud del campo SerieDocumento es de un máximo de 4 caracteres, mínimo de 1 caracter");
            }
        }

        /// <summary>
        /// Correlativo de la Nota Electronica
        /// </summary>
        public string CorrelativoDocumento
        {
            get { return _correlativodocumento; }
            set
            {
                if (Validar(value, i => i <= 8))
                    _correlativodocumento = value.Trim();
                else
                    throw new ArgumentException("La longitud del campo CorrelativoDocumento es de un máximo de 8 caracteres y mínimo de 1 caracter.");
            }
        }

        /// <summary>
        /// Moneda en la que el documento se presenta..
        /// </summary>
        public string CodigoMoneda
        {
            get { return _codigomoneda; }
            set
            {
                if (Validar(value, i => i == 3))
                    _codigomoneda = value.Trim();
                else
                    throw new ArgumentException("El campo CodigoMoneda debe tener 3 caracteres.");
            }
        }

        /// <summary>
        /// Identifica al documento modificado por la nota Electronica. Se consignará en el formato: Serie-Número correlativo de documento.
        /// </summary>
        public string DocumentoRef
        {
            get { return _idDoc; }
            set {
                if (Validar(value, i => i <= 13))
                    _idDoc = value;
                else
                    throw new ArgumentException("La longitud del DocumentoRef no puede estar vacio, ni tener mas de 13 caracteres");
            }
        }

        /// <summary>
        /// Código del tipo de documento modificado
        /// </summary>
        public TipoDocumentoElectronico TipoDocRef;

        /// <summary>
        /// Sustento o descripción del motivo de la nota de electronica. Se debe consignar una descripción
        /// </summary>
        public string Motivo
        {
            get
            {
                return _motivo;
            }
            set {
                if (Validar(value, i => i <= 100))
                    _motivo = value;
                else
                    throw new ArgumentException("La descripcion debe tener de 1 - 100 caracteres");
            }
        }

        /// <summary>
        /// Tipo de documento de identidad del Cliente.
        /// </summary>
        public TipoDocumentoIdentidad TipoDocumentoIdentidadCliente;

        /// <summary>
        /// Indica tipo de documento de identidad del receptor de la nota
        /// </summary>
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
                    throw new ArgumentException("El campo NroDocCliente no debe superar los 15 caracteres");
            }
        }

        /// <summary>
        /// Nombre o Razón Social de la Empresa o Persona a la que se emite el Document.
        /// </summary>
        public string NombreRazonSocialCliente
        {
            get { return _razonsocialnombreapellidos; }
            set
            {
                if (Validar(value, i => i <= 100))
                    _razonsocialnombreapellidos = value.Trim();
                else
                    throw new ArgumentException("El campo NombreRazonSocialCliente debe tener más de 1 caracter y un máximo de 100");
            }
        }

        /// <summary>
        /// Especifica la Direccion del Emisor Electronico
        /// </summary>
        public DireccionType DireccionEmisor;

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
                if (value >= 0)
                    _descuentoGlobal = decimal.Round(value, 2);
                else
                    throw new ArgumentException("El Descuento Global debe ser Mayor que 0.00");
            }
        }

        /// <summary>
        /// Se define como el total de todos los cargos aplicados a nivel de total del documento.
        /// </summary>
        public decimal TotalCargos
        {
            get { return _totalCargos; }
            set
            {
                if(value >= 0)
                    _totalCargos = decimal.Round(value, 2);
                else
                    throw new ArgumentException("Total Cargos no puede ser menor a 0(cero)");
            }
        }

        /// <summary>
        /// Indica el monto de los aportes afectos e inafectos de los impuestos.
        /// </summary>
        public List<TotalTributosType> TotalTributosAdicionales;

        /// <summary>
        /// Resumen de impuestos pertenecientes al total del documento
        /// </summary>
        public List<TotalImpuestosType> Impuesto;

        /// <summary>
        /// Representa el importe total a pagar para el documento.
        /// </summary>
        public decimal Total
        {
            get { return _total; }
            set
            {
                if (value >= 0)
                {
                    _total = decimal.Round(value, 2);
                }
                else
                    throw new ArgumentException("El campo TotalVenta es obligatorio y debe ser mayor que 0.");
            }
        }

        #region Guias de Remision y Otros Docs
        /// <summary>
        /// Tipo de comprobante que se está referenciando.
        /// </summary>
        public TipoDocumentoElectronico DocumentoReferenciaTipoDocumento { get; set; }

        /// <summary>
        /// Serie y Correlativo del documento al que se está referenciando.
        /// </summary>
        public string DocumentoReferenciaNumero
        {
            get { return _documentorefseriecorrelativo; }
            set
            {
                if (Validar(value, i => i <= 30) && value.Contains("-"))
                    _documentorefseriecorrelativo = value.Trim();
                else
                    throw new ArgumentException("El campo DocumentoReferenciaNumero debe estar separado con un '-' y debe tener más de 1 caracter y un máximo de 30");
            }
        }

        /// <summary>
        /// Indica la guía o guías de remision referenciadas al documento electrónico.
        /// </summary>
        public List<GuiaRemisionType> GuiaRemisionReferencia;
        #endregion
    }
}
