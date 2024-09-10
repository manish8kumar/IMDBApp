namespace IMDBApp.Domain
{
    public class Person
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public Person(string name, DateTime dob)
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
