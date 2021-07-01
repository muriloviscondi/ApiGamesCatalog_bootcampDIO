using ApiGamesCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetAll(int page, int quantity);

        Task<Game> GetById(Guid id);

        Task<List<Game>> GetByNameAndProducer(string name, string produce);

        Task Create(Game game);

        Task Alter(Game game);

        Task Delete(Guid id);
    }
}
