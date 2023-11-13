namespace cotr.backend.Model.Request
{
    public class SignupRequest
    {
        public string Nickname { get; }
        public string Email { get; }
        public string Name { get; }
        public string Surname { get; }
        public string? SecondSurname { get; }
        public DateTime Birthdate { get; set; }
        public string? Affiliation { get; }
        public string Password { get; set; }

        public SignupRequest(string nickname, string email, string name, string surname, string? secondSurname, DateTime birthdate, string? affiliation, string password)
        {
            Nickname = nickname;
            Email = email;
            Name = name;
            Surname = surname;
            SecondSurname = secondSurname;
            Birthdate = birthdate;
            Affiliation = affiliation;
            Password = password;
        }
    }
}
