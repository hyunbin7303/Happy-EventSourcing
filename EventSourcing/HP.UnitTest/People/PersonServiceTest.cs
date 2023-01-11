﻿using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure.Repository;
using HP.Infrastructure;
using HP.test;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.Shared.Contacts;

namespace HP.UnitTest.People
{
    public class PersonServiceTest : TestBase
    {
        IPersonService _personService = null;
        IPersonAggregateRepository _personRepository = null;
        [SetUp]
        public void Setup()
        {
            _personRepository = new PersonRepository(_mongoDbContext);
        }
    }
}
