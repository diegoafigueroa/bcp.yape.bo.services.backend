﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExternalPeopleService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonRequest", Namespace="http://schemas.datacontract.org/2004/07/bcp.yape.bo.services.wcf.DataContracts.Pe" +
        "ople")]
    public partial class PersonRequest : object
    {
        
        private string CellPhoneNumberField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CellPhoneNumber
        {
            get
            {
                return this.CellPhoneNumberField;
            }
            set
            {
                this.CellPhoneNumberField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonResponse", Namespace="http://schemas.datacontract.org/2004/07/bcp.yape.bo.services.wcf.DataContracts.Pe" +
        "ople")]
    public partial class PersonResponse : object
    {
        
        private string CellPhoneNumberField;
        
        private string DocumentNumberField;
        
        private ExternalPeopleService.DocumentTypeEnum DocumentTypeField;
        
        private string LastNameField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CellPhoneNumber
        {
            get
            {
                return this.CellPhoneNumberField;
            }
            set
            {
                this.CellPhoneNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DocumentNumber
        {
            get
            {
                return this.DocumentNumberField;
            }
            set
            {
                this.DocumentNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ExternalPeopleService.DocumentTypeEnum DocumentType
        {
            get
            {
                return this.DocumentTypeField;
            }
            set
            {
                this.DocumentTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentTypeEnum", Namespace="http://schemas.datacontract.org/2004/07/bcp.yape.bo.services.rules.Entities")]
    public enum DocumentTypeEnum : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        IdentityCard = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Passport = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ExternalPeopleService.IPeopleService")]
    public interface IPeopleService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPeopleService/GetPeopleByPhoneNumber", ReplyAction="http://tempuri.org/IPeopleService/GetPeopleByPhoneNumberResponse")]
        System.Threading.Tasks.Task<ExternalPeopleService.PersonResponse[]> GetPeopleByPhoneNumberAsync(ExternalPeopleService.PersonRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public interface IPeopleServiceChannel : ExternalPeopleService.IPeopleService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public partial class PeopleServiceClient : System.ServiceModel.ClientBase<ExternalPeopleService.IPeopleService>, ExternalPeopleService.IPeopleService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PeopleServiceClient() : 
                base(PeopleServiceClient.GetDefaultBinding(), PeopleServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IPeopleService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PeopleServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(PeopleServiceClient.GetBindingForEndpoint(endpointConfiguration), PeopleServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PeopleServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PeopleServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PeopleServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PeopleServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PeopleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<ExternalPeopleService.PersonResponse[]> GetPeopleByPhoneNumberAsync(ExternalPeopleService.PersonRequest request)
        {
            return base.Channel.GetPeopleByPhoneNumberAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPeopleService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IPeopleService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:53679/PeopleService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return PeopleServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IPeopleService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return PeopleServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IPeopleService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IPeopleService,
        }
    }
}
