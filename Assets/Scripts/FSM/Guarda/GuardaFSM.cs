using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuardaFSM : StateMachineManager<GuardaFSM.AIState>
{
    public enum AIState
    {
        Patrol,       // Patrulhando
        Investigate,  // Investigando algo suspeito
        Chase,        // Perseguindo ladrão
        Arrest,       // Prendendo o ladrão
        Alert         // Chamando reforços/drones
    }
    
    protected override AIState StartingStateKey { get; } = AIState.Patrol;
    
    [Header("Configurações")]
    public float detectionRange = 5f;   // alcance de visão
    public float arrestRange = 1.5f;    // distância mínima para prender
    public float moveSpeed = 3f;        // velocidade de movimento

    [Header("Referências")]
    public Transform originPoint;       // ponto inicial do guarda
    public List<Transform> patrolPoints; // pontos de patrulha

    public int currentPatrolIndex = 0;
    public Transform targetEnemy;       // referência ao ladrão

    protected override Dictionary<AIState, BaseState<AIState>> States { get; set; } =
        new Dictionary<AIState, BaseState<AIState>>()
        {
            { AIState.Patrol, new PatrolGuardaState(AIState.Patrol)},
            { AIState.Investigate, new InvestigateGuardaState(AIState.Investigate)},
            { AIState.Chase, new ChaseGuardaState(AIState.Chase)},
            { AIState.Arrest, new ArrestGuardaState(AIState.Arrest)},
            { AIState.Alert, new AlertGuardaState(AIState.Alert)},
        };

    protected override void Update()
    {
        targetEnemy = GetNearestEnemy();
        base.Update();
    }

    #region MétodosAdicionais

    Transform GetNearestEnemy()
    {
        // Procura apenas objetos com tag "Player"
        GameObject ladrao = GameObject.FindWithTag("Player");
        if (ladrao == null) return null;
    
        float distance = Vector3.Distance(transform.position, ladrao.transform.position);
        if (distance <= detectionRange)
        {
            return ladrao.transform;
        }
    
        return null;
    }

    public void MoveTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    #endregion
}
