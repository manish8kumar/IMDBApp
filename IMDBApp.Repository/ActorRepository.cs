using IMDBApp.Domain;
using IMDBApp.Repository.Interfaces;

namespace IMDBApp.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly List<Actor> _actors;
        private int _nextId;

        public ActorRepository()
        {
            _actors = new List<Actor>();
            _nextId = 1; 
        }

        public void AddAct(Actor actor)
        {
            actor.SetId(_nextId++);
            _actors.Add(actor);
        }

        public List<Actor> GetAll() => _actors;
    }
}
