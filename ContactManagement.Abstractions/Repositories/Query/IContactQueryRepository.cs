using ContactManagement.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Abstractions.Repositories.Query
{
    public interface IContactQueryRepository : IQueryModelRepository<Contact, Guid>
    {
    }
}
