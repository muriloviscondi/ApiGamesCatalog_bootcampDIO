using ApiGamesCatalog.Interfaces.Arguments;
using System;
using System.Collections.Generic;

namespace ApiGamesCatalog.Arguments.Game
{
    public class GameResponse : IResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Producer { get; set; }

        public double Price { get; set; }

        public static explicit operator GameResponse(Entities.Game game)
        {
            return new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price,
            };
        }

    }
}
