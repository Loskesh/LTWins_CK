namespace HRManagementSystem
{
    public class Attendance
    {
        public string AttendanceId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public AttendanceStatus Status { get; set; }
        public bool IsAbsentRecord { get; set; }
    }

}