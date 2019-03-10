using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RESTful.Interfaces;
using RESTful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTful.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IMemoryCache _cache;
        private readonly BoardGameContext _context;

        public BoardGameService(IMemoryCache cache, BoardGameContext context)
        {
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<List<BoardGame>> GetAllBoardGamesAsync()
        {
            return await _context.BoardGames.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<BoardGame> GetBoardGameAsync(int id)
        {
            BoardGame boardGame = null;

            if (!_cache.TryGetValue(id, out boardGame))
            {
                boardGame = await _context.BoardGames.FirstOrDefaultAsync(x => x.Id == id);

                if (boardGame is null)
                {
                    _cache.Set(boardGame.Id, boardGame, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return boardGame;
        }

        /// <inheritdoc/>
        public async Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame)
        {
            if (_context != null)
            {
                await _context.BoardGames.AddAsync(boardGame);

                int n = await _context.SaveChangesAsync();

                if (n > 0)
                {
                    _cache.Set(boardGame.Id, boardGame, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }

                return boardGame;
            }

            return null;            
        }
    }
}
