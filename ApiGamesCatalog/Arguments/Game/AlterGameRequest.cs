using ApiGamesCatalog.Interfaces.Arguments;
using System;

namespace ApiGamesCatalog.Arguments.Game
{
    public class AlterGameRequest : IRequest
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Producer { get; set; }
    }
}
