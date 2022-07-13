using UnityEngine;

public class BehaviourSystem : GameSys {
    private float countFrame;
    private GameSystem gameSystem;
    private CharacterData mainCharacterData;
    private CameraComponent mainCharacterCameraComponent;

    public override void Init(GameSystem gameSystem) {
        base.Init(gameSystem);
        this.gameSystem = gameSystem;
    }

    public override void Update() {
        base.Update();
        OnCameraScreenCenterRayRecognize();
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void Clear() {
        base.Clear();
    }

    public override void LateUpdate() {
        base.LateUpdate();
    }

    // 相机中心打出射线获取物体
    private void OnCameraScreenCenterRayRecognize() {
        // ++countFrame;
        // if (countFrame % 10 != 0) {
        //     return;
        // }
        if (GameData.MainCharacater != -1) {
            mainCharacterData = gameSystem.MyGameObjFeature.Get<CharacterGameObj>(GameData.MainCharacater)
                .GetData<CharacterData>();
            mainCharacterCameraComponent = gameSystem.MyGameObjFeature.Get<CameraGameObj>(GameData.MainCharacterCamera)
                .GetComponent<CameraComponent>();

            // 获取角色相机
            var camera = mainCharacterCameraComponent.MyCamera;

            // 打射线
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // 获取贴士 UI
            var uiTipWindow = gameSystem.MyWindowFeature.Get<UITipWindow>();
            if (null == uiTipWindow) {
                return;
            }

            // 打射线
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9)) {
                var sceneItemId = hit.collider.gameObject.GetInstanceID();

                // 获取物体标识
                var sceneItemGameObj = gameSystem.MyGameObjFeature.Get<SceneItemGameObj>(sceneItemId);
                var sceneItemComponent = sceneItemGameObj.GetComponent<SceneItemComponent>();
                var name = sceneItemGameObj.GetData<SceneItemData>().MySceneItemSign;

                // 设置贴士 UI 内容
                uiTipWindow.SetTipDescription(UITipType.ItemNameTip, name);
                uiTipWindow.SetTipDescription(UITipType.ItemKeycode, "拾取[F]");
                uiTipWindow.Open();

                // 输入F键
                if (gameSystem.MyInputSystem.GetKeyDown(KeyCode.F)) {
                    if (sceneItemComponent.MySceneItemType == SceneItemType.MainWeapon) {
                        // 根据物品的类型 将物品 id 放入玩家 物品 id 中
                        if (mainCharacterData.AddSceneItemMainWeapon(sceneItemId)) {
                            Debug.Log("拾取主武器：" + sceneItemId);
                        }
                    } else if (sceneItemComponent.MySceneItemType == SceneItemType.SideWeapon) {
                        if (mainCharacterData.AddSceneItemSideWeapon(sceneItemId)) {
                            Debug.Log("拾取副武器：" + sceneItemId);
                        }
                    } else if (sceneItemComponent.MySceneItemType == SceneItemType.Consume) {
                    }

                    sceneItemGameObj.HideObj();
                }

                Debug.DrawLine(mainCharacterCameraComponent.MyCamera.transform.position, hit.point, Color.red);
                Debug.Log("检测物体 " + name + " InstanceId: " + hit.collider.gameObject.GetInstanceID());
            } else {
                uiTipWindow.Close();
            }
        }
    }
}