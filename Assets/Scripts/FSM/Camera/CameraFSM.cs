using System.Collections.Generic;
using UnityEngine;

public class CameraFSM : StateMachineManager<CameraFSM.AIState>
{
    public enum AIState
    {
        Scan,   // Movendo de um lado para outro
        Detect  // Detectou ladrão
    }

    protected override AIState StartingStateKey { get; } = AIState.Scan;

    [Header("Configurações")]
    public float detectionRange = 6f;
    public Transform targetEnemy;

    protected override Dictionary<AIState, BaseState<AIState>> States { get; set; } =
        new Dictionary<AIState, BaseState<AIState>>()
        {
            { AIState.Scan, new CameraScanState(AIState.Scan)},
            { AIState.Detect, new CameraDetectState(AIState.Detect)},
        };
}