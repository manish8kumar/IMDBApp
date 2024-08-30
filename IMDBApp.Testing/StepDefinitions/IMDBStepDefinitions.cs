using IMDBApp.Domain;
using IMDBApp.Services.Interfaces;
using IMDBApp.Services;
using IMDBApp.Repository;
using NUnit.Framework;

namespace IMDBApp.Tests.StepDefinitions
{
    [Binding]
    public class IMDBStepDefinitions
    {
        private int _yearOfRelease;
        private string _name, _plot;
        private Producer? _selectedProducer;
        private string _result = "";
        private List<Actor> _selectedActors = new List<Actor>();
        private IActorService _actorService;
        private IProducerService _producerService;
        private IMovieService _movieService;
        private List<Movie> _movies;

        public IMDBStepDefinitions()
        {
            _actorService = new ActorService(new ActorRepository());
            _producerService = new ProducerService(new ProducerRepository());
            _movieService = new MovieService(new MovieRepository(), _actorService, _producerService);
        }

        [Given(@"I have a movie with title '([^']*)',the year of release is '([^']*)',the plot is '([^']*)',the actors are '([^']*)',the producer is '([^']*)'")]
        public void GivenIHaveAMovieWithTitleTheYearOfReleaseIsThePlotIsTheActorsAreTheProducerIs(string name, int yearOfRelease, string plot, string actorsChoice, string producerChoice)
        {
            _name = name;
            _yearOfRelease = yearOfRelease;
            _plot = plot;
            _selectedActors = actorsChoice.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => new Actor(s.Trim(), DateTime.Now)).ToList();
            _selectedProducer = producerChoice.Length > 0 ? new Producer(producerChoice, DateTime.Now) : null;
        }


        [When(@"I add the movie in IMDB app")]
        public void WhenIAddTheMovieInIMDBApp()
        {
            try
            {
                _result = _movieService.AddMovie(_name, _yearOfRelease, _plot, _selectedActors, _selectedProducer);
            }
            catch (Exception ex)
            {
                _result = "An unexpected error occurred: "+ex.Message;
            }
        }

        [Then(@"Response should be '([^']*)'")]
        public void ThenResponseShouldBe(string expectedResponse)
        {
            Assert.AreEqual(expectedResponse, _result);
        }

        [Given(@"I have collection of movies")]
        public void GivenIHaveCollectionOfMovies()
        {
            _movies = _movieService.GetMovies();
        }

        [When(@"I fetch my movies")]
        public void WhenIFetchMyMovies()
        {
            // fetching movies is done automatically by the _movies field
        }

        [Then(@"I should have the following movies")]
        public void ThenIShouldHaveTheFollowingMovies(Table expectedTable)
        {
            foreach (var row in expectedTable.Rows)
            {
                var name = row["Name"];
                var year = int.Parse(row["Year_Of_Release"]);
                var plot = row["Plot"];
                var actorNames = row["Actors"].Split(',').Select(a => a.Trim()).ToList();
                var producerName = row["Producer"].Trim();

                var movie = _movies.SingleOrDefault(m => m.Name == name && m.YearOfRelease == year && m.Plot == plot);
                Assert.AreEqual(movie.Name, name);
                Assert.AreEqual(movie.YearOfRelease, year);
                Assert.AreEqual(movie.Plot, plot);
                CollectionAssert.AreEquivalent(actorNames, movie.Actors.Select(a => a.Name).ToList());
                Assert.AreEqual(producerName, movie.Producer.Name);

            }
        }


        [BeforeScenario]
        public void AddSampleActorsForAdd()
        {
            _actorService.CreateActor("Tom Hanks", new DateTime(1956, 7, 9));
            _actorService.CreateActor("Meryl Streep", new DateTime(1949, 6, 22));
            _actorService.CreateActor("Christian Bale", new DateTime(1974, 1, 30));
            _actorService.CreateActor("Matt Damon", new DateTime(1970, 9, 10));

        }

        [BeforeScenario]
        public void AddSampleProducersForAdd()
        {
            _producerService.CreateProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            _producerService.CreateProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            
        }

        [BeforeScenario]
        public void AddSampleMovieForAdd()
        {
            _movieService = new MovieService(new MovieRepository(), _actorService, _producerService);
            _movieService.AddMovie("Inception", 2010, "A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home.",
                new List<Actor> { _actorService.GetActors().First(a => a.Name == "Tom Hanks"), _actorService.GetActors().First(a => a.Name == "Meryl Streep") },
                _producerService.GetProducers().First(p => p.Name == "Christopher Nolan"));
        }
    }
}