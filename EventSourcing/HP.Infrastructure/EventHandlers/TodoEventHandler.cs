﻿using HP.Core.Common;
using HP.Domain;
using HP.Domain.Todos.Read;
using MongoDB.Driver;
using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public class TodoEventHandler : ITodoEventHandler
    {
        private readonly ITodoDetailsRepository _todoRepository;
        //private readonly IBaseRepository<TodoDetails> _todoRepository;
        #region Ctors
        public TodoEventHandler(ITodoDetailsRepository todoRepository)
        {
            this._todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        #endregion

        #region handlers
        public async Task On(TodoCreated @event)
        {
            var todoDetails = new TodoDetails(@event.TodoId)
            {
                PersonId = @event.PersonId,
                Title = @event.TodoTitle,
                Description = @event.TodoDesc,
                TodoType = @event.TodoType
            };
            await _todoRepository.CreateAsync(todoDetails);
        }
        public async Task On(TodoUpdated @event)
        {
            var todoDetails = new TodoDetails(@event.TodoId)
            {
                Title = @event.TodoTitle,
                Description = @event.TodoDesc,
                TodoType = @event.TodoType
            };
            await _todoRepository.UpdateAsync(todoDetails);
        }
        public async Task On(TodoActivated @event)
        {
            var findTodo = await _todoRepository.FindOneAsync(x => x.Id == @event.TodoId);
            if(findTodo != null)
            {
                findTodo.IsActive = true;
                await _todoRepository.UpdateAsync(findTodo);
            }
        }
        public Task On(TodoDeactivated @event)
        {
            throw new NotImplementedException();
        }
        public async Task On(TodoRemoved @event)
        {
            await _todoRepository.DeleteByIdAsync(@event.TodoId);
        }
        public Task On(TodoItemCreated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoItemUpdated @event)
        {
            throw new NotImplementedException();
        }

        #endregion


        private async Task SaveTodoDetailsViewAsync(TodoDetails todoView, CancellationToken cancellationToken)
        {
            var filter = Builders<TodoDetails>.Filter
                           .Eq(a=> a.Id, todoView.Id);

            var update = Builders<TodoDetails>.Update
                .Set(a => a.Id, todoView.Id)
                .Set(a => a.Description, todoView.Description)
                .Set(a => a.TodoType, todoView.TodoType)
                .Set(a => a.TodoStatus, todoView.TodoStatus)
                .Set(a => a.Created, todoView.Created)
                .Set(a => a.SubTodos, todoView.SubTodos);


        }
    }
}
