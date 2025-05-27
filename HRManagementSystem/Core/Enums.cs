using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    // Enumerations
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmployeeStatus
    {
        Active,
        OnLeave,
        Terminated,
        Suspended
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveType
    {
        Annual = 0,
        Sick = 1,
        Personal = 2,
        Maternity = 3,
        Training = 4,
        Compensatory = 5
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendanceStatus
    {
        Present,
        HalfDay,
        WorkFromHome,
        Late
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin,
        Employee
    }
}