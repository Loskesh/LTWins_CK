using System.Text.Json.Serialization;

namespace HRManagementSystem
{
    public class RoleSelectionService
    {
        private UserRole currentRole;

        // Singleton instance
        private static RoleSelectionService instance;
        private static readonly object lockObject = new object();

        // Public accessor for the singleton instance
        public static RoleSelectionService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new RoleSelectionService(UserRole.Admin);
                        }
                    }
                }
                return instance;
            }
        }

        // Make constructors private
        private RoleSelectionService()
        {
            // Default to Employee role
            currentRole = UserRole.Employee;
        }

        private RoleSelectionService(UserRole initialRole)
        {
            currentRole = initialRole;
        }

        // Static method to initialize with specific role (if needed)
        public static void Initialize(UserRole initialRole)
        {
            lock (lockObject)
            {
                instance = new RoleSelectionService(initialRole);
            }
        }

        [JsonPropertyName("currentRole")]
        public UserRole CurrentRole
        {
            get { return currentRole; }
            private set { currentRole = value; }
        }

        public void SwitchRole(UserRole newRole)
        {
            if (currentRole != newRole)
            {
                currentRole = newRole;
                OnRoleChanged(new RoleChangedEventArgs(newRole));
            }
        }

        public bool CanAccessFeature(string featureName)
        {
            if (string.IsNullOrEmpty(featureName))
            {
                return false;
            }

            if (currentRole == UserRole.Admin)
            {
                return true; // Admin can access all features
            }
            else if (currentRole == UserRole.Employee)
            {
                return IsEmployeeFeature(featureName);
            }

            return false;
        }

        private bool IsEmployeeFeature(string featureName)
        {
            if (featureName == "ViewProfile" || featureName == "RequestLeave")
            {
                return true;
            }
            return false;
        }

        public event RoleChangedEventHandler RoleChanged;

        protected void OnRoleChanged(RoleChangedEventArgs e)
        {
            RoleChangedEventHandler handler = RoleChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    // Delegate for role changed event
    public delegate void RoleChangedEventHandler(object sender, RoleChangedEventArgs e);
}