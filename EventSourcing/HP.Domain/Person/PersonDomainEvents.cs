﻿using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Person
{
    public static class PersonDomainEvents
    {
        public class PersonCreated : DomainEventBase
        {
            public PersonCreated(string personId, string firstName, string lastName, string email, Address address) : base(nameof(Person))
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Address = address;
            }
            public string FirstName { get; }
            public string LastName { get; }
            public Address Address { get; }
            public string Email { get; }

        }

        public class PersonUpdated : DomainEventBase
        {
            public PersonUpdated()
            {

            }
        }


        public class AddressChanged : DomainEventBase
        {
            public AddressChanged(string personId, string country, string city, string stress, string zipCode) : base(entityType: nameof(Address))
            {
                Country = country;
                City = city;
                Stress = stress;
                ZipCode = zipCode;
            }
            public string Country { get; }
            public string City { get; }
            public string Stress { get; }
            public string ZipCode { get; }
        }
    }
}
