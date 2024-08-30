using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;
using IMDBApp.Services.Interfaces;
using System.Globalization;

namespace IMDBApp.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepo;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepo = producerRepository;
        }

        public string AddProducerFromUserInput()
        {
            string producerName = "";
            DateTime dateOfBirth;

            var producersList = GetProducers();

            Console.WriteLine("Enter the name of the producer: ");

            while (true)
            {
                try
                {
                    producerName = Console.ReadLine();

                    if (string.IsNullOrEmpty(producerName))
                        throw new Exception("Invalid name, please try again.");

                    if (producersList.SingleOrDefault(a => a.Name == producerName) != null)
                        throw new Exception("A producer with this name already exists, please try again.");

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Enter the date of birth (DD/MM/YYYY) of the producer: ");

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

            return CreateProducer(producerName, dateOfBirth);
        }

        public string CreateProducer(string name, DateTime dob)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Invalid arguments");
            if (GetProducers().SingleOrDefault(p => p.Name == name) != null)
                throw new ArgumentException("This producer already exists");

            _producerRepo.AddPro(new Producer(name, dob));
            return "The producer was successfully added";
        }

        public List<Producer> GetProducers()
        {
            return _producerRepo.GetAll();
        }
    }
}
