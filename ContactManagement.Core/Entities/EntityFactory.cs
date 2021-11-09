using ContactManagement.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Core.Entities
{
    public class EntityFactory
    {

        public static IContactEntity CreateContact()
        {
            return new ContactEntity
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
