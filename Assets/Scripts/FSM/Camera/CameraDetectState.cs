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
    }

    public override CameraFSM.AIState GetNextState()
    {
        // Depois de avisar, volta a escanear
        return CameraFSM.AIState.Scan;
    }
}