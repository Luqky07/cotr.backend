using cotr.backend.Model.Tables;

namespace cotr.backend.Model.Response
{
    public class ProfileResponse
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Affiliation { get; set; }

        public ProfileResponse(Users user)
        {
            UserId = user.UserId;
            Nickname = user.Nickname;
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            SecondSurname = user.SecondSurname;
            Birthdate = user.Birthdate;
            Affiliation = user.Affiliation;
        }
    }
}
