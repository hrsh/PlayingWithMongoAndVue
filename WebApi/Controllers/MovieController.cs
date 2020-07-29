using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController 
        : ControllerBase
    {

        public IActionResult Get()
        {
            var client = new MongoClient(
                "mongodb://localhost:27017");

            var db = client.GetDatabase("mymoviesdb");
            var movies = db.GetCollection<Movie>("movies");
            var moviesList = movies.Find(FilterDefinition<Movie>.Empty).ToList();

            return Ok(moviesList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movie model)
        {
            var client = new MongoClient(
                "mongodb://localhost:27017");

            var db = client.GetDatabase("mymoviesdb");
            var movies = db.GetCollection<Movie>("movies");

            movies.InsertOne(model);


            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove([FromBody] Movie model)
        {
            var client = new MongoClient(
                "mongodb://localhost:27017");

            var db = client.GetDatabase("mymoviesdb");
            var movies = db.GetCollection<Movie>("movies");
            movies.DeleteOne(a => a.Id == model.Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Movie model)
        {
            var client = new MongoClient(
                "mongodb://localhost:27017");

            var db = client.GetDatabase("mymoviesdb");
            var movies = db.GetCollection<Movie>("movies");
            movies.ReplaceOne(a => a.Id == model.Id, model);
            return Ok();
        }
    }
}
