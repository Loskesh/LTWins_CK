using System.Text.Json;
using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class JsonFileStorage : IFileStorage
    {
        private readonly JsonSerializerOptions _options;

        public JsonFileStorage()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            // Add JsonConverter for handling Employee polymorphism
            _options.Converters.Add(new JsonStringEnumConverter());
            _options.Converters.Add(new EmployeeJsonConverter());
        }

        public bool SaveData<T>(string filename, T data)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(filename, jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T LoadData<T>(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (!File.Exists(filename))
            {
                return default;
            }

            string jsonString = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<T>(jsonString, _options);
        }
    }

    // Custom JsonConverter for Employee types
    public class EmployeeJsonConverter : JsonConverter<Employee>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Employee).IsAssignableFrom(typeToConvert);
        }

        public override Employee Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Clone the options without this converter to avoid infinite recursion
            JsonSerializerOptions clonedOptions = new JsonSerializerOptions(options);

            // Create a new converter list without this converter
            List<JsonConverter> filteredConverters = new List<JsonConverter>();
            foreach (JsonConverter converter in options.Converters)
            {
                if (!(converter is EmployeeJsonConverter))
                {
                    filteredConverters.Add(converter);
                }
            }

            // Clear and re-add the converters
            clonedOptions.Converters.Clear();
            foreach (JsonConverter? converter in filteredConverters)
            {
                clonedOptions.Converters.Add(converter);
            }

            // Read the JSON into a document
            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            JsonElement rootElement = document.RootElement;

            // Determine the employee type
            string employeeType = "Regular";
            if (rootElement.TryGetProperty("employeeType", out JsonElement typeElement))
            {
                employeeType = typeElement.GetString() ?? "Regular";
            }

            // Convert JsonElement back to JSON string
            string json = rootElement.GetRawText();

            // Deserialize to the appropriate type
            try
            {
                return employeeType switch
                {
                    "FullTime" => JsonSerializer.Deserialize<FullTimeEmployee>(json, clonedOptions),
                    "Contract" => JsonSerializer.Deserialize<ContractEmployee>(json, clonedOptions),
                    _ => JsonSerializer.Deserialize<Employee>(json, clonedOptions)
                };
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error deserializing employee of type '{employeeType}': {ex.Message}", ex);
            }
        }

        public override void Write(Utf8JsonWriter writer, Employee value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Write common properties
            writer.WriteString("id", value.Id);
            writer.WriteString("name", value.Name);
            writer.WriteString("email", value.Email);
            writer.WriteString("phone", value.Phone);
            writer.WriteString("dateOfBirth", value.DateOfBirth);
            writer.WriteString("address", value.Address);
            writer.WriteString("employeeId", value.EmployeeId);
            writer.WriteString("hireDate", value.HireDate);
            writer.WriteString("position", value.Position);
            writer.WriteNumber("baseSalary", value.BaseSalary);
            writer.WriteString("departmentId", value.DepartmentId);
            writer.WriteString("status", value.Status.ToString());

            // Write specific type
            if (value is FullTimeEmployee fullTimeEmployee)
            {
                writer.WriteString("employeeType", "FullTime");
                writer.WriteNumber("annualBonus", fullTimeEmployee.AnnualBonus);
            }
            else if (value is ContractEmployee contractEmployee)
            {
                writer.WriteString("employeeType", "Contract");
                writer.WriteNumber("hourlyRate", contractEmployee.HourlyRate);
                writer.WriteNumber("hoursWorked", contractEmployee.HoursWorked);
            }
            else
            {
                writer.WriteString("employeeType", "Regular");
            }

            writer.WriteEndObject();
        }
    }
}