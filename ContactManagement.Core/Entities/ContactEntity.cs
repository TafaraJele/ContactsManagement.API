using ContactManagement.Abstractions.Entities;
using System;

namespace ContactManagement.Core.Entities
{
    public class ContactEntity : IContactEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Notes { get; set; }
        public DateTime LastProcessedEventTime => DateTime.Now;
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}
