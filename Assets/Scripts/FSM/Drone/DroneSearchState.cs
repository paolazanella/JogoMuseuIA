using UnityEngine;

public class DroneSearchState : BaseState<DroneFSM.AIState>
{
    private DroneFSM _drone;

    public DroneSearchState(DroneFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _drone = (DroneFSM)MyFsm;
        Debug.Log("Drone ativado, procurando ladrão!");
    }

    public override void UpdateState()
    {
        if (_drone == null || _drone.targetEnemy == null) return;

        // Drone se move em direção ao ladrão
        Vector3 direction = (_drone.targetEnemy.position - _drone.transform.position).normalized;
        _drone.transform.position += direction * (_drone.moveSpeed * Time.deltaTime);
    }

    public override DroneFSM.AIState GetNextState()
    {
        if (_drone.targetEnemy == null)
        {
            // Se perder o ladrão, volta para Idle
            return DroneFSM.AIState.Idle;
        }

        return StateKey;
    }
}