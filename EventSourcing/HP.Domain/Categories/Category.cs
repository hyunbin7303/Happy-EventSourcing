﻿using HP.Core.Models;

namespace HP.Domain.Categories
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; }
        public bool IsAllowPersonChange { get; set; }
        public bool IsFilterRequired { get; set; }
        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
