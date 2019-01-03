using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TestDotNetCore.Models;

namespace TestDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;

        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<Movie>> Get()
        {
            var movies = await _movieRepo.Fetch();
            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie != null)
                return movie;

            return NotFound();
        }
        
        // POST: api/Movies
        [HttpPost]
        public async Task<Movie> Post([FromBody] Movie data)
        {
            if (ModelState.IsValid)
            {
                var movie = await _movieRepo.CreateAsync(data);
                return movie;
            }
            return null;
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<Movie> Put(int id, [FromBody] Movie data)
        {
            if (ModelState.IsValid)
            {
                var movie = await _movieRepo.UpdateAsync(data);
                return movie;
            }
            return null;

        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie != null)
            {
                _movieRepo.Delete(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
