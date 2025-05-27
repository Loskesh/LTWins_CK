using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    // Base class for all entities
    public class Person
    {
        private string id;
        private string name;
        private string email;
        private string phone;
        private DateTime dateOfBirth;
        private string address;

        public Person()
        {
            // Default constructor required for JSON serialization
            id = string.Empty;
            name = string.Empty;
            email = string.Empty;
            phone = string.Empty;
            address = string.Empty;
        }

        public Person(string id, string name, string email, string phone, DateTime dateOfBirth, string address)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.phone = phone;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
        }

        [JsonPropertyName("id")]
        public string Id
        {
            get { return id; }
            set { id = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get { return name; }
            set { name = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("email")]
        public string Email
        {
            get { return email; }
            set { email = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("phone")]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        [JsonPropertyName("address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}