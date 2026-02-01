namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Department { get; private set; }

        private Employee() { }

        public static Employee Create(string name, string department)
        {
            return new Employee
            {
                Name = name,
                Department = department
            };
        }

        public static Employee Update(int id, string name, string department)
        {
            return new Employee
            {
                Id = id,
                Name = name,
                Department = department
            };
        }
    }
}