using IMDBApp.Domain;

namespace IMDBApp.Services.Interfaces
{
    public interface IProducerService
    {
        string CreateProducer(string name, DateTime dob);
        List<Producer> GetProducers();
        string AddProducerFromUserInput();
    }
}
