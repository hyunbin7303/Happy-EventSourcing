﻿using HP.Api.DTO;
using HP.Application.Commands;
using HP.Application.Queries;
using HP.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken token = default)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id)); 
            if(person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePersonDto personDto, CancellationToken cancellationToken = default)
        {
            if (personDto == null)
                return BadRequest();
            
            var cmd = new InsertPersonCommand(personDto.UserId,personDto.FirstName, personDto.LastName, personDto.Address);
            // Message Broker call? Should we need to do in both? 
            //var userId = await _domainMessageBroker.SendAsync(createUserCommand, CancellationToken.None);
            var person = await _mediator.Send(cmd);
            //TODO: Since it is a Create, I think it's desirable to use Publish command .  
            //await _mediator.Publish(cmd, cancellationToken);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<Person> Update(string id, [FromBody]UpdatePersonDto personDto)
        {
            return await _mediator.Send(new UpdatePersonCommand(id, personDto.FirstName, personDto.LastName, personDto.Address, personDto.Email));
        }

    }
}
