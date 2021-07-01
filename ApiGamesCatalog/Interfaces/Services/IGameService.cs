using ApiGamesCatalog.Arguments.Game;
using ApiGamesCatalog.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Interfaces.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameResponse>> GetAll(int page, int quantity);

        Task<GameResponse> GetById(Guid id);        

        Task<GameResponse> Create(CreateGameRequest game);

        Task Alter(Guid id, AlterGameRequest game);

        Task AlterPrice(Guid id, double price);

        Task Delete(Guid id);
    }
}
