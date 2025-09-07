using UnityEngine;

public class ChaseGuardaState : BaseState<GuardaFSM.AIState>
{
    private GuardaFSM _guarda;

    public ChaseGuardaState(GuardaFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _guarda = (GuardaFSM)MyFsm;
        Debug.Log("guarda esta correndo atras do ladao");
    }

   
    public override void UpdateState()
    {
        if (_guarda == null || _guarda.targetEnemy == null) return;

        // Perseguir o ladrão
        _guarda.MoveTowards(_guarda.targetEnemy.position);

        base.UpdateState();
    }

    public override GuardaFSM.AIState GetNextState()
    {
        if (_guarda == null) return StateKey;

        if (_guarda.targetEnemy == null)
        {
            // Perdeu o ladrão → pede reforço
            return GuardaFSM.AIState.Alert;
        }

        float distance = Vector3.Distance(
            _guarda.transform.position,
            _guarda.targetEnemy.position
        );

        if (distance <= _guarda.arrestRange)
        {
            // Chegou perto o suficiente para prender
            return GuardaFSM.AIState.Arrest;
        }

        // Continua perseguindo
        return StateKey;
    }
}