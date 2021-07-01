using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Utils.Exceptions
{
    public class GameAlreadyExistException : Exception
    {
        public GameAlreadyExistException()
            :base("Jogo já cadastrado")
        {

        }
    }
}
