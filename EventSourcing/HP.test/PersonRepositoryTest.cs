using HP.Domain;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace HP.test
{
    public class PersonRepositoryTest : TestBase
    {
        IPersonRepository personRepository = null;
        IEventStore eventStore = null;
        [SetUp]
        public void Setup()
        {
            eventStore = new EventStoreRepository(_configuration, _mongoDbContext);
            personRepository = new PersonRepository(_mongoDbContext, eventStore);
        }

        [Test]
        public void GetAllAsync_ReturnAllPeople()
        {
            var people = personRepository.GetAllAsync();
            Assert.NotNull(people);
        }
        [Test]
        public void Delete_Specific_person_by_PersonId()
        {
            // Creating a person object into the db.
            Address addr = new Address("Canada", "Waterloo", "ON", "n2l4m2");
            Person person = Person.Create("Kevin", "Park", addr, "hyunbin7303@gmail.com");
            var newPerson = personRepository.CreateAsync(person).Result;

            var isRemoved = personRepository.DeletePersonAsync(newPerson.Id)?.Result;
            Assert.IsTrue(isRemoved);
        }

        [Test]
        public void CreatePerson()
        {
            // Arrange
            Address addr = new Address("Canada", "Waterloo", "ON", "n2l4m2");
            Person person = Person.Create("Kevin", "Park", addr, "hyunbin7303@gmail.com");
            var personObj = personRepository.CreateAsync(person)?.Result;
            Assert.That(personObj, Is.Not.Null);
        }

        [Test]
        public void UpdatePersonAsync_UpdateSuccessful()
        {
            // Arrange
            Address addr = new Address("Canada", "Waterloo", "ON", "n2l4m2");
            Person person = Person.Create("Kevin", "Park", addr, "hyunbin7303@gmail.com","hyunbin7303");
            var personObj = personRepository.UpdatePersonAsync(person)?.Result;
            Assert.That(personObj, Is.Not.Null);
        }

    }
}