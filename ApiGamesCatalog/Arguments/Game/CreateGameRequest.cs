using ApiGamesCatalog.Interfaces.Arguments;

namespace ApiGamesCatalog.Arguments.Game
{
    public class CreateGameRequest : IRequest
    {
        public string Name { get; set; }

        public string Producer { get; set; }

        public double Price { get; set; }
    }
}
