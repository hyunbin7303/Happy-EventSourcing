﻿using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public class StartTodoCommandHandler : IRequestHandler<StartTodoCommand, Todo>
    {
        private readonly ITodoRepository _repository;
        public StartTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Todo> Handle(StartTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetByIdAsync(cmd.TodoId);
            if(todo == null)
            {

            }
            todo.ActivateTodo(todo.Id);
            todo.SetStatus(todo.Id, TodoStatus.Started);
            await _repository.UpdateAsync(todo);
            return todo;
        }
    }
}
