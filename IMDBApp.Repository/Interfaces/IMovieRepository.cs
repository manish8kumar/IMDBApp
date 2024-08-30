using IMDBApp.Domain;

namespace IMDBApp.Repository.Interfaces
{
    public interface IMovieRepository
    {
        public void AddMov(Movie movie);
        public List<Movie> GetAll();
        public void Delete(int id);
    }
}
