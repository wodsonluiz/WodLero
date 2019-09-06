using System.Collections.Generic;
using WodLero.Domain.Entities;

namespace WodLero.Domain.Interface
{
    public interface IPhrasesRepository
    {
        IEnumerable<Phrases> GetAll(string _connection);
        Phrases GetById(int id, string _connection);
        bool Insert(Phrases phrases, string _connection);
        bool Update(Phrases phrases, string _connection);
        bool Delete(int id, string _connection);
    }
}
