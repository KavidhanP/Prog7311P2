namespace TechMove.States
{
    public static class ContractStateFactory
    {
        public static IContractState GetState(string status) => status switch // Matches status string to correct state object
        {
            "Active" => new ActiveState(),  // Returns ActiveState for Active contracts
            "Expired" => new ExpiredState(), // Returns ExpiredState for Expired contracts
            "On Hold" => new OnHoldState(),  // Returns OnHoldState for On Hold contracts
            "Draft" => new DraftState(),   // Returns DraftState for Draft contracts
            _ => new DraftState()    // Default to DraftState if status is unrecognised
        };
    }
}