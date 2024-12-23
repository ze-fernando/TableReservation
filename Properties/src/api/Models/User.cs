using src.api.Models.Enum;

namespace src.api.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Nickname { get; set; }
        public required string Tel { get; set; }
        public required string Password { get; set; }
        public required Roles Role { get; set; }
    }
}


