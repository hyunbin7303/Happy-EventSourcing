﻿using HP.Core.Common;
namespace HP.Domain
{
    public interface IPersonAggregateRepository : IAggregateBaseRepository<Person>
    {
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(Guid personId);
        Task<Person> GetPersonByPersonNameAsync(string personName);
        Task<IEnumerable<Person>> GetListByGroupIdAsync(int groupId);
        Task<IEnumerable<Person>> GetListByRoleAsync(string role);
    }
}
