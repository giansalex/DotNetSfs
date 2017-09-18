using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace DotNetSfs.Ws.Security
{
    internal class SessionHeaderBehavior : IEndpointBehavior
    {
        public SessionHeaderBehavior(ClientCredentials credential)
        {
            _credentials = credential;
        }

        private readonly ClientCredentials _credentials;

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new SessionHeaderInspector(_credentials));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }


    }
    internal class SessionHeaderInspector : IClientMessageInspector
    {
        public SessionHeaderInspector(ClientCredentials credential)
        {
            _credentials = credential;
        }

        private readonly ClientCredentials _credentials;

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            request.Headers.Add(new SessionHeader(_credentials));

            return Guid.NewGuid();
        }
    }
    internal class SessionHeader : MessageHeader
    {
        private const string Prefix = "sc";
        public SessionHeader(ClientCredentials credential)
        {
            _credentials = credential;
        }

        private readonly ClientCredentials _credentials;

        public override string Name => "Security";

        public override string Namespace => "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

        protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement(Prefix, Name, Namespace);
            //base.OnWriteStartHeader(writer, messageVersion);
            //writer.WriteXmlnsAttribute("wsse", Namespace);
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("UsernameToken", Namespace);
            writer.WriteElementString("Username", Namespace, _credentials.UserName.UserName);
            writer.WriteElementString("Password", Namespace, _credentials.UserName.Password);
            writer.WriteEndElement();
        }
    }
}
