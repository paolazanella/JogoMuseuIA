using UnityEngine;

public class AlertGuardaState : BaseState<GuardaFSM.AIState>
{
    private GuardaFSM _guarda;
    private float alertTimer;
    private float alertDuration = 5f; // tempo em alerta antes de voltar a patrulhar

    public AlertGuardaState(GuardaFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _guarda = (GuardaFSM)MyFsm;
        alertTimer = 0f;
        Debug.Log(" Guarda entrou em modo ALERTA! Chamando reforços/drones...");
    }

    public override void UpdateState()
    {
        if (_guarda == null) return;

        // Tempo em alerta
        alertTimer += Time.deltaTime;

        // colocar lógica extra, tipo:
        // - Notificar outros guardas
        // - Ativar drones/câmeras
        // - Mudar cor do guarda para vermelho (feedback visual)
    }

    public override GuardaFSM.AIState GetNextState()
    {
        if (_guarda == null) return StateKey;

        if (_guarda.targetEnemy != null)
        {
            // Se avistar o ladrão de novo, persegue
            return GuardaFSM.AIState.Chase;
        }

        if (alertTimer >= alertDuration)
        {
            // Passou o tempo → volta a patrulhar
            return GuardaFSM.AIState.Patrol;
        }

        // Continua em alerta
        return StateKey;
    }
}