using ContactManagement.Abstractions.Entities;
using ContactManagement.Abstractions.Enums;
using ContactManagement.Abstractions.Repositories.Write;
using ContactManagement.Abstractions.Settings;
using ContactManagement.Core.Entities;
using ContactManagement.Infrastructure.Data.Data.Mongo.Read;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;
using Veneka.Platform.Common.Enums;

namespace ContactManagement.Infrastructure.Data.Data.Mongo.Write
{
    public class ContactRepository : IContactRepository
    {
        private MongoContext _context;
        public ContactRepository(IOptions<ApplicationSettings> config)
        {
            _context = new MongoContext(config);
        }
        public ContactRepository(string serverName, string databaseName)
        {
            _context = new MongoContext(serverName, databaseName);
        }

        public async Task<IEnumerable<IContactEntity>> FindAggregatesAsync(List<SearchParameter> searchParameters, FilterType filterType)
        {
            FilterDefinition<ContactEntity> filter = Builders<ContactEntity>.Filter.Ne("isDeleted", true);
            foreach (var parameter in searchParameters.Where(
                    parameter => !string.IsNullOrEmpty(parameter.Name) && !string.IsNullOrEmpty(parameter.Value)))
            {
                var validParameter = Enum.TryParse(parameter.Name.ToUpper(), out SearchOptions option);
                if (!validParameter)
                {
                    continue;
                }
                switch (option)
                {
                    case SearchOptions.ID:
                        {
                            if (filter == null)
                            {
                                filter = Builders<ContactEntity>.Filter.Eq("_id", parameter.Value);
                            }
                            else
                            {
                                filter = Builders<ContactEntity>.Filter.Eq("_id", parameter.Value) & filter;
                            }

                        }
                        break;

                }
            }


            if (filter == null) throw new ArgumentException("Invalid search parameters specified");
            List<ContactEntity> result = await _context.Contacts.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IContactEntity> LoadAggregateAsync(Guid id)
        {
            FilterDefinition<ContactEntity> filter = Builders<ContactEntity>.Filter.Ne("isDeleted", true);

            filter = Builders<ContactEntity>.Filter.Eq("_id", id) & filter;
            if (filter == null)
            {
                throw new ArgumentException("Invalid agent search parameters specified");

            }
            var result = (await _context.Contacts.FindAsync(filter)).FirstOrDefault();
            return result ?? EntityFactory.CreateContact();
        }

        public async Task<Guid> SaveAggregateAsync(IContactEntity aggregate)
        {
            FilterDefinition<ContactEntity> filter = Builders<ContactEntity>.Filter.Eq("_id", aggregate.Id);

            var result = await _context.Contacts.FindAsync(filter);

            if (result.Any())
            {
                await _context.Contacts.ReplaceOneAsync(filter, aggregate as ContactEntity);
            }
            else
            {
                await _context.Contacts.InsertOneAsync(aggregate as ContactEntity);
            }
            return aggregate.Id;
        }
    }
}
