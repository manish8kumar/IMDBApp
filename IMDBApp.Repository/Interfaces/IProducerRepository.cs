using IMDBApp.Domain;

namespace IMDBApp.Repository.Interfaces
{
    public interface IProducerRepository
    {
        public void AddPro(Producer producer);
        public List<Producer> GetAll();
    }
}
