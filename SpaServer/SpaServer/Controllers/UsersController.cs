using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
    public class UsersController : ControllerBase
    {
        private readonly Spa586DBContext _context;

        public UsersController(Spa586DBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            

            //return await _context.Users.ToListAsync();
            //return await _context.Users.Include(u => u.Boards).ToListAsync() ;
            return await _context.Users.Select(b =>
            new UserDto()
            {
                User = b.User,
                Username = b.Username,
                Boards = _context.Boards.Where(t => t.UserNavigation.User == b.User).Select(t =>
                new BoardDtoLite()
                {
                    Id = t.Id,
                    Subject = t.Subject,
                    Content = t.Content,
                    Owner = t.UserNavigation.Username,
                }).ToList(),

                Comments = _context.Comments.Where(c => c.UserNavigation.User == b.User).Select(c =>
                new CommentDto()
                {
                    Id = c.Id,
                    Content = c.Content,
                    IdBoard = c.IdBoard,
                    BoardName = c.IdBoardNavigation.Subject,
                    Owner = c.UserNavigation.Username
                }).ToList(),

            }).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(UserDto))]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUsers(string id)
        {
            //var users = await _context.Users.FindAsync(id);
            

            var userDto = await _context.Users.Select(b =>
                new UserDto()
                {
                    User = b.User,
                    Username = b.Username,
                    Boards = _context.Boards.Where(t => t.UserNavigation.User == id).Select( t =>
                    new BoardDtoLite()
                    {
                        Id = t.Id,
                        Subject = t.Subject,
                        Content = t.Content,
                        Owner = t.UserNavigation.Username,
                    }).ToList(),

                    Comments = _context.Comments.Where(c => c.UserNavigation.User == id).Select(c =>

                    new CommentDto()
                    {
                        Id = c.Id,
                        Content = c.Content,
                        IdBoard = c.IdBoard,
                        BoardName = c.IdBoardNavigation.Subject,
                        Owner = c.UserNavigation.Username
                    }).ToList()


        }).SingleOrDefaultAsync(b => b.User == id);

            if (userDto == null)
            {
                return NotFound();
            }

            return userDto;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsers(string id, Users users)
        {
            if (id != users.User)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.User))
                {
                    //return Conflict();
                    return Ok(users); //Simply ignore  the conflict. //For this project's architecture it is fine.
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsers", new { id = users.User }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteUsers(string id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.User == id);
        }
    }
}
