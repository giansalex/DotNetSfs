using System.ServiceModel.Channels;
using System.Xml;

namespace DotNetSfs.Ws.Res
{
    public class SecurityHeader : AddressHeader
    {
        private readonly string _username;
        private readonly string _password;

        public SecurityHeader(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public override string Name => "Security";

        public override string Namespace => "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

        protected override void OnWriteAddressHeaderContents(XmlDictionaryWriter writer)
        {
            writer.WriteStartElement("UsernameToken", Namespace);
            writer.WriteElementString("Username", Namespace, _username);
            writer.WriteElementString("Password", Namespace, _password);
            writer.WriteEndElement();
        }
    }
}
