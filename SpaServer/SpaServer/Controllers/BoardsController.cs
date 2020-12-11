using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaServer.Models;

namespace SpaServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly Spa586DBContext _context;

        public BoardsController(Spa586DBContext context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BoardDtoLite>>> GetBoards()
        {
            //return await _context.Boards.ToListAsync();
            return await _context.Boards.Select(b =>
            new BoardDtoLite
            {
                Id = b.Id,
                Subject = b.Subject,
                Content = b.Content,
                Owner = b.UserNavigation.User
            }).OrderByDescending(b=>b.Id).ToListAsync();
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BoardDtoDetail>> GetBoards(int id)
        {
            //var boards = await _context.Boards.FindAsync(id);

            var boards = await _context.Boards.Select(b =>
            new BoardDtoDetail
            {
                Id = b.Id,
                Subject = b.Subject,
                Content = b.Content,
                Owner = b.UserNavigation.User,

                Comments = _context.Comments.Where(c => c.IdBoard == b.Id).Select(c =>
                new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    IdBoard = c.IdBoard,
                    BoardName = c.IdBoardNavigation.Subject,
                    Owner = c.UserNavigation.Username
                }).OrderByDescending(c => c.Id).ToList(),

            }).SingleOrDefaultAsync(b => b.Id == id);


            if (boards == null)
            {
                return NotFound();
            }
            
            return Ok(boards);
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBoards(int id, Boards boards)
        {
            if (id != boards.Id)
            {
                return BadRequest();
            }

            _context.Entry(boards).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Boards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Boards>> PostBoards(Boards boards)
        {
            _context.Boards.Add(boards);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoards", new { id = boards.Id }, boards);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Boards>> DeleteBoards(int id)
        {
            var boards = await _context.Boards.FindAsync(id);
            if (boards == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(boards);
            await _context.SaveChangesAsync();

            return boards;
        }

        private bool BoardsExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
    }
}
