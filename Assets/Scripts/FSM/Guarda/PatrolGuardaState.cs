using UnityEngine;

public class PatrolGuardaState : BaseState<GuardaFSM.AIState>
{
    private GuardaFSM _guarda;

    // Construtor
    public PatrolGuardaState(GuardaFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        // Referência do FSM atual quando entra no estado
        _guarda = (GuardaFSM)MyFsm;
    }

    public override void UpdateState()
    {
        if (_guarda == null) return;

        // Patrulhar normalmente
        if (_guarda.patrolPoints.Count > 0)
        {
            Transform patrolTarget = _guarda.patrolPoints[_guarda.currentPatrolIndex];
            _guarda.MoveTowards(patrolTarget.position);

            if (Vector3.Distance(_guarda.transform.position, patrolTarget.position) < 0.2f)
            {
                _guarda.currentPatrolIndex =
                    (_guarda.currentPatrolIndex + 1) % _guarda.patrolPoints.Count;
            }
        }

        base.UpdateState();
    }

    public override GuardaFSM.AIState GetNextState()
    {
        if (_guarda == null) return StateKey;

        // Se ver algo suspeito → vai investigar
        if (_guarda.targetEnemy != null)
        {
            return GuardaFSM.AIState.Investigate;
        }

        // Continua patrulhando
        return StateKey;
    }
}