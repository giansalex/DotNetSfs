using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DotNetSfs.Ws.Properties;

namespace DotNetSfs.Ws.Res
{
    internal static class ProcessXml
    {
        /// <summary>
        /// The common agregate Namespace.
        /// </summary>
        private const string CommonAgregate = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        /// <summary>
        /// The common basic Namespace.
        /// </summary>
        private const string CommonBasic = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";

        /// <summary>
        /// Obtiene al descripcion de un Error con su CodeName.
        /// </summary>
        /// <param name="code">codigo de error</param>
        /// <returns>Descripcion del error</returns>
        public static string GetDescriptionError(string code)
        {
            string resp = null;
            try
            {
                code = System.Text.RegularExpressions.Regex.Replace(code, @"[^\d]", "");
                if (!int.TryParse(code, out var val))
                {
                    return string.Empty;
                }
                code = val.ToString();
                using (TextReader reader= new StringReader(Resources.ListaDeErrores))
                {
                    var errors = XElement.Load(reader);
                    var filter = (from x in errors.Elements()
                        let o = x.Attribute("code")
                        where o != null && o.Value.Equals(code)
                                  select x.Value).ToList();
                    if (filter.Any())
                        resp = filter.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

        /// <summary>
        /// Obtiene la Entidad ApplicationResponse
        /// </summary>
        /// <param name="contentXml">The content XML.</param>
        /// <returns>Entidad UBL</returns>
        public static CdrResponse GetAppResponse(Stream contentXml)
        {
            var xml = XElement.Load(contentXml);
            XNamespace cac = CommonAgregate;
            XNamespace cbc = CommonBasic;
            var document = xml.Element(cac + "DocumentResponse");
            var resp = document?.Element(cac + "Response");
            var r = new CdrResponse();
            if (resp != null)
            {
                r.Id = resp.Element(cbc + "ReferenceID")?.Value;
                r.Codigo = resp.Element(cbc + "ResponseCode")?.Value;
                r.Descripcion = resp.Element(cbc + "Description")?.Value;
            }
            var notes = xml.Elements(cbc + "Note");
            r.Notas = notes.Select(g => g.Value).ToArray();

            return r;
        }
    }
}
