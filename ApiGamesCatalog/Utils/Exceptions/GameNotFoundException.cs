using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Utils.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException()
            :base("Jogo não cadastrado")
        {

        }
    }
}
