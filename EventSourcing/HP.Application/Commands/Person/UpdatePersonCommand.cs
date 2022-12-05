﻿using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Person
{
    public record UpdatePersonCommand(string PersonId, string PersonType, int? GroupId = null) : BaseCommand;
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _repository = personRepository;
        }
        public async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByUserIdAsync(request.PersonId.ToUpper()).Result;
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateBasicInfo(person.PersonType, person.GroupId);
            var check = await _repository.UpdatePersonAsync(person);
            if (check != null)
                return new CommandResult(false, "Updated failure. ", person.Id);
            return new CommandResult(true, "", person.Id);
        }
    }
}
