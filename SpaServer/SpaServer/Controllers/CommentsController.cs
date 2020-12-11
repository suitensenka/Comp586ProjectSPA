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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Spa586DBContext _context;

        public CommentsController(Spa586DBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
        {
            //return await _context.Comments.ToListAsync();
            return await _context.Comments.Select(b =>

            new CommentDto()
            {
                Id = b.Id,
                Content = b.Content,
                IdBoard = b.IdBoard,
                BoardName = b.IdBoardNavigation.Subject,
                Owner = b.UserNavigation.Username
            }).ToListAsync();

        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(CommentDto))]
        [Authorize]
        public async Task<ActionResult<CommentDto>> GetComments(int id)
        {
            // var comments = await _context.Comments.FindAsync(id);
            var commentDto = await _context.Comments.Select(b =>

            new CommentDto()
            {
                Id = b.Id,
                Content = b.Content,
                IdBoard = b.IdBoard,
                BoardName = b.IdBoardNavigation.Subject,
                Owner = b.UserNavigation.Username
            }).SingleOrDefaultAsync(b => b.Id == id);

            if (commentDto == null)
            {
                return NotFound();
            }

            return Ok(commentDto);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //NOT IMPLEMENTED!
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutComments(int id, Comments comments)
        {
            if (id != comments.Id)
            {
                return BadRequest();
            }

            _context.Entry(comments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comments.Id }, comments);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [Authorize]
        //NOTE IMPLEMENTED
        public async Task<ActionResult<Comments>> DeleteComments(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();

            return comments;
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
