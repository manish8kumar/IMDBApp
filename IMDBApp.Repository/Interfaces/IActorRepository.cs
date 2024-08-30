using IMDBApp.Domain;

namespace IMDBApp.Repository.Interfaces
{
    public interface IActorRepository
    {
        public void AddAct(Actor actor);
        public List<Actor> GetAll();
    }
}
