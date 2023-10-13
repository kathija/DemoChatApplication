using System.Text.Json.Serialization;

namespace ChatApplication.Model
{
    public class User : BaseEnitity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public int ContactNumber { get; set; }


    }
}