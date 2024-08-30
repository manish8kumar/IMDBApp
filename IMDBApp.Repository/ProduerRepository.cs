using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;

namespace IMDBApp.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly List<Producer> _producers;
        private int _nextId;

        public ProducerRepository()
        {
            _producers = new List<Producer>();
            _nextId = 1; 
        }

        public void AddPro(Producer producer)
        {
            producer.SetId( _nextId++);
            _producers.Add(producer);
        }

        public List<Producer> GetAll() => _producers;
    }
}
