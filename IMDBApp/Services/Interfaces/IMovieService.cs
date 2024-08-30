using IMDBApp.Domain;

namespace IMDBApp.Services.Interfaces
{
    public interface IMovieService
    {
        string AddMovie(string name, int year, string plot, List<Actor> actors, Producer producer);
        string ListMovies();
        string DeleteMovie(int id);
        List<Movie> GetMovies();
        
    }
}
