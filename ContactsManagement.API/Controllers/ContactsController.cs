using ContactManagement.Abstractions.Commands;
using ContactManagement.Abstractions.Logging;
using ContactManagement.Abstractions.Models;
using ContactManagement.Abstractions.Services;
using ContactsManagement.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactManagementApplication _contactApplication;
        private IContactQueryHandler _contactQuery;
        private ILogger<ContactsController> _logger;
        private readonly LoggingContext _loggingContext;

        public ContactsController(IContactManagementApplication contactApplication, ILogger<ContactsController> logger, IContactQueryHandler contactQuery)
        {
            _contactApplication = contactApplication;
            _contactQuery = contactQuery;
            _logger = logger;
        }
        /// <summary>
        /// Creates a new contact in the system given the contact details
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/contacts")]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            var command = new CreateContact
            {
                CommandData = contact
            };

            var result = await _contactApplication.CreateContact(command);

            if (result.Accepted)
            {
                return Ok(result);
            }

            return BadRequest(result.Messages);
        }

        /// <summary>
        /// Updates a contact in the system given the contact details
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/contacts")]
        public async Task<IActionResult> UpdateContact([FromBody]Contact contact)
        {
            var command = new UpdateContact
            {
                CommandData = contact
            };
            var result = await _contactApplication.UpdateContact(command);

            if (result.Accepted)
            {
                return Ok(result);
            }

            return BadRequest(result.Messages);
        }
        /// <summary>
        /// Sets the contact as deleted given the contact id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/contacts/{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {

            var result = await _contactApplication.DeleteContact(id);

            if (result.Accepted)
            {
                return Ok();
            }

            return BadRequest();
        }
        /// <summary>
        /// Get a contact in the system given the contact Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/contacts/{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var result = await _contactQuery.GetContact(id);

            if (result!= null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        /// <summary>
        /// Gets all contacts in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/contacts")]

        public async Task<IActionResult> GetContacts()
        {
            var result = await _contactQuery.GetContacts();

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
