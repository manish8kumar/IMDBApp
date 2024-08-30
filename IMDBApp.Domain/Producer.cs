namespace IMDBApp.Domain
{
    public class Producer
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public Producer(string name, DateTime dateOfBirth)
        {
            Name = name;
            DOB = dateOfBirth;
        }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
