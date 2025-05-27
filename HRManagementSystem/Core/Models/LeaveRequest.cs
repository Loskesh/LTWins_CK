using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class LeaveRequest
    {
        private string requestId;
        private string employeeId;
        private string employeeName;
        private DateTime requestDate;
        private DateTime startDate;
        private DateTime endDate;
        private LeaveType type;
        private LeaveStatus status;
        private string remarks;
        private string approverId;

        public LeaveRequest()
        {
            // Default constructor required for JSON serialization
            requestId = Guid.NewGuid().ToString("N");
        }

        public LeaveRequest(
            string requestId,
            string employeeId,
            string employeeName,
            DateTime requestDate,
            DateTime startDate,
            DateTime endDate,
            LeaveType type,
            string remarks,
            string approverId = null)
        {
            this.requestId = requestId ?? throw new ArgumentNullException(nameof(requestId));
            this.employeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
            this.employeeName = employeeName;
            this.requestDate = requestDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.type = type;
            status = LeaveStatus.Pending;
            this.remarks = remarks;
            this.approverId = approverId;
        }

        [JsonPropertyName("requestId")]
        public string RequestId
        {
            get { return requestId; }
            set { requestId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("employeeId")]
        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value ?? throw new ArgumentNullException(nameof(value)); }
        }

        [JsonPropertyName("employeeName")]
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        [JsonPropertyName("requestDate")]
        public DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }

        [JsonPropertyName("startDate")]
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [JsonPropertyName("endDate")]
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        [JsonPropertyName("type")]
        public LeaveType Type
        {
            get { return type; }
            set { type = value; }
        }

        [JsonPropertyName("status")]
        public LeaveStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        [JsonPropertyName("remarks")]
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        [JsonPropertyName("approverId")]
        public string ApproverId
        {
            get { return approverId; }
            set { approverId = value; }
        }

        [JsonIgnore]
        public Employee Employee { get; set; }

        public int CalculateDays()
        {
            return (EndDate - StartDate).Days + 1;
        }

        public void Submit()
        {
            Status = LeaveStatus.Pending;
        }

        public void Cancel()
        {
            Status = LeaveStatus.Cancelled;
        }

        public void Approve(string approverId)
        {
            Status = LeaveStatus.Approved;
            ApproverId = approverId;
        }

        public void Reject(string approverId, string rejectionReason)
        {
            Status = LeaveStatus.Rejected;
            ApproverId = approverId;
            Remarks += $" Rejection Reason: {rejectionReason}";
        }
    }
}