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
        ///     GET
        ///     {
        ///        "id": 1,
        ///        "title": "BoardGame1",
        ///        "description": "DescriptionForBoardGame1",
        ///        "Price": 20
        ///     }
        ///
        /// </remarks>
        /// <returns>A new board game</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBoardGameById(int id)
        {
            var boardGames = await _service.GetBoardGameAsync(id);

            return Ok(boardGames);
        }

        /// <summary>
        /// Gets the list of board games.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     GET
        ///     {
        ///        "id": 1,
        ///        "title": "BoardGame1",
        ///        "description": "DescriptionForBoardGame1",
        ///        "Price": 20
        ///     }
        ///
        /// </remarks>
        /// <returns>A new board game</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">If the list is null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBoardGames()
        {
            var boardGames = await _service.GetAllBoardGamesAsync();

            return Ok(boardGames);
        }

        /// <summary>
        /// Creates a new board game.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "id": 1,
        ///        "title": "Item1",
        ///        "description": "Article1",
        ///        "price": 20
        ///     }
        ///
        /// </remarks>
        /// <param name="game">Input item</param>
        /// <returns>A new board game</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">If the item is null</response>   
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddBoardGame([FromBody] BoardGame game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var boardGame = await _service.CreateBoardGameAsync(game);

                    if (boardGame != null)
                    {
                        return Ok(boardGame);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }                
            }

            return BadRequest();
        }

        /* https://localhost:44319/swagger/v1/swagger.json */
    }
}
