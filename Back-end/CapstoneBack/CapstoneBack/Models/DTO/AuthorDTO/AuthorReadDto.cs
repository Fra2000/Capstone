namespace CapstoneBack.Models.DTO.AuthorDTO
{
    public class AuthorReadDto
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
        public List<string> Books { get; set; }
    }
}
