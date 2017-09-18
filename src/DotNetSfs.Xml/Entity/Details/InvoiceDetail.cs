using System;
using System.Collections.Generic;
using DotNetSfs.Xml.Entity.Misc;

namespace DotNetSfs.Xml.Entity.Details
{
    /// <summary>
    /// Item de un Comprobante Electronico
    /// </summary>
    public class InvoiceDetail
    {
        #region Fields
        private string _codigoproducto;
        private string _unidadmedida;
        private decimal _cantidad;
        private string _descripcionproducto;
        private decimal _preciounitario;
        private decimal _valorventaproducto;
        #endregion

        /// <summary>
        /// Especifica el código del producto.
        /// </summary>
        /// <value>The codigo producto.</value>
        /// <exception cref="System.ArgumentException">El campo CodigoProducto debe tener como máximo 30 caracteres.</exception>
        public string CodigoProducto
        {
            get { return _codigoproducto; }
            set { _codigoproducto = value; }
        }

        /// <summary>
        /// Especifica la unidad de medida, según el estándar internacional, del producto.
        /// </summary>
        /// <value>The unidad medida.</value>
        /// <exception cref="System.ArgumentException">El campo UnidadMedida debe tener como máximo 3 caracteres.</exception>
        public string UnidadMedida
        {
            get { return _unidadmedida; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 3)
                {
                    _unidadmedida = value.Trim();
                }
                else
                    throw new ArgumentException("El campo UnidadMedida debe tener como máximo 3 caracteres.");
            }
        }

        /// <summary>
        /// Cantidad ingresada de ese producto en el comprobante.
        /// </summary>
        /// <value>The cantidad.</value>
        /// <exception cref="System.ArgumentException">El campo Cantidad debe ser mayor a 0.</exception>
        public decimal Cantidad
        {
            get { return _cantidad; }
            set
            {
                if (value > 0)
                {
                    //Se le redondea a un máximo de 3 decimales.
                    _cantidad = Math.Round(value, 3, MidpointRounding.AwayFromZero);
                }
                else
                    throw new ArgumentException("El campo Cantidad debe ser mayor a 0.");
            }
        }

        /// <summary>
        /// Detalla el nombre o descripción del producto o servicio brindado.
        /// </summary>
        /// <value>The descripcion producto.</value>
        /// <exception cref="System.ArgumentException">El campo CodigoProducto debe tener como máximo 250 caracteres.</exception>
        public string DescripcionProducto
        {
            get { return _descripcionproducto; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 250)
                {
                    _descripcionproducto = value.Trim();
                }
                else
                    throw new ArgumentException("El campo DescripcionProducto debe tener como máximo 250 caracteres.");
            }
        }

        /// <summary>
        /// Valor o Monto Unitario del Producto. Este importe no incluye los
        /// tributos (IGV, ISC y otros Tributos) ni los cargos globales
        /// </summary>
        /// <value>The precio unitario.</value>
        /// <exception cref="System.ArgumentException">El campo PrecioUnitario debe ser positivo.</exception>
        public decimal PrecioUnitario
        {
            get { return _preciounitario; }
            set
            {
                if (value >= 0)
                {
                    //Se le redondea a un máximo de 2 decimales.
                    _preciounitario = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                }
                else
                    throw new ArgumentException("El campo PrecioUnitario debe ser positivo.");
            }
        }

        /// <summary>
        /// Importe total de la venta.
        /// </summary>
        /// <value>The valor venta.</value>
        /// <exception cref="System.ArgumentException">El campo ValorVenta debe ser positivo.</exception>
        public decimal ValorVenta
        {
            get { return _valorventaproducto; }
            set
            {
                if (value >= 0)
                {
                    //Se le redondea a un máximo de 2 decimales.
                    _valorventaproducto = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                }
                else
                    throw new ArgumentException("El campo ValorVenta debe ser positivo.");
            }
        }

        /// <summary>
        /// Indica el Tipo de Precio Unitario y/o Referencial del item.
        /// </summary>
        public List<PrecioItemType> PrecioAlternativos;

        /// <summary>
        /// Codigo del Impuesto
        /// Catálogo No. 05
        /// </summary>
        public List<TotalImpuestosType> Impuesto;
    }
}
