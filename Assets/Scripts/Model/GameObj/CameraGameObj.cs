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
        cameraData = (CameraData)data;
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
                float offY = soCameraSetting.CameraOffsetPosition.y;
                float offZ = soCameraSetting.CameraOffsetPosition.z;

                // cameraComponent.MyCameraX.localPosition = new Vector3(0, mouseY, 0);

                // 父物体位置
                cameraTran.position = Vector3.Lerp(cameraTran.position, characterTran.position,
                    Time.deltaTime * soCameraSetting.CameraTraceSpeed);

                // 父物体旋转
                mouseY -= inputSystem.GetAxis("Mouse Y") * 0.5f;
                cameraTran.rotation = Quaternion.Slerp(cameraTran.rotation,
                    characterTran.rotation * Quaternion.Euler(new Vector3(mouseY, 0, 0)),
                    Time.deltaTime * soCameraSetting.CameraTraceSpeed);

                RaycastHit hit;
                var targetTran = GameData.MainCharacterComponent.Head.transform;
                Ray ray = new Ray(targetTran.position,
                    (cameraComponent.MyCameraX.transform.position - targetTran.transform.position).normalized);
                if (Physics.Raycast(ray, out hit, 3.5f, ~(1 << 8))) {
                    if (Vector3.Distance(targetTran.position, hit.point) < 3.5f) {
                        cameraComponent.MyCameraX.transform.position = hit.point;
                    } else {
                        cameraComponent.MyCameraX.transform.localPosition = Vector3.Lerp(
                            cameraComponent.MyCameraX.transform.localPosition, soCameraSetting.CameraOffsetPosition,
                            Time.deltaTime * soCameraSetting.CameraTraceSpeed);
                    }
                } else {
                    cameraComponent.MyCameraX.transform.localPosition = Vector3.Lerp(
                        cameraComponent.MyCameraX.transform.localPosition, soCameraSetting.CameraOffsetPosition,
                        Time.deltaTime * soCameraSetting.CameraTraceSpeed);
                }

                Debug.Log(mouseY);

                // 相机旋转
                cameraComponent.MyCamera.transform.LookAt(GameData.MainCharacterComponent.Head.transform.position);

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