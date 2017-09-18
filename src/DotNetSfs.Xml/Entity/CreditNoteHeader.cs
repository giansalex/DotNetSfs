using DotNetSfs.Xml.Entity.Details;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Documento de Nota de Credito
    /// </summary>
    public class CreditNoteHeader : NotasBase<InvoiceDetail>
    {
        /// <summary>
        /// Código del tipo de nota de crédito. Se utilizará el Catálogo No. 09.
        /// </summary>
        public TipoNotaCreditoElectronica TipoNota;

        /// <summary>
        /// Crea una Instancia para una Nota de Credito
        /// </summary>
        public CreditNoteHeader() { }

        /// <summary>
        /// Crea una Instancia a partir de una Nota Electronica Base.
        /// </summary>
        /// <param name="nota">Nota Base que inicializara la nueva instancia</param>
        public CreditNoteHeader(NotasBase<InvoiceDetail> nota)
        {
            var properties = nota.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var v = prop.GetValue(nota, null);
                if(v != null)
                    prop.SetValue(this,v, null);
            }
            var fields = nota.GetType().GetFields();
            foreach (var field in fields)
            {
                var v = field.GetValue(nota);
                if (v != null)
                    field.SetValue(this, v);
            }

        }
    }
}
