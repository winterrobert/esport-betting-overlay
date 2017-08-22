using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using esport_betting_overlay.Models;
using System.Linq;

namespace esport_betting_overlay.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameContext _context;

        public GameController(GameContext context)
        {
            _context = context;

            if (_context.Games.Count() == 0)
            {
                _context.Games.Add(new Game { Name = "Game1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.ToList();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetById(long id)
        {
            var item = _context.Games.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Game game)
        {
            if (game == null || game.Id != id)
            {
                return BadRequest();
            }

            var g = _context.Games.FirstOrDefault(t => t.Id == id);
            if (g == null)
            {
                return NotFound();
            }

            g.Name = game.Name;

            _context.Games.Update(g);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var game = _context.Games.FirstOrDefault(t => t.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}