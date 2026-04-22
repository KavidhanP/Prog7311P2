namespace TechMove.States
{
    public class ExpiredState : IContractState
    {

        public string StateName => "Expired"; // Identifies this state as Expired
        public bool CanCreateServiceRequest() => false; // Expired contracts cannot have service requests created
        public string GetBlockedReason() => "Contract is Expired and cannot accept service requests."; // Reason shown to user
    }
}