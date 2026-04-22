namespace TechMove.States
{
    public class ActiveState : IContractState
    {
        public string StateName => "Active"; // Identifies this state as Active
        public bool CanCreateServiceRequest() => true; // Active contracts can have service requests created
        public string GetBlockedReason() => string.Empty; // No reason needed as service requests are allowed
    }
}