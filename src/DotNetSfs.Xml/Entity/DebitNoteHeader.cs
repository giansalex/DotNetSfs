
using DotNetSfs.Xml.Entity.Details;
using FacturacionElectronica.GeneradorXml.Enums;

namespace DotNetSfs.Xml.Entity
{
    /// <summary>
    /// Documento de Nota de Debito
    /// </summary>
    public class DebitNoteHeader : NotasBase<InvoiceDetail>
    {
        /// <summary>
        /// Código que representa el tipo de nota de débito utilizada. Se utilizará el Catálogo No. 10.
        /// </summary>
        public TipoNotaDebitoElectronica TipoNota;

        /// <summary>
        /// Crea una Instancia para una Nota de Debito
        /// </summary>
        public DebitNoteHeader() { }

        /// <summary>
        /// Crea una Instancia a partir de una Nota Electronica Base.
        /// </summary>
        /// <param name="nota">Nota Base que inicializara la nueva instancia</param>
        public DebitNoteHeader(NotasBase<InvoiceDetail> nota)
        {
            var properties = nota.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var v = prop.GetValue(nota, null);
                if (v != null)
                    prop.SetValue(this, v, null);
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
