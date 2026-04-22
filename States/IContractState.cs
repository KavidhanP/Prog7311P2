namespace TechMove.States
{
    public interface IContractState
    {
        string StateName { get; } // Returns the name of the current state
        bool CanCreateServiceRequest(); // Returns true if a service request can be created in this state
        string GetBlockedReason(); // Returns the reason why a service request cannot be created
    }
}