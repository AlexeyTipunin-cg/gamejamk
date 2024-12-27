using Cinemachine;
using UnityEngine;

public class LockCameraY : CinemachineExtension
{
    [SerializeField]public PlayerInputController playerInputController;
    private float posY;
    protected override void Awake()
    {
        base.Awake();
        posY = playerInputController.transform.position.y;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.y = posY;
            state.RawPosition = pos;
        }
    }
}
