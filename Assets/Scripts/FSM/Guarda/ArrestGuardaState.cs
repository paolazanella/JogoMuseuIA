using UnityEngine;

public class ArrestGuardaState : BaseState<GuardaFSM.AIState>
{
    private GuardaFSM _guarda;

    public ArrestGuardaState(GuardaFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _guarda = (GuardaFSM)MyFsm;
        Debug.Log("Guarda prendeu o ladrão!");
    }

    public override void UpdateState()
    {
        if (_guarda == null || _guarda.targetEnemy == null) return;

        float distance = Vector3.Distance(
            _guarda.transform.position,
            _guarda.targetEnemy.position
        );

        
        
        if (distance <= _guarda.arrestRange)
        {
            // define oque fazer quando o ladrao e prezo
            Debug.Log("Ladrão capturado!");

            // Exemplo: desativar ladrão
            _guarda.targetEnemy.gameObject.SetActive(false);

            // Depois de prender, pode voltar a patrulhar
            _guarda.targetEnemy = null;
        }
    }

    public override GuardaFSM.AIState GetNextState()
    {
        if (_guarda == null) return StateKey;

        // Depois de prender → volta a patrulhar
        if (_guarda.targetEnemy == null)
        {
            return GuardaFSM.AIState.Patrol;
        }

        return StateKey;
    }
}