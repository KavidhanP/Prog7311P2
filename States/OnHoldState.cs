namespace TechMove.States
{
    public class OnHoldState : IContractState
    {
        public string StateName => "On Hold"; // Identifies this state as On Hold
        public bool CanCreateServiceRequest() => false; // On Hold contracts cannot have service requests created
        public string GetBlockedReason() => "Contract is On Hold and cannot accept service requests."; // Reason shown to user
    }
}