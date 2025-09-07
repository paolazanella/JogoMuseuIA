using UnityEngine;

public class DroneIdleState : BaseState<DroneFSM.AIState>
{
    private DroneFSM _drone;

    public DroneIdleState(DroneFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _drone = (DroneFSM)MyFsm;
        Debug.Log("Drone está em espera...");
    }

    public override DroneFSM.AIState GetNextState()
    {
        if (_drone.targetEnemy != null)
        {
            return DroneFSM.AIState.Search;
        }

        return StateKey;
    }
}