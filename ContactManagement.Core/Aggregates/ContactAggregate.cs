using ContactManagement.Abstractions.Models;
using ContactManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Veneka.Platform.Common;
using Veneka.Platform.Common.Enums;

namespace ContactManagement.Core.Aggregates
{
    public class ContactAggregate : BaseAggregate<ContactEntity>
    {
        private ValidationResult validationResult;

        public ContactAggregate(ContactEntity entity) : base(entity)
        {
            validationResult = new ValidationResult();
        }

        public ValidationResult CreateContact(Contact contact)
        {
            if (ValidateContact(contact).IsValid)
            {
                contact.Id = Entity.Id;
                SetDetails(contact);
            }
            return validationResult;

        }

        private void SetDetails(Contact contact)
        {
            Entity.Company = contact.Company;
            Entity.Email = contact.Email;
            Entity.Name = contact.Name;
            Entity.Notes = contact.Notes;
            Entity.Phone = contact.Phone;  

        }

        private ValidationResult ValidateContact(Contact contact)
        {
            if (string.IsNullOrEmpty(contact.Name))
            {
                validationResult.AddValidationMessage(ResultMessageType.Error, "01", "Name is required");
            }
            if (string.IsNullOrEmpty(contact.Phone) && string.IsNullOrEmpty(contact.Email))
            {
                validationResult.AddValidationMessage(ResultMessageType.Error, "01", "At least Phone or Email is required to save a contact");
            }
           
            return validationResult;
        }

        public ValidationResult DeleteContact()
        {
            Entity.IsDeleted = true;
            Entity.Company = entity.Company;
            Entity.Email = entity.Email;
            Entity.Name = entity.Name;
            Entity.Notes = entity.Notes;
            Entity.Phone = entity.Phone;           

            return validationResult;
        }

        public ValidationResult Update(Contact contact)
        {
            if (ValidateContact(contact).IsValid)
            {
                contact.Id = Entity.Id;
                SetDetails(contact);
            }
            return validationResult;

        }
    }
}
