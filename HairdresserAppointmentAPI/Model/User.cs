namespace HairdresserAppointmentAPI.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(Guid id, DateTime createDate, DateTime updateDate, string fullName, string firstName, string lastName, string email, string password)
        {
            Id = id;
            CreateDate = createDate;
            UpdateDate = updateDate;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
