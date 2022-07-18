using FluentAssertions;
using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;

namespace HP.test
{
    [TestFixture]
    public class TodoRepositoryTest : TestBase
    {
        private ITodoRepository todoRepository;
        private IEventStore eventStore = null;
        [SetUp]
        public void Setup()
        {
            eventStore = new EventStoreRepository(_configuration, _mongoDbContext);
            todoRepository = new TodoRepository(_mongoDbContext, eventStore);
            // Seed Data Insertion?
        }
        [Test]
        public void GetListByUserId_Return_Nothing()
        {
            var check = todoRepository.GetListByUserId("userId7303");
            Assert.That(check, Is.Not.Null);    
        }
        [Test]
        public void Exists_ReturnTrueIfExist()
        {
            var check = todoRepository.Exists(x => x.IsActive);
            Assert.IsTrue(check);
        }
        [Test]
        public void CreateNewTodo_From_Repository()
        {
            // Arrange
            var expectedUserName = "TestUser123";
            var expectedTitle = "Creating Todo";

            // Act
            var todo = TodoFactory.Create(expectedUserName, expectedTitle, true);
            var todoObj = todoRepository.CreateAsync(todo)?.Result;

            // Assert
            Assert.NotNull(todoObj);
            todoObj.UserId.Should().Be(expectedUserName);
            todoObj.Title.Should().Be(expectedTitle);
        }

        [Test]
        public void ActivateTodo_Todo_Activated_True()
        {
            // Arrange
            var todo = TodoFactory.Create();

            //Act 
            todo.ActivateTodo(todo.Id);

            //Assert
            todo.IsActive.Should().BeTrue();
        }

        [Test]
        public void DeactivateTodo_Todo_Is_Deactivated()
        {
            // Arrange
            var todo = TodoFactory.Create();

            // Act
            todo.DeactivateTodo(todo.Id);

            //Assert
            todo.IsActive.Should().BeFalse();
        }
    }
}