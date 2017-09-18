using System;
using System.Collections.Generic;
using DotNetSfs.Xml.Entity.Misc;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity.Details
{
    /// <summary>
    /// Item de una Resumen Diario.
    /// </summary>
    public class SummaryDetail
    {
        #region Fields
        private string _documento;
        private string _nroDocCliente;
        private string _seriedocumento;
        private string _initDoc;
        private string _endDoc;
        #endregion

        /// <summary>
        /// Tipo de documento del rango a informar
        /// </summary>
        public TipoDocumentoElectronico TipoDocumento;

        /// <summary>
        /// Gets or sets the documento(Serie-Correlativo).
        /// </summary>
        /// <value>The documento.</value>
        /// <exception cref="ArgumentException">La longitud de Documento no puede estar vacio, ni tener más 13 caracteres</exception>
        public string Documento
        {
            get { return _documento; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 13)
                {
                    _documento = value.Trim();
                }
                else
                {
                    throw new ArgumentException("La longitud de Documento no puede estar vacio, ni tener más 13 caracteres");
                }
            }
        }

        /// <summary>
        /// Serie a la que pertenece el rango referenciando.
        /// </summary>
        /// <value>The serie documento.</value>
        /// <exception cref="System.ArgumentException">La longitud de SerieDocumento no puede estar vacio, ni tener mas 4 caracteres</exception>
        public string SerieDocumento
        {
            get { return _seriedocumento; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 4)
                {
                    _seriedocumento = value.Trim();
                }
                else
                {
                    throw new ArgumentException("La longitud de SerieDocumento no puede estar vacio, ni tener mas 4 caracteres");
                }
            }
        }

        /// <summary>
        /// Tipo de documento de identidad del adquiriente o usuario.
        /// </summary>
        public TipoDocumentoIdentidad TipoDocumentoIdentidadCliente { get; set; }

        /// <summary>
        /// Numero de Documento de identidad del adquiriente o usuario.
        /// </summary>
        /// <value>The nro document cliente.</value>
        /// <exception cref="System.ArgumentException">El campo NroDocCliente no debe superar los 15 caracteres</exception>
        public string NroDocCliente
        {
            get { return _nroDocCliente; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length <= 20)
                {
                    _nroDocCliente = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo NroDocCliente no debe superar los 20 caracteres");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Estado del item.
        /// </summary>
        /// <value>The estado.</value>
        public EstadoResumen Estado { get; set; }

        /// <summary>
        /// Lo Documento que modifica (Boletas) usado en NCR y NDB.
        /// </summary>
        public List<DocReferenciaType> Referencia;

        /// <summary>
        /// Número correlativo del documento inicial del rango
        /// </summary>
        /// <value>The nro correlativo inicial.</value>
        /// <exception cref="System.ArgumentException">La longitud de NroCorrelativoInicial no puede estar vacio, ni tener mas de 8 caracteres</exception>
        public string NroCorrelativoInicial
        {
            get { return _initDoc; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 8)
                {
                    _initDoc = value;
                }
                else
                {
                    throw new ArgumentException("La longitud de NroCorrelativoInicial no puede estar vacio, ni tener mas de 8 caracteres");
                }
            }
        }

        /// <summary>
        /// Número correlativo del documento final del rango
        /// </summary>
        /// <value>The nro correlativo final.</value>
        /// <exception cref="System.ArgumentException">La longitud de NroCorrelativoFinal no puede estar vacio, ni tener mas de 8 caracteres</exception>
        public string NroCorrelativoFinal
        {
            get { return _endDoc; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 8)
                {
                    _endDoc = value;
                }
                else
                {
                    throw new ArgumentException("La longitud de NroCorrelativoFinal no puede estar vacio, ni tener mas de 8 caracteres");
                }
            }
        }

        /// <summary>
        /// En este elemento se consignarán Asocia cada repetición a un determinado importe expresado para el item de comprobantes de pago.
        /// </summary>
        public List<TotalImporteType> Importe;

        /// <summary>
        /// Corresponde al total de otros cargos cobrados al adquirente o usuario y que no forman parte del(os) valor(es) de venta, pero si se incluyen al importe total de la operación.
        /// </summary>
        public List<TotalImporteExtType> OtroImporte;

        /// <summary>
        /// Muestra la información relacionada con los impuestos.
        /// </summary>
        public List<TotalImpuestosType> Impuesto;
    }
}
