using ContactManagement.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Abstractions.Repositories.Write
{
    public interface IContactRepository : IRepository<IContactEntity, Guid>
    {
    }
}
