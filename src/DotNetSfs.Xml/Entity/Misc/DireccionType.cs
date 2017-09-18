using System;

namespace DotNetSfs.Xml.Entity.Misc
{
    /// <summary>
    /// Domicilio Fiscal
    /// </summary>
    public class DireccionType
    {
        #region Fields
        private string _ubigueo;
        private string _direccion; 
        private string _zona;
        private string _dep;
        private string _prov;
        private string _distrito;
        private string _codPais;
        #endregion

        /// <summary>
        /// Código de UBIGEO
        /// </summary>
        /// <value>The codigo ubigueo.</value>
        /// <exception cref="System.ArgumentException">Codigo Ubigueo debe tener 6 caracteres</exception>
        public string CodigoUbigueo
        {
            get { return _ubigueo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length == 6)
                {
                    _ubigueo = value;
                }
                else
                {
                    throw new ArgumentException("Codigo Ubigueo debe tener 6 caracteres");
                }
            }
        }
        /// <summary>
        /// Dirección completa y detallada
        /// </summary>
        /// <value>The direccion.</value>
        /// <exception cref="System.ArgumentException">Direccion no puede estar vacio, ni tener mas de 100 caracteres</exception>
        public string Direccion
        {
            get { return _direccion; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 100)
                {
                    _direccion = value;
                }
                else
                {
                    throw new ArgumentException("Direccion no puede estar vacio, ni tener mas de 100 caracteres");
                }
            }
        }
        /// <summary>
        /// Urbanización o Zona
        /// </summary>
        /// <value>The zona.</value>
        /// <exception cref="System.ArgumentException">Urbanizacion o Zona no puede estar vacio, ni tener mas de 25 caracteres</exception>
        public string Zona
        {
            get { return _zona; }
            set { _zona = value;  }
        }
        /// <summary>
        /// Departamento
        /// </summary>
        /// <value>The departamento.</value>
        /// <exception cref="System.ArgumentException">Departamento no puede estar vacio, ni tener mas de 30 caracteres</exception>
        public string Departamento
        {
            get { return _dep; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _dep = value;
                }
                else
                {
                    throw new ArgumentException("Departamento no puede estar vacio, ni tener mas de 30 caracteres");
                }
            }
        }
        /// <summary>
        /// Provincia
        /// </summary>
        /// <value>The provincia.</value>
        /// <exception cref="System.ArgumentException">Provincia no puede estar vacio, ni tener mas de 30 caracteres</exception>
        public string Provincia
        {
            get { return _prov; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _prov = value;
                }
                else
                {
                    throw new ArgumentException("Provincia no puede estar vacio, ni tener mas de 30 caracteres");
                }
            }
        }
        /// <summary>
        /// Distrito
        /// </summary>
        /// <value>The distrito.</value>
        /// <exception cref="System.ArgumentException">Distrito no puede estar vacio, ni tener mas de 30 caracteres</exception>
        public string Distrito
        {
            get { return _distrito; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _distrito = value;
                }
                else
                {
                    throw new ArgumentException("Distrito no puede estar vacio, ni tener mas de 30 caracteres");
                }
            }
        }
        /// <summary>
        /// Código del País
        /// </summary>
        /// <value>The codigo pais.</value>
        /// <exception cref="System.ArgumentException">Codigo de Pais debe tener 2 caracteres</exception>
        public string CodigoPais
        {
            get { return _codPais; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 2)
                {
                    _codPais = value;
                }
                else
                {
                    throw new ArgumentException("Codigo de Pais debe tener 2 caracteres");
                }
            }
        }
        
    }
}
