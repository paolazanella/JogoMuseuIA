using System.Collections.Generic;
using UnityEngine;

public class VisitanteFSM : StateMachineManager<VisitanteFSM.AIState>
{
    public enum AIState
    {
        Idle,   // Passeando
        Alert   // Avisando guardas
    }

    protected override AIState StartingStateKey { get; } = AIState.Idle;

    [Header("Configurações")]
    public float detectionRange = 3f; // alcance de visão do visitante
    public Transform targetEnemy;     // referência ao ladrão

    protected override Dictionary<AIState, BaseState<AIState>> States { get; set; } =
        new Dictionary<AIState, BaseState<AIState>>()
        {
            { AIState.Idle, new VisitanteIdleState(AIState.Idle)},
            { AIState.Alert, new VisitanteAlertState(AIState.Alert)},
        };

    protected override void Update()
    {
        // Detecta se o ladrão está por perto
        if (targetEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, targetEnemy.position);
            if (distance <= detectionRange)
            {
                // transição controlada no GetNextState
            }
        }
        base.Update();
    }
}