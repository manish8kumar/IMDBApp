using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;

namespace IMDBApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies;
        private int _nextId;

        public MovieRepository()
        {
            _movies = new List<Movie>();
            _nextId = 1; 
        }

        public void AddMov(Movie movie)
        {
             movie.SetId(_nextId++);
            _movies.Add(movie);
        }

        public List<Movie> GetAll() => _movies;

        public void Delete(int id)
        {
            var movieToDelete = _movies.SingleOrDefault(m => m.Id == id);
            if (movieToDelete != null)
                _movies.Remove(movieToDelete);
           
        }
    }
}
