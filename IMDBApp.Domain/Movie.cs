namespace IMDBApp.Domain
{
    public class Movie
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<Actor> Actors { get; set; }
        public Producer Producer { get; set; }

        public Movie(string name, int yearOfRelease, string plot, List<Actor> actors, Producer producer)
        {
            Name = name;
            YearOfRelease = yearOfRelease;
            Plot = plot;
            Actors = actors;
            Producer = producer;
        }
        public void SetId(int id)
        {
            Id = id;
        }

    }
}
