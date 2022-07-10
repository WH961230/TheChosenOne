using UnityEngine;

public class CameraGameObj : GameObj {
    private Quaternion cameraTranDefaultRotation;
    private Vector3 cameraTranDefaultPosition;
    private float mouseY;
    private Transform cameraTran;
    private CameraComponent cameraComponent;
    
    private InputSystem inputSystem {
        get { return game.MyGameSystem.MyInputSystem; }
    }

    private SOCameraSetting soCameraSetting {
        get { return game.MyGameSystem.MyCameraSystem.MySoCameraSetting; }
    }

    private CameraData cameraData;
    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        cameraData = (CameraData) data;
        cameraComponent = MyObj.transform.GetComponent<CameraComponent>();
        cameraTran = cameraData.MyObj.transform;
    }

    public override void Clear() {
        base.Clear();
    }

    public override void Update() {
        base.Update();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void LateUpdate() {
        base.LateUpdate();
        // 更新主角色相机目标
        UpdateMainCharacterCameraTarget();
        // 相机追踪行为
        TraceBehaviour();
    }

    private void UpdateMainCharacterCameraTarget() {
        if (cameraData.MyCameraType == CameraType.MainCharacterCamera) {
            if (GameData.MainCharacater != -1) {
            }
        }
    }

    private void TraceBehaviour() {
        // 主角色主相机
        if (cameraData.MyCameraType == CameraType.MainCharacterCamera) {
            if (null != GameData.MainCharacterComponent) {
                var characterTran = GameData.MainCharacterComponent.transform;
                //计算出相机的位置
                mouseY -= inputSystem.GetAxis("Mouse Y") * 0.1f;
                Vector3 disPos = characterTran.position + Vector3.up * (soCameraSetting.CameraOffsetPosition.y + mouseY) -
                                 characterTran.forward * soCameraSetting.CameraOffsetPosition.z;

                cameraTran.position = Vector3.Lerp(cameraTran.position, disPos, Time.deltaTime * 5);
                cameraTran.LookAt(GameData.MainCharacterComponent.Head.transform.position);

                // 自由状态
                if (inputSystem.GetKey(KeyCode.LeftAlt) || inputSystem.GetKey(KeyCode.RightAlt)) {
                    var rX = inputSystem.GetAxis("Mouse Y");
                    var rY = inputSystem.GetAxis("Mouse X");
                    cameraData.MyObj.transform.Rotate(new Vector3(rX, rY, 0));
                }
            }
        }
    }
}