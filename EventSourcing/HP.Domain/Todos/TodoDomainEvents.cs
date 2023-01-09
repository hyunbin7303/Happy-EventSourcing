﻿using HP.Core.Models;
namespace HP.Domain
{
    public static class TodoDomainEvents
    {
        public class TodoCreated : DomainEvent
        {
            public TodoCreated(Guid todoId, string PersonId, string todoTitle, string todoType) 
            {
                this.TodoId = todoId;
                this.PersonId = PersonId;
                this.TodoTitle = todoTitle;
                this.Type = todoType;
            }
            public Guid TodoId { get; }
            public string PersonId { get; }
            public string TodoTitle { get; }
            public string Type { get; }
        }
        public class TodoUpdated : DomainEvent
        {
            public TodoUpdated(Guid id, string todoTitle, string type)
            {
                TodoId = id;
                TodoTitle = todoTitle;
                TodoType = type;
            }
            public Guid TodoId { get; set; }
            public string TodoTitle { get; set; }
            public string TodoType { get; set; }
        }
        public class TodoRemoved : DomainEvent
        {
            public TodoRemoved(Guid todoId)
            {
                this.TodoId = todoId;   
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToPending : DomainEvent
        {
            public TodoStatusToPending(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStatusToAccepted : DomainEvent
        {
            public TodoStatusToAccepted(Guid todoId) 
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }


        public class TodoActivated : DomainEvent
        {
            public TodoActivated(Guid todoId) 
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoDeactivated : DomainEvent
        {
            public TodoDeactivated(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        }
        public class TodoStarted : DomainEvent
        {
            public TodoStarted(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoCompleted : DomainEvent
        {
            public TodoCompleted(Guid todoId)
            {
                this.TodoId = todoId;
            }
            public Guid TodoId { get; }
        } 
        public class TodoItemRemoved : DomainEvent
        {
            public TodoItemRemoved(Guid todoItemId) 
            {
                if (todoItemId == null)
                    throw new ArgumentNullException(nameof(todoItemId));

                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
        public class TodoItemCreated : DomainEvent
        {
            public TodoItemCreated(Guid todoItemId)
            {
                if (todoItemId == null)
                    throw new ArgumentNullException(nameof(todoItemId));

                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
        public class TodoItemUpdated : DomainEvent
        {
            public TodoItemUpdated(Guid todoItemId) 
            {
                this.TodoItemId = todoItemId;
            }
            public Guid TodoItemId { get; }
        }
    }
}
