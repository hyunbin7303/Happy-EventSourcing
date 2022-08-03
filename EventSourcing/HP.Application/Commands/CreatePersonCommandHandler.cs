﻿using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Person>
    {
        private readonly IPersonRepository _repository;
        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public async Task<Person> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // await event service. PersistAsync
            var person = Person.Create(request.FirstName, request.LastName, request.Address, request.emailAddr, request.UserName);
            return await _repository.CreateAsync(person);
        }
    }
}


//please read this !!!
//TODO https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa
// This one too! https://matthiasnoback.nl/2021/02/does-it-belong-in-the-application-or-domain-layer/