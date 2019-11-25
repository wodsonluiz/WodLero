using System.Collections.Generic;
using System.Threading.Tasks;
using WodLero.Domain.Entities;

namespace WodLero.Domain.Interface
{
    public interface IPhrasesRepository
    {
        Task<IEnumerable<Phrases>> GetAll(string _connection);
        Task<Phrases> GetById(int id, string _connection);
        Task<bool> Insert(Phrases phrases, string _connection);
        Task<bool> Update(Phrases phrases, string _connection);
        Task<bool> Delete(int id, string _connection);
    }
}
