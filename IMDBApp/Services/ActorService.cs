using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;
using IMDBApp.Services.Interfaces;
using System.Globalization;

namespace IMDBApp.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepo;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepo = actorRepository;
        }

        public string CreateActor(string name, DateTime dOB)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Invalid Name,try again!");
            if (GetActors().SingleOrDefault(a => a.Name == name) != null)
                throw new ArgumentException("This actor already exists");


            _actorRepo.AddAct(new Actor(name, dOB));
            return "The actor was successfully added";
        }

        public string AddActorFromUserInput()
        {
            string actorName = "";
            DateTime dateOfBirth;

            var actorsList = GetActors();

            Console.WriteLine("Enter the name of the actor: ");

            while (true)
            {
                try
                {
                    actorName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(actorName))
                        throw new Exception("Invalid name, please try again.");

                    if (actorsList.SingleOrDefault(a => a.Name == actorName) != null)
                        throw new Exception("An actor with this name already exists, please try again.");

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Enter the date of birth (DD/MM/YYYY) of the actor: ");

            while (true)
            {
                try
                {
                    dateOfBirth = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return CreateActor(actorName, dateOfBirth);
        }

        public List<Actor> GetActors()
        {
            return _actorRepo.GetAll();
        }
    }
}
