using UnityEngine;

public class CameraScanState : BaseState<CameraFSM.AIState>
{
    private CameraFSM _camera;

    public CameraScanState(CameraFSM.AIState key) : base(key)
    {
        StateKey = key;
    }

    public override void EnterState()
    {
        _camera = (CameraFSM)MyFsm;
        Debug.Log("Câmera está escaneando a área...");
    }

    public override CameraFSM.AIState GetNextState()
    {
        if (_camera.targetEnemy != null)
        {
            float dist = Vector3.Distance(_camera.transform.position, _camera.targetEnemy.position);
            if (dist <= _camera.detectionRange)
            {
                return CameraFSM.AIState.Detect;
            }
        }

        return StateKey;
    }
}