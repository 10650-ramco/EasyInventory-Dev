namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private User() { }

        public static User Create(string userName, string name,
                                string email, string password, string lastName)
        {
            return new User
            {
                UserName = userName,
                Password = password, //PasswordHasher.Hash(password),
                Name = name,
                LastName = lastName,
                Email = email
            };
        }

        public static User Update(int id, string userName, string name,
                                string email, string password, string lastName)
        {
            return new User
            {
                Id = id,
                UserName = userName,
                Password = password, //PasswordHasher.Hash(password),
                Name = name,
                Email = email,
                LastName = lastName

            };
        }
    }
}
