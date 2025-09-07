using UnityEngine;

public class VisitanteIdleState : BaseState<VisitanteFSM.AIState>
{
    private VisitanteFSM _visitante;

    public VisitanteIdleState(VisitanteFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _visitante = (VisitanteFSM)MyFsm;
        Debug.Log("Visitante está passeando pelo museu...");
    }

    public override VisitanteFSM.AIState GetNextState()
    {
        if (_visitante == null) return StateKey;

        if (_visitante.targetEnemy != null)
        {
            float dist = Vector3.Distance(_visitante.transform.position, _visitante.targetEnemy.position);
            if (dist <= _visitante.detectionRange)
            {
                return VisitanteFSM.AIState.Alert;
            }
        }

        return StateKey;
    }
}