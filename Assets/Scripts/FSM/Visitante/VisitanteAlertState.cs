using UnityEngine;

public class VisitanteAlertState : BaseState<VisitanteFSM.AIState>
{
    private VisitanteFSM _visitante;

    public VisitanteAlertState(VisitanteFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _visitante = (VisitanteFSM)MyFsm;
        Debug.Log("Visitante: 'Guarda! Tem um ladrão aqui!'");
    }

    public override VisitanteFSM.AIState GetNextState()
    {
        // Depois de avisar, o visitante volta ao estado normal
        return VisitanteFSM.AIState.Idle;
    }
}