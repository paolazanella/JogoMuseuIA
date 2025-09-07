using System.Collections.Generic;
using UnityEngine;

public class DroneFSM : StateMachineManager<DroneFSM.AIState>
{
    public enum AIState
    {
        Idle,    // Aguardando ordem
        Search   // Procurando ladrão
    }

    protected override AIState StartingStateKey { get; } = AIState.Idle;

    [Header("Configurações")]
    public float moveSpeed = 4f;
    public float detectionRange = 7f;
    public Transform targetEnemy;

    protected override Dictionary<AIState, BaseState<AIState>> States { get; set; } =
        new Dictionary<AIState, BaseState<AIState>>()
        {
            { AIState.Idle, new DroneIdleState(AIState.Idle)},
            { AIState.Search, new DroneSearchState(AIState.Search)},
        };
}