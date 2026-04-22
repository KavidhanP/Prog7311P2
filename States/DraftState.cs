namespace TechMove.States
{
    public class DraftState : IContractState
    {
        public string StateName => "Draft"; // Identifies this state as Draft
        public bool CanCreateServiceRequest() => false; // Draft contracts cannot have service requests created
        public string GetBlockedReason() => "Contract is still in Draft and cannot accept service requests."; // Reason shown to user
    }
}
