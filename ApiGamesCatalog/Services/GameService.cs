using ApiGamesCatalog.Arguments.Game;
using ApiGamesCatalog.Interfaces.Services;
using ApiGamesCatalog.Model;
using ApiGamesCatalog.Repositories;
using ApiGamesCatalog.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task Alter(Guid id, AlterGameRequest game)
        {
            var response = await _gameRepository.GetById(id);

            if (response == null)
            {
                throw new GameAlreadyExistException();
            }

            response.Alter(game);

            await _gameRepository.Alter(response);
        }

        public async Task AlterPrice(Guid id, double price)
        {
            var response = await _gameRepository.GetById(id);

            if (response == null)
            {
                throw new GameNotFoundException();
            }

            response.AlterPrice(price);

            await _gameRepository.Alter(response);
        }

        public async Task<GameResponse> Create(CreateGameRequest game)
        {
            var response = await _gameRepository.GetByNameAndProducer(game.Name, game.Producer);

            if (response.Count() > 0)
            {
                throw new GameAlreadyExistException();
            }

            var request = new Entities.Game(game.Name, game.Producer, game.Price);

            await _gameRepository.Create(request);

            return (GameResponse)request;
        }

        public async Task Delete(Guid id)
        {
            var game = _gameRepository.GetById(id);

            if (game == null)
            {
                throw new GameNotFoundException();
            }

            await _gameRepository.Delete(id);
        }

        public async Task<List<GameResponse>> GetAll(int page, int quantity)
        {
            var games = await _gameRepository.GetAll(page, quantity);

            return games.Select(game => new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameResponse> GetById(Guid id)
        {
            var game = await _gameRepository.GetById(id);

            if (game == null)
            {
                return null;
            }

            return new GameResponse 
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price,
            };
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
