namespace HRManagementSystem
{
    public class EntityNotFoundException : HRSystemException
    {
        public string EntityId { get; private set; }
        public string EntityType { get; private set; }

        public EntityNotFoundException() : base()
        {
        }

        public EntityNotFoundException(string entityType, string entityId)
            : base(string.Format("Entity of type {0} with ID {1} was not found.", entityType, entityId))
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
            EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}