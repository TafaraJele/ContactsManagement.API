using ContactManagement.Abstractions.Commands;
using ContactManagement.Abstractions.Models;
using ContactManagement.Abstractions.Repositories.Write;
using ContactManagement.Abstractions.Services;
using ContactManagement.Core.Aggregates;
using ContactManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;
using Veneka.Platform.Common.Enums;

namespace ContactManagement.Core.Services
{
    public class ContactManagementApplication : IContactManagementApplication
    {
        private readonly IContactRepository _contactRepository;


        public ContactManagementApplication(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;

        }
       

        public async Task<CommandResult<Contact>> CreateContact(CreateContact command)
        {
            var resource = command.CommandData;
            var commandResult = new CommandResult<Contact>(Guid.NewGuid(), resource, false);
            var entity = (ContactEntity)(await _contactRepository.LoadAggregateAsync(Guid.NewGuid()));
            
            //validate contact detail in the aggregate class and create entity model if valid
            var aggregate = new ContactAggregate (entity);

            var result = aggregate.CreateContact(command.CommandData);           

            if (result.IsValid)
            {
                await _contactRepository.SaveAggregateAsync(aggregate.Entity);

                commandResult = new CommandResult<Contact>(Guid.NewGuid(), resource, true);

                return commandResult;

            }

            return commandResult;
        }

        public async Task<CommandResult<Guid>> DeleteContact(Guid Id)
        {
            var resource = Id;
            var commandResult = new CommandResult<Guid>(Guid.NewGuid(), resource, false);

            //get contact to be deleted
            var entity = (ContactEntity)(await _contactRepository.LoadAggregateAsync(Id));

            //set is delete value of the contact = true
            var aggregate = new ContactAggregate(entity);

            var result = aggregate.DeleteContact();

            if (result.IsValid)
            {
                await _contactRepository.SaveAggregateAsync(aggregate.Entity);

                commandResult = new CommandResult<Guid>(Guid.NewGuid(), resource, true);

                return commandResult;

            }

            return commandResult;

        }
        
        public async Task<CommandResult<Contact>> UpdateContact(UpdateContact command)
        {
            var resource = command.CommandData;
            var commandResult = new CommandResult<Contact>(Guid.NewGuid(), resource, false);
            
            //find contact to be updated
            var entities = await _contactRepository.FindAggregatesAsync(new List<SearchParameter> { new SearchParameter { Name = "", Value = "" } }, FilterType.And) as List<ContactEntity>;

            //select contact to update
            var entity = entities.Where(c => c.Id == command.CommandData.Id && c.IsDeleted == false).Select(c => c).FirstOrDefault();

            if (entity == null)
            {
                commandResult = new CommandResult<Contact>(Guid.NewGuid(), resource, false);
                commandResult.AddResultMessage(ResultMessageType.Error, "01", "Update record not found");
                return commandResult;

            }

            //validate contact detail in the aggregate class and create entity model if valid
            var aggregate = new ContactAggregate(entity);

            var result = aggregate.CreateContact(command.CommandData);

            if (result.IsValid)
            {
                await _contactRepository.SaveAggregateAsync(aggregate.Entity);

                commandResult = new CommandResult<Contact>(Guid.NewGuid(), resource, true);

                return commandResult;

            }

            return commandResult;
        }

        
    }
}
