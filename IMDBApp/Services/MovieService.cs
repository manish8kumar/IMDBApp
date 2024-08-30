using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;
using IMDBApp.Services.Interfaces;


namespace IMDBApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;

        public MovieService(IMovieRepository movieRepository, IActorService actorService, IProducerService producerService)
        {
            _movieRepo = movieRepository;
            _actorService = actorService;
            _producerService = producerService;
        }


        public string AddMovieFromUserInput()
        {
            string name = GetValidInput("Enter the name of the movie: ", "Invalid name, please try again.");
            int yearOfRelease = GetValidYearOfRelease();
            string plot = GetValidInput("Enter the plot of the movie: ", "Invalid plot, please try again.");
            List<Actor> actors = GetActorsFromUserInput();
            Producer producer = GetProducerFromUserInput();

            return AddMovie(name, yearOfRelease, plot, actors, producer);
        }

        private string GetValidInput(string prompt, string errorMessage)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(errorMessage);
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private int GetValidYearOfRelease()
        {
            int yearOfRelease;
            while (true)
            {
                Console.WriteLine("Enter the year of release of the movie: ");
                try
                {
                    yearOfRelease = Convert.ToInt32(Console.ReadLine());
                    if (yearOfRelease < 1900 || yearOfRelease > DateTime.Now.Year + 1)
                    {
                        Console.WriteLine("Invalid year of release, please try again.");
                        continue;
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message}");
                }
            }
            return yearOfRelease;
        }

        private List<Actor> GetActorsFromUserInput()
        {
            var actorsList = _actorService.GetActors();
            List<Actor> selectedActors = new List<Actor>();

            while (true)
            {
                Console.WriteLine("Enter the ID of the actors separated with commas (For example, 1, 2):");

                for (int i = 0; i < actorsList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {actorsList[i].Name}");
                }

                Console.WriteLine($"{actorsList.Count + 1}. Add new actor");

                try
                {
                    var ids = Console.ReadLine()
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => Convert.ToInt32(s) - 1)
                        .ToList();

                    if (ids.Count == 0)
                    {
                        Console.WriteLine("There must be at least one actor in the movie, please try again.");
                        continue;
                    }

                    if (ids.Count == 1 && ids[0] == actorsList.Count)
                    {
                        _actorService.AddActorFromUserInput();
                        actorsList = _actorService.GetActors();
                        continue;
                    }

                    selectedActors = ids.Select(id =>
                    {
                        var actor = actorsList.ElementAtOrDefault(id);  // we can use trnery opertor instead of this method
                        if (actor == null)
                            throw new Exception($"{id + 1} is not a valid ID of an actor, please try again.");
                        return actor;
                    }).ToList();

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return selectedActors;
        }

        private Producer GetProducerFromUserInput()
        {
            var producersList = _producerService.GetProducers();
            Producer selectedProducer = null;

            while (true)
            {
                Console.WriteLine("Enter the ID of the producer: ");
                for (int i = 0; i < producersList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {producersList[i].Name}");
                }

               
                Console.WriteLine($"{producersList.Count + 1}. Add new producer");

                var input = Console.ReadLine();

                if (input == (producersList.Count + 1).ToString())
                {
                    _producerService.AddProducerFromUserInput();
                    producersList = _producerService.GetProducers();
                    continue;
                }

                try
                {
                    var id = Convert.ToInt32(input);

                    if (id < 1 || id > producersList.Count)
                    {
                        Console.WriteLine("Invalid producer ID, please try again.");
                        continue;
                    }

                    selectedProducer = producersList[id - 1];
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return selectedProducer;
        }

        public string AddMovie(string name, int year, string plot, List<Actor> actors, Producer producer)
        {
            if (string.IsNullOrEmpty(name))
                return "Movie Name cannot be empty";

            if (year <= 0 || year < 1900 || year > DateTime.Now.Year + 1)
                return "Invalid Year Of Release";

            if (string.IsNullOrEmpty(plot))
                return "Plot cannot be empty";

            if (actors == null || actors.Count == 0)
                return "List of actor cannot be empty";

            if (producer == null)
                return "Producer cannot be empty";

            if (GetMovies().SingleOrDefault(m => m.Name == name) != null)
                return "This movie already exists";

            _movieRepo.AddMov(new Movie(name, year, plot, actors, producer));
            return "Movie Added";
        }

        public string ListMovies()
        {
            var movies = GetMovies();
            if (movies.Count == 0)
                Console.WriteLine("There are no movies in the list.");
            return string.Join("\n\n", movies.Select(m =>
            {
                return 
                       $"Name: {m.Name}\n" +
                       $"Year of release: {m.YearOfRelease}\n" +
                       $"Plot: {m.Plot}\n" +
                       $"Actors: {string.Join(", ", m.Actors.Select(a => a.Name))}\n" +
                       $"Producer: {m.Producer.Name}";
            }));
        }

        public string DeleteMovieFromUserInput()
        {
            var movies = GetMovies();

            if (movies.Count == 0)
            {
                Console.WriteLine("There are no movies in the list.");
                return null;
            }

            Console.WriteLine("Enter the ID of the movie you want to delete: ");
            Console.WriteLine(string.Join("\n", movies.Select(m => m.Id + ". " + m.Name)));

            while (true)
            {
                try
                {
                    var id = Convert.ToInt32(Console.ReadLine());

                    if (movies.SingleOrDefault(m => m.Id == id) == null)
                        throw new Exception("Movie not found!");

                    return DeleteMovie(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public string DeleteMovie(int id)
        {
            
                _movieRepo.Delete(id);
                return "Movie deleted successfully.";
            
        }


        public List<Movie> GetMovies()
        {
            return _movieRepo.GetAll();
        }
    }
}
