﻿using HP.Domain.Common;

namespace HP.Domain.Person
{
    public class Person : Entity, IAggregateRoot
    {
        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        public string Description { get; private set; }
        public int GroupId { get; private set; }
        public string Role { get; private set; }
        public bool IsActive { get; private set; }
        protected Person() 
        {
            IsActive = false;
        }
        public Person(string firstName, string lastName, Address address, Email email, string userId = null)
        {

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UserId = userId;
            IsActive = true;    

            AddDomainEvent(new PersonEvents.PersonCreated(Id, firstName, lastName, email, address));
        }

        public static Person Create(string firstName, string lastName, Address address, string emailValue, string userId= null)
        {
            if (firstName is null || lastName is null)
                throw new ArgumentNullException("Firstname or lastName cannot be null");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            //var userCreatedEvent = new UserCreatedEvent(user, password);
            Email email = new Email(emailValue);
            return new Person(firstName, lastName, address, email, userId); 
        }

        public static Person Update(string firstName, string lastName, string emailAddr, Address address)
        {
            // Person . How to update the Person info?
            return null;
        }
        public static Address CreateAddress(string Country, string City, string Region, string PostalCode)
        {
            // TODO Validation for the Address.
            return new Address(Country, City, Region, PostalCode);
        }

        protected override void When(IDomainEvent @event)
        {
            switch(@event)
            {
                case PersonEvents.PersonCreated created:
                    //@event.Equals(@event);
                    break;

                case PersonEvents.PersonUpdated u:
                    
                    break;

                case PersonEvents.PersonRoleSetAdminAssigned a:

                    break;

                //case PersonEvents.PersonDeleted d:
                //  break;
            }
        }
    }
}
