using System;
using System.Collections.Generic;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Base para Documento para Sunat
    /// </summary>
    /// <typeparam name="T">Tipo del Detalle del Documento</typeparam>
    public class SunatDocumentBase <T> 
        where T: new()
    {
        #region Fields
        private string _rucemisor;
        private string _razonsocialemisor;
        private string _nombrecomercialemisor;
        private DateTime _fechaEmision;
        #endregion

        /// <summary>
        /// Tipo de documento de identidad del Emisor.
        /// </summary>
        public TipoDocumentoIdentidad TipoDocumentoIdentidadEmisor;

        /// <summary>
        /// RUC de la empresa
        /// </summary>
        /// <value>The ruc emisor.</value>
        /// <exception cref="ArgumentException">El campo RucEmisor debe tener 11 caracteres.</exception>
        public string RucEmisor
        {
            get { return _rucemisor; }
            set
            {
                if (Validar(value, i => i == 11))
                {
                    _rucemisor = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo RucEmisor debe tener 11 caracteres.");
                }
            }
        }

        /// <summary>
        /// Nombre o Razón Social de la Empresa emisora.
        /// </summary>
        /// <value>The nombre razon social emisor.</value>
        /// <exception cref="ArgumentException">El campo NombreRazonSocialEmisor debe tener más de 1 caracter y un máximo de 100</exception>
        public string NombreRazonSocialEmisor
        {
            get { return _razonsocialemisor; }
            set
            {
                if (Validar(value, i => i<= 100))
                {
                    _razonsocialemisor = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo NombreRazonSocialEmisor debe tener más de 1 caracter y un máximo de 100");
                }
            }
        }

        /// <summary>
        /// Nombre Comercial de la Empresa emisora
        /// </summary>
        /// <value>The nombre comercial emisor.</value>
        /// <exception cref="ArgumentException">El campo NombreComercialEmisor debe tener más de 1 caracter y un máximo de 100</exception>
        public string NombreComercialEmisor
        {
            get { return _nombrecomercialemisor; }
            set
            {
                if (Validar(value, i => i <= 100))
                {
                    _nombrecomercialemisor = value.Trim();
                }
                else
                {
                    throw new ArgumentException("El campo NombreComercialEmisor debe tener más de 1 caracter y un máximo de 100");
                }
            }
        }

        /// <summary>
        /// Fecha en la que se está emitiendo el documento.
        /// </summary>
        /// <value>The fecha emision.</value>
        /// <exception cref="ArgumentException">El campo FechaEmision debe ser del año actual.</exception>
        public DateTime FechaEmision
        {
            get { return _fechaEmision; }
            set
            {
                if (value.Date.Year == DateTime.Today.Year)
                {
                    _fechaEmision = value;
                }
                else
                {
                    throw new ArgumentException("El campo FechaEmision debe ser del año actual.");
                }
            }
        }

        /// <summary>
        /// Detalles del Documento
        /// </summary>
        public List<T> DetallesDocumento;

        /// <summary>
        /// Validador de Campo String
        /// </summary>
        /// <param name="valor">cadena a validar</param>
        /// <param name="predicate">predicado que debe cumplirse</param>
        /// <returns></returns>
        protected bool Validar(string valor, Func<int, bool> predicate)
        {
            return !string.IsNullOrWhiteSpace(valor) && predicate(valor.Trim().Length);
        }
    }
}
