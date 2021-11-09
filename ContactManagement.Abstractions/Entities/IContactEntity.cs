using System;
using System.Collections.Generic;
using System.Text;
using Veneka.Platform.Common;

namespace ContactManagement.Abstractions.Entities
{
    public interface IContactEntity : IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Notes { get; set; }
    }
}
