using UnityEngine;

public class CameraDetectState : BaseState<CameraFSM.AIState>
{
    private CameraFSM _camera;

    public CameraDetectState(CameraFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _camera = (CameraFSM)MyFsm;
        Debug.Log("🚨 Câmera detectou o ladrão! Avisando guardas...");
        
        //aleta simples usando a posição da câmera
        if (SimpleGameManager.Instance != null)
        {
            SimpleGameManager.Instance.AlertarGuardas(_camera.transform.position);
        }
    }

    public override CameraFSM.AIState GetNextState()
    {
        // Depois de avisar, volta a escanear
        return CameraFSM.AIState.Scan;
    }
}