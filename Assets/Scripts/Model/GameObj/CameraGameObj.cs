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

    private CameraData cameraData;
    private Game game;

    public override void Init(Game game, Data data) {
        base.Init(game, data);
        this.game = game;
        cameraData = (CameraData) data;
        MyComponent = MyObj.transform.GetComponent<CameraComponent>();
        cameraComponent = (CameraComponent) MyComponent;
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
        // 相机追踪行为
        TraceBehaviour();
        // 相机射线中心物体识别
        OnCameraScreenCenterRayRecognize();
    }

    // 相机中心打出射线获取物体
    private void OnCameraScreenCenterRayRecognize() {
        // 角色未加载
        if (!GameData.IsHaveMainCharacter) {
            return;
        }

        // 不是角色相机
        if (cameraData.MyCameraType != CameraType.MainCharacterCamera) {
            return;
        }

        var mCamera = cameraComponent.MyCamera;

        // 打射线
        Ray ray = mCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // 获取贴士 UI
        var uiTipWindow = game.MyWindowFeature.Get<UITipWindow>();
        if (null == uiTipWindow) {
            return;
        }

        // 打射线
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9 | 1 << 12)) {
            var id = hit.collider.gameObject.GetInstanceID();
            var layer = hit.collider.gameObject.layer;
            string name = "";
            switch (layer) {
                case 9:
                    // 获取物体标识
                    var sceneItemGameObj = game.MyGameSystem.MyItemSystem.GetSceneItemGameObj(id);
                    name = sceneItemGameObj.GetData<SceneItemData>().MySceneItemSign;
                    
                    // 输入F键
                    if (inputSystem.GetKeyDown(KeyCode.F)) {
                        MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.BACKPACKSYSTEM_ADDSCENEITEM, id);
                    }
                    
                    break;
                case 12:
                    // 获取物体标识
                    var weaponGameObj = game.MyGameSystem.MyWeaponSystem.GetWeaponGameObj(id);
                    name = weaponGameObj.GetData<WeaponData>().MyWeaponSign;
                    
                    // 输入F键
                    if (inputSystem.GetKeyDown(KeyCode.F)) {
                        MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.BACKPACKSYSTEM_ADDWEAPON, id);
                    }
                    
                    break;
            }

            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, UITipType.ItemNameTip, name);
            MyGame.MyGameMessageCenter.Dispather(GameMessageConstants.UITIPWINDOW_SETTIPDESCRIPTION, UITipType.ItemKeycode, "拾取[F]");



            Debug.Log("检测物体 " + name + " InstanceId: " + hit.collider.gameObject.GetInstanceID());
        } else {
            uiTipWindow.Close();
        }
    }

    private void TraceBehaviour() {
        // 主角色主相机
        if (cameraData.MyCameraType == CameraType.MainCharacterCamera) {
            if (null != GameData.MainCharacterComponent) {
                var characterTran = GameData.MainCharacterComponent.transform;

                // 父物体位置
                cameraTran.position = Vector3.Lerp(cameraTran.position, characterTran.position,
                    Time.deltaTime * SOData.MySOCameraSetting.CameraTraceSpeed);

                // 父物体旋转
                mouseY -= inputSystem.GetAxis("Mouse Y") * 0.5f;
                cameraTran.rotation = Quaternion.Slerp(cameraTran.rotation,
                    characterTran.rotation * Quaternion.Euler(new Vector3(mouseY, 0, 0)),
                    Time.deltaTime * SOData.MySOCameraSetting.CameraTraceSpeed);

                RaycastHit hit;
                var targetTran = GameData.MainCharacterComponent.Head.transform;
                Ray ray = new Ray(targetTran.position,
                    (cameraComponent.MyCameraX.transform.position - targetTran.transform.position).normalized);
                if (Physics.Raycast(ray, out hit, 3.5f, ~(1 << 8))) {
                    if (Vector3.Distance(targetTran.position, hit.point) < 3.5f) {
                        cameraComponent.MyCameraX.transform.position = hit.point;
                    } else {
                        cameraComponent.MyCameraX.transform.localPosition = Vector3.Lerp(
                            cameraComponent.MyCameraX.transform.localPosition, SOData.MySOCameraSetting.CameraOffsetPosition,
                            Time.deltaTime * SOData.MySOCameraSetting.CameraTraceSpeed);
                    }
                } else {
                    cameraComponent.MyCameraX.transform.localPosition = Vector3.Lerp(
                        cameraComponent.MyCameraX.transform.localPosition, SOData.MySOCameraSetting.CameraOffsetPosition,
                        Time.deltaTime * SOData.MySOCameraSetting.CameraTraceSpeed);
                }

                // 相机旋转
                cameraComponent.MyCamera.transform.LookAt(GameData.MainCharacterComponent.Head.transform.position + SOData.MySOCameraSetting.LookTargetOffsetPosition);

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