//
// This example signs an XML file using an
// envelope signature. It then verifies the 
// signed XML.
//
// You must have a certificate with a subject name
// of "CN=XMLDSIG_Test" in the "My" certificate store. 
//
// Run the following command to create a certificate
// and place it in the store.
// makecert -r -pe -n "CN=XMLDSIG_Test" -b 01/01/2005 -e 01/01/2010 -sky signing -ss my

using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace DotNetSfs.Xml
{
    /// <summary>
    /// Class para Firmar XML
    /// </summary>
    public static class XmlSignatureProvider
    {
        /// <summary>
        /// The Ext Namespace-
        /// </summary>
        private const string Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";

        /// <summary>
        /// Firma el Xml y lo guarda como un nuevo archvo xml.
        /// </summary>
        /// <param name="doc">Documento XML object</param>
        /// <param name="cert">Certificado X509</param>
        public static void SignXmlFile(XmlDocument doc, X509Certificate2 cert)
        {
            doc.PreserveWhitespace = true;
            var signedXml = new SignedXml(doc) {SigningKey = cert.PrivateKey};
            var reference = new Reference {Uri = ""};

            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            var keyInfo = new KeyInfo();
            var x509KeyInfo = new KeyInfoX509Data(cert);
            //x509KeyInfo.AddSubjectName(cert.SubjectName.Name);
            keyInfo.AddClause(x509KeyInfo);
            signedXml.KeyInfo = keyInfo;
            signedXml.Signature.Id = "SignatureSP";
            signedXml.ComputeSignature();

            var nameSpace = new XmlNamespaceManager(doc.NameTable);
            nameSpace.AddNamespace("ext", Ext);
            var signNodes = doc.SelectNodes("//ext:ExtensionContent", nameSpace);
            var xmlDigitalSignature = signedXml.GetXml();
            xmlDigitalSignature.Prefix = "ds";

            if (signNodes.Count > 0)
                signNodes[signNodes.Count - 1].AppendChild(doc.ImportNode(xmlDigitalSignature, true)); // Firma y agrega al doc XML
            
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "ISO-8859-1", "no");
            doc.ReplaceChild(xmlDeclaration, doc.FirstChild);
        }

        public static bool VerifyXmlFile(string fileName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            var signedXml = new SignedXml(xmlDocument);
            var nodeList = xmlDocument.GetElementsByTagName("ds:Signature");
            var certNode = xmlDocument.GetElementsByTagName("ds:X509Certificate");
            if (certNode.Count == 0)
                certNode = xmlDocument.GetElementsByTagName("X509Certificate");

            try
            {
                var cert = new X509Certificate2(Convert.FromBase64String(certNode[0].InnerText));
                signedXml.LoadXml((XmlElement)nodeList[0]);

                return signedXml.CheckSignature(cert, true);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}