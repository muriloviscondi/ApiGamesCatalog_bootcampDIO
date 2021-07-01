using ApiGamesCatalog.Arguments.Game;
using ApiGamesCatalog.Interfaces.Services;
using ApiGamesCatalog.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAll([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 1)
        {
            var games = await _gameService.GetAll(page, quantity);

            if (games.Count() == 0)
            {
                return NoContent();
            }

            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<object>> GetById([FromRoute] Guid idgame)
        {
            var game = await _gameService.GetById(idgame);

            if (game == null)
            {
                return NoContent();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<object>> Create([FromBody] CreateGameRequest game)
        {
            try
            {
                var request = await _gameService.Create(game);

                return Ok(request);
            }
            catch (GameAlreadyExistException ex)
            {

                return UnprocessableEntity("Já existe um jogo cadastrado com este nome para esta produtora");
            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> Alter([FromRoute] Guid idGame, [FromBody] AlterGameRequest game)
        {
            try
            {
                await _gameService.Alter(idGame, game);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {

                return NotFound("Não existe este jogo");
            }
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> AlterPrice([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.AlterPrice(idGame, price);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {

                return NotFound("Não existe este jogo");
            }
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult> Delete(Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {

                return NotFound("Não existe este jogo");
            }
        }
    }
}
