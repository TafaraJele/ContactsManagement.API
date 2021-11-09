using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Veneka.Platform.Common;
using Veneka.Platform.Messaging;

namespace ContactManagement.Abstractions.Models
{
    [DataContract]
    public class Contact : IQueryModel, ICommandData, IEventData
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name= "name")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "company")]
        public string Company { get; set; }

        [DataMember(Name = "notes")]
        public string Notes { get; set; }
    }
}
