using ContactManagement.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Abstractions.Services
{
    public interface IContactQueryHandler
    {
        Task<Contact> GetContact(Guid id);
        Task<List<Contact>> GetContacts();
    }
}
