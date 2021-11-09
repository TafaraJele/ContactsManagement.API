using ContactManagement.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Veneka.Platform.Messaging;

namespace ContactManagement.Abstractions.Commands
{
    public class CreateContact : ICommand<Contact>
    {
        public CreateContact()
        {
            CommandId = Guid.NewGuid();
            CommandTime = DateTime.Now.ToLocalTime();
            Name = GetType().Name.ToLower();
        }
        public string UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CommandTime { get; set; }
        public Guid CommandId { get; set; }
        public Contact CommandData { get; set; }
        public string Name { get; set; }

    }
}
