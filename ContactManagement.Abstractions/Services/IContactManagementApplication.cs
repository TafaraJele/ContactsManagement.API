using ContactManagement.Abstractions.Commands;
using ContactManagement.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactManagement.Abstractions.Services
{
    public interface IContactManagementApplication
    {
        Task<CommandResult<Contact>> CreateContact (CreateContact command);
        Task<CommandResult<Contact>> UpdateContact(UpdateContact command);
        Task<CommandResult<Guid>> DeleteContact(Guid Id);
    }
}
