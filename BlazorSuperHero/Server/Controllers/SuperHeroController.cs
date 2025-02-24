﻿using BlazorSuperHero.Client.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSuperHero.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();

            return Ok(comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHeroes(int Id)
        {
            var hero = _context.SuperHeroes.Include(h => h.Comic).FirstOrDefault(h => h.Id == Id);
            if (hero == null)
            {
                return NotFound("Sorry No Hero here:/");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHeroes(SuperHero hero)
        {
            hero.Comic = null;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        [HttpPut("Id")]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int id)
        {
            var dbHero = await _context.SuperHeroes.Include(sh => sh.Comic).FirstOrDefaultAsync(sh => sh.Id == id);

            if (dbHero == null)
                return NotFound("Sorry No Super Hero for You!");

            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.HeroName = hero.HeroName;
            dbHero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }


        [HttpDelete("Id")]
        public async Task<ActionResult<List<SuperHero>>> DeletSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes.Include(sh => sh.Comic).FirstOrDefaultAsync(sh => sh.Id == id);

            if (dbHero == null)
                return NotFound("Sorry No Super Hero for You!");

            _context.SuperHeroes.Remove(dbHero);
            
            await _context.SaveChangesAsync();

            return Ok(await GetDbHeroes());
        }

        private async Task<List<SuperHero>>GetDbHeroes()
        {
            return await _context.SuperHeroes.Include(sh => sh.Comic).ToListAsync();
        }

    }
}
