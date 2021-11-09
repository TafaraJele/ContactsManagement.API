using ContactManagement.Abstractions.Models;
using ContactManagement.Abstractions.Repositories.Query;
using ContactManagement.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactManagement.Core.Services
{
    public class ContactQueryHandler : IContactQueryHandler
    {
        private readonly IContactQueryRepository _contactQueryRepository;

        public ContactQueryHandler(IContactQueryRepository contactQueryRepository)
        {
            _contactQueryRepository = contactQueryRepository;
        }
        public Task<Contact> GetContact(Guid id)
        {            
            var contact = _contactQueryRepository.LoadModelAsync(id);

            return contact;
        }

        public async Task<List<Contact>> GetContacts()
        {
            var searchParameter = new List<SearchParameter>
            {
                 new SearchParameter { Name = "", Value = ""
            } };

            var contacts = await _contactQueryRepository.FindModelsAsync(searchParameter) as List<Contact>;

            return contacts;
        }
    }
}
