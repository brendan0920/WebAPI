using Microsoft.AspNetCore.Mvc;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MovieController : ControllerBase
    {
        static List<string> Movies = new List<string> { 
            "Star Wars Episode",
            "Pulp Fiction",
            "Evil Dead: Rise"
        };

        [HttpGet]
        public List<string> GetAllMovies()
        {
            return Movies;
        }

        [HttpGet("{idx}")]
        public string GetMovieByIdx(int idx)
        {
            if (!ValidIndex(idx))
            {
                return "No Movie at that index position. Try again.";
            }
            else
            {
                return Movies[idx];
            }
        }

        private bool ValidIndex(int idx)
        {
            bool valid = true;
            if (idx < 0 || idx >= Movies.Count)
            {
                valid = false;
            }
            return valid;
        }

        [HttpPost]
        public string AddMovie(string movie)
        {
            Movies.Add(movie);
            return "Movie added";
        }

        [HttpDelete("{idx}")]
        public string RemoveMovie(int idx)
        {
            if (!ValidIndex(idx))
            {
                return "No movie at that index positon. try again";
            }
            else
            {
                Movies.RemoveAt(idx);
                return "Movie removed";
            }
        }

        [HttpPut]
        public string UpdateMovie(int idx, string newTitle)
        {
            if (!ValidIndex(idx))
            {
                return "No movie at that index positon. try again";
            }
            else
            {
                Movies[idx] = newTitle;
                return "Movie updated";
            }
        }
        
    }
}
