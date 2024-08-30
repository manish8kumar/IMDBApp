using IMDBApp.Domain;

namespace IMDBApp.Services.Interfaces
{
    public interface IActorService
    {
        string CreateActor(string name, DateTime dOB);
        List<Actor> GetActors();
        string AddActorFromUserInput();
    }
}
