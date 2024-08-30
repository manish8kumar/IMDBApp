namespace IMDBApp.Domain
{
    public class Actor
    {
        public int Id { get; private set;}
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public Actor(string name, DateTime dob)
        {
            Name = name;
            DOB = dob;
        }
        public void SetId(int id)
        {
            Id = id;
        }
    }

}
