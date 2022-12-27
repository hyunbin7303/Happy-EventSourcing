﻿using HP.Core.Models;
namespace HP.Domain
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : DomainEventBase
        {
            public TodoCreated(Guid todoId, string userId, string todoTitle, string todoType) 
                : base(entityType:nameof(Todo))
            {
                this.TodoId = todoId;
                this.UserId = userId;
                this.TodoTitle = todoTitle;
                this.Type = todoType;
            }
            public Guid TodoId { get; }
            public string UserId { get; }
            public string TodoTitle { get; }
            public string Type { get; }
        }
        public class TodoUpdated : DomainEventBase
        {
            public TodoUpdated(string userId, Guid id, string todoTitle, string type) : base(entityType: nameof(Todo))
            {
                UserId = userId;
                TodoId = id;
                TodoTitle = todoTitle;
                TodoType = type;
            }
            public string UserId { get; set; }
            public Guid TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoRemoved : DomainEventBase
        {
            public TodoRemoved(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;   
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToPending : DomainEventBase
        {
            public TodoStatusToPending(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToAccepted : DomainEventBase
        {
            public TodoStatusToAccepted(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }


        public class TodoActivated : DomainEventBase
        {
            public TodoActivated(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoDeactivated : DomainEventBase
        {
            public TodoDeactivated(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStarted : DomainEventBase
        {
            public TodoStarted(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoCompleted : DomainEventBase
        {
            public TodoCompleted(Guid todoId) : base(entityType: nameof(Todo))
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoItemRemoved : DomainEventBase
        {
            public TodoItemRemoved(Guid todoItemId) : base(entityType: nameof(TodoItem))
            {
                if (todoItemId == null)
                    throw new ArgumentNullException(nameof(todoItemId));

                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
        public class TodoItemUpdated : DomainEventBase
        {
            public TodoItemUpdated(Guid todoItemId) : base(entityType: nameof(TodoItem))
            {
                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
    }
}
