using UnityEngine;

public class CameraGameObj : GameObj {
    private CameraData cameraData;
    public override void Init(Game game, Data data) {
        base.Init(game, data);
        cameraData = (CameraData)data;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
        // 更新主角色相机目标
        UpdateMainCharacterCameraTarget();
        // 相机追踪行为
        TraceBehaviour();
    }

    private void UpdateMainCharacterCameraTarget() {
        if (cameraData.MyCameraType == CameraType.CharacterCamera) {
            if (GameData.MainCharacater != -1) {
                cameraData.MyCameraTarget = GameData.MainCharacterComponent.CameraTarget;
            }
        }
    }

    private void TraceBehaviour() {
        if (cameraData.MyCameraType == CameraType.CharacterCamera) {
            if (null != cameraData.MyCameraTarget) {
                cameraData.MyObj.transform.position = Vector3.Lerp(cameraData.MyObj.transform.position, cameraData.MyCameraTarget.transform.position, 10 * Time.deltaTime);
                cameraData.MyObj.transform.rotation = Quaternion.Slerp(cameraData.MyObj.transform.rotation, cameraData.MyCameraTarget.transform.rotation, 10 * Time.deltaTime);
            }
        }
    }
}