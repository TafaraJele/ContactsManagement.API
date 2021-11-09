using ContactManagement.Abstractions.Enums;
using ContactManagement.Abstractions.Models;
using ContactManagement.Abstractions.Repositories.Query;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactManagement.Infrastructure.Data.Data.Mongo.Read
{
    public class ContactQueryRepository : IContactQueryRepository
    {
        private readonly MongoContext _context;

        public ContactQueryRepository(string serverName, string databaseName)
        {
            _context = new MongoContext(serverName, databaseName);
        }       

        public async Task<IEnumerable<Contact>> FindModelsAsync(List<SearchParameter> searchParameters)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Ne("isDeleted", true);
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
                                filter = Builders<Contact>.Filter.Eq("id", parameter.Value);
                            }
                            else
                            {
                                filter = Builders<Contact>.Filter.Eq("id", parameter.Value) & filter;
                            }

                        }
                        break;                    
                }

            }
            if (filter == null) throw new ArgumentException("Invalid search parameters specified");
            List<Contact> result = await _context.ContactQuery.Find(filter).ToListAsync();
            return result;
        }

        public async Task<Contact> LoadModelAsync(Guid modelId)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", modelId);
            filter = Builders<Contact>.Filter.Ne("isDeleted", true) & filter;
            if (filter == null)
            {
                throw new ArgumentException("Invalid  search parameters specified");
            }
            var result = await _context.ContactQuery.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<Guid> SaveModelAsync(Contact model)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("_id", model.Id);

            var result = await _context.ContactQuery.FindAsync(filter);

            if (result.Any())
            {
                await _context.ContactQuery.ReplaceOneAsync(filter, model);
            }
            else
            {
                await _context.ContactQuery.InsertOneAsync(model);
            }

            return model.Id;
        }
    }
}
