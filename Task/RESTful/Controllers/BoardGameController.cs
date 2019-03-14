using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTful.Interfaces;
using RESTful.Models;

namespace RESTful.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IBoardGameService _service;

        public BoardGameController(IBoardGameService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Gets a board game.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET/BoardGame
        ///     {
        ///        "id": 1,
        ///        "title": "BoardGame1",
        ///        "description": "DescriptionForBoardGame1",
        ///        "Price": 20
        ///     }
        ///
        /// </remarks>
        /// <returns>A board game</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBoardGameById(int id)
        {
            var boardGames = await _service.GetBoardGameAsync(id);

            if (boardGames == null)
            {
                return NotFound();
            }

            return Ok(boardGames);
        }

        /// <summary>
        /// Gets the list of board games.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET/BoardGames
        ///     {
        ///        "id": 1,
        ///        "title": "BoardGame1",
        ///        "description": "DescriptionForBoardGame1",
        ///        "Price": 20
        ///     }
        ///
        /// </remarks>
        /// <returns>The list of board games</returns>
        /// <response code="200">Ok</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBoardGames()
        {
            var boardGames = await _service.GetAllBoardGamesAsync();

            if (boardGames == null)
            {
                return NotFound();
            }

            return Ok(boardGames);
        }

        /// <summary>
        /// Creates a new board game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/BoardGame
        ///     {
        ///        "title": "BoardGame1",
        ///        "description": "DescriptionForBoardGame1",
        ///        "Price": 20
        ///     }
        ///
        /// </remarks>
        /// <param name="game">A <see cref="BoardGame"/></param>
        /// <returns>A new board game</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddBoardGame([FromBody] BoardGame game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (game == null)
            {
                NotFound();
            }

            var boardGame = game;

            await _service.CreateBoardGameAsync(boardGame);

            if (boardGame == null)
            {
                return NotFound();
            }

            var outputGame = boardGame;

            return Ok(outputGame);
        }

        /// <summary>
        /// Removes a board game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/BoardGame
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="id">A board game identifier</param>
        /// <returns>Removed board game</returns>
        /// <response code="200">Ok</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>   
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]        
        public async Task<IActionResult> DeleteBoardGame(int id)
        {
            var boardGame = await _service.DeleteBoardGameAsync(id);

            if (boardGame == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        /* https://localhost:44319/swagger/v1/swagger.json  the link access to localhost Swagger (https)*/
    }
}
