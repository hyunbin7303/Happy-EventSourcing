﻿using HP.Core.Commands;
using HP.Domain.Todos.Write;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeavtivateTodoCommand(Guid TodoId) : BaseCommand;
    public class DeavtivateTodoCommandHandler : IRequestHandler<DeavtivateTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public DeavtivateTodoCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeavtivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            todo.DeactivateTodo();
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo is deactiavated", todo.Id.ToString());
        }
    }
}
