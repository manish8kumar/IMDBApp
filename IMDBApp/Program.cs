using IMDBApp.Repository;
using IMDBApp.Services;


namespace IMDBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var movieRepository = new MovieRepository();
            var actorRepository = new ActorRepository();
            var producerRepository = new ProducerRepository();

            var actorService = new ActorService(actorRepository);
            var producerService = new ProducerService(producerRepository);
            var movieService = new MovieService(movieRepository, actorService, producerService);

            //SAMPLE ACTORS
            actorService.CreateActor("Tom Hanks", new DateTime(1956, 7, 9));
            actorService.CreateActor("Meryl Streep", new DateTime(1949, 6, 22));
            actorService.CreateActor("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            actorService.CreateActor("Emma Watson", new DateTime(1990, 4, 15));
            actorService.CreateActor("Joseph Gordon-Levitt", new DateTime(1981, 2, 17));
            actorService.CreateActor("Tim Robbins", new DateTime(1958, 10, 16));
            actorService.CreateActor("Morgan Freeman", new DateTime(1937, 6, 1));


            //SAMPLE PRODUCERS
            producerService.CreateProducer("Christopher Nolan", new DateTime(1970, 7, 30));
            producerService.CreateProducer("Steven Spielberg", new DateTime(1946, 12, 18));
            producerService.CreateProducer("Niki Marvin", new DateTime(1956, 2, 18));

            Console.WriteLine("WELCOME TO IMDB CONSOLE APP....");

            while (true)
            {
                try
                {
                    PrintGap();

                    Console.WriteLine(@"Choose a Option:
1. List Movies
2. Add Movie
3. Add Actor
4. Add Producer
5. Delete Movie
6. Exit");

                    var command = Convert.ToInt32(Console.ReadLine());

                    if (command <= 0 || command > 6)
                    {
                        Console.WriteLine("Invalid command");
                        continue;
                    }

                    PrintGap();

                    if (command == 1)
                    {
                        Console.WriteLine(movieService.ListMovies());
                        continue;
                    }

                    if (command == 2)
                    {
                        Console.WriteLine(movieService.AddMovieFromUserInput());
                        continue;
                    }

                    if (command == 3)
                    {
                        Console.WriteLine(actorService.AddActorFromUserInput());
                        continue;
                    }

                    if (command == 4)
                    {
                        Console.WriteLine(producerService.AddProducerFromUserInput());
                        continue;
                    }

                    if (command == 5)
                    {
                        Console.WriteLine(movieService.DeleteMovieFromUserInput());
                        continue;
                    }

                    if (command == 6)
                    {
                        Console.WriteLine("Closing the app, Have a Nice Day!");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void PrintGap()
        {
            Console.WriteLine("---------------------------------------------");
        }
    }
}
