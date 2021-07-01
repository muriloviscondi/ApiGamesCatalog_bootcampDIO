using ApiGamesCatalog.Arguments.Game;
using System;

namespace ApiGamesCatalog.Entities
{
    public class Game
    {
        public Game(string name, string producer, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Producer = producer;
            Price = price;
        }

        public Game(Guid id, string name, string producer, double price)
        {
            Id = id;
            Name = name;
            Producer = producer;
            Price = price;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Producer { get; private set; }

        public double Price { get; private set; }

        public void Alter(AlterGameRequest request)
        {
            Name = request.Name;
            Producer = request.Producer;
            Price = request.Price;
        }

        public void AlterPrice(double price)
        {
            Price = price;
        }
    }
}
