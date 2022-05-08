﻿using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using MongoDB.Driver;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IMongoCollection<Person> _mongoCollection;
        public PersonRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            this._mongoCollection = dbContext.GetCollection<Person>() ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public Task<bool> DeletePersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(string personId)
        {
            throw new NotImplementedException();
        }


        public Task<Person> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
