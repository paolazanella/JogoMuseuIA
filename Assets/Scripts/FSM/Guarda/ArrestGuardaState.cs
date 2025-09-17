
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

            // Chama método do ladrão
          //  ThiefController thief = _guarda.targetEnemy.GetComponent<ThiefController>();
          //  if (thief != null)
          //  {
          //      thief.ForPego();
          //  }

            //reseta a cena
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );

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
