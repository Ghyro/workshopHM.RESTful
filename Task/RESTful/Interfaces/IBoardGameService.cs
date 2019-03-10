using RESTful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTful.Interfaces
{
    public interface IBoardGameService
    {
        /// <summary>
        /// Gets the list of board games asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{List{BoardGame}}"/>.</returns>
        Task<List<BoardGame>> GetAllBoardGamesAsync();

        /// <summary>
        /// Gets a new board game by id asynchronously.
        /// </summary>
        /// <param name="id">A board game identifier</param>
        /// <returns>A <see cref="Task{BoardGame}"/>.</returns>
        Task<BoardGame> GetBoardGameAsync(int id);

        /// <summary>
        /// Creates a new board game asynchronously.
        /// </summary>
        /// <param name="boardGame">A <see cref="BoardGame"/></param>
        /// <returns>A <see cref="Task{BoardGame}"/></returns>
        Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame);
    }
}
