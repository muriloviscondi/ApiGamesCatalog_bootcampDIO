﻿using ApiGamesCatalog.Entities;
using ApiGamesCatalog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Persistence.Reporitories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            //{Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 21", Producer = "EA", Price = 200} },
            //{Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Fifa 20", Producer = "EA", Price = 190} },
            //{Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Fifa 19", Producer = "EA", Price = 180} },
            //{Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Name = "Fifa 18", Producer = "EA", Price = 170} },
            //{Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "Street Fighter V", Producer = "Capcom", Price = 80} },
            //{Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Grand Theft Auto V", Producer = "Rockstar", Price = 190} }
        };

        public Task<List<Game>> GetAll(int pagina, int quantidade)
        {
            return Task.FromResult(games.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Game> GetById(Guid id)
        {
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> GetByNameAndProducer(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task<List<Game>> GetByNameAndProducerNoLambda(string name, string producer)
        {
            var response = new List<Game>();

            foreach (var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Producer.Equals(producer))
                    response.Add(game);
            }

            return Task.FromResult(response);
        }

        public Task Create(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Alter(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
